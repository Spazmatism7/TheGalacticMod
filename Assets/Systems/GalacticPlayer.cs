﻿using GalacticMod.Buffs;
using GalacticMod.Items;
using GalacticMod.NPCs;
using GalacticMod.NPCs.Bosses;
using GalacticMod.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.UI;
using GalacticMod.Items.Boss;
using GalacticMod.NPCs.Bosses.PostML;
using GalacticMod.NPCs.Bosses.PreHM;
using GalacitcMod.Items;

namespace GalacticMod.Assets.Systems
{
    public class GalacticPlayer : ModPlayer
    {
        public float GrenadeExplosionModifier = 1f;
        public bool HellflameBonus; //Player has full set of Hellflame armour
        public bool SteelBonus; //Player has full set of Hellflame armour
        public bool GalacticAmulet;
        public bool Hellfire;
        public bool VanadiumHeal;
        public int modDash;
        public bool OsmiumDamage;
        public bool ZirconiumSpeed;
        public bool Smoke;
        public bool glacierDebuff;
        public bool sandBlasted;
        public bool spiritCurse;
        public bool terraBurn;
        public bool shadeBonus;
        public bool asteroidBlaze;
        public bool iridiumPoisoning;
        public bool elementalBlaze;
        public bool ZirconiumWarp;

        public int defenseBuffs;
        public float oldEndurance;
        public int osmiumSouls;
        public int osmiumSoulsMax = 16;
        public bool zirconiumCharged; //Activates when the player is able to teleport with the Zirconium armour

        //public Vector2 HitboxPosition = Vector2.Zero;

        public bool spineScarf;
        public int spineScarfCounter;

        public int cooldown;

        public override void ResetEffects()
        {
            GrenadeExplosionModifier = 1f;
            HellflameBonus = false;
            SteelBonus = false;
            GalacticAmulet = false;
            Hellfire = false;
            VanadiumHeal = false;
            modDash = 0;
            OsmiumDamage = false;
            ZirconiumSpeed = false;
            Smoke = false;
            glacierDebuff = false;
            sandBlasted = false;
            spiritCurse = false;
            terraBurn = false;
            shadeBonus = false;
            asteroidBlaze = false;
            iridiumPoisoning = false;
            defenseBuffs = 0;
            oldEndurance = 0;
            osmiumSouls = 0;
            osmiumSoulsMax = 16;
            elementalBlaze = false;
            ZirconiumWarp = false;

            spineScarf = false;

            cooldown = 0;
        }

        public override void UpdateDead() //Reset all ints and bools if dead
        {
            osmiumSouls = 0;
            zirconiumCharged = false;
        }

        int HfProj = ProjectileType<HellflameArmorProj>();

        public override void PostUpdateEquips()
        {
            oldEndurance = Player.endurance;

            if (spineScarf)
                SpineScarf();

            if (GetInstance<Gamemodes>().noHitMode)
            {
                Player.endurance = -9000;
            }
            if (GetInstance<Gamemodes>().BloodMode)
            {
                Player.lifeRegenTime = 0;
                Player.GetDamage(DamageClass.Generic) += 0.2f;
                Player.statLifeMax2 *= 2;
                Player.lifeRegen = 0;
            }

            if (osmiumSouls > osmiumSoulsMax)
                osmiumSouls = osmiumSoulsMax;

            cooldown--;

            switch (defenseBuffs)
            {
                case > 7:
                    Player.endurance = oldEndurance - 100;
                    oldEndurance = Player.endurance + 100;
                    break;
                case > 6:
                    Player.endurance = oldEndurance - 60;
                    oldEndurance = Player.endurance + 60;
                    break;
                case > 4:
                    Player.endurance = oldEndurance - 40;
                    oldEndurance = Player.endurance + 40;
                    break;
                case > 2:
                    Player.endurance = oldEndurance - 20;
                    oldEndurance = Player.endurance + 20;
                    break;
                case < 2:
                    Player.endurance = oldEndurance;
                    break;
            }

            //For Zirconium Armour ====================================================================

            float xWarplimit = 640; //640 = ~40 tiles
            float yWarplimit = 400; //400 = ~25 tiles
            if (ZirconiumWarp)
            {
                float distanceX = Player.Center.X - Main.MouseWorld.X;
                float distanceY = Player.Center.Y - Main.MouseWorld.Y;
                float distance = (float)Math.Sqrt((double)(distanceX * distanceX + distanceY * distanceY));

                int xcursor = (int)(Main.MouseWorld.X / 16);
                int ycursor = (int)(Main.MouseWorld.Y / 16);
                Tile tile = Main.tile[xcursor, ycursor];
                if ((tile != null && !tile.HasTile || !Main.tileSolid[tile.TileType]) && !Player.HasBuff(BuffType<Instability>())) //Checks if mouse is in valid postion
                {
                    if (((distanceX < -xWarplimit || distanceX > xWarplimit || distanceY < -yWarplimit || distanceY > yWarplimit) && Collision.CanHitLine(Main.MouseWorld, 1, 1, Player.position, Player.width, Player.height)) ||
                        (distanceX > -xWarplimit && distanceX < xWarplimit && distanceY > -yWarplimit && distanceY < yWarplimit)) //If there is no line of sight and cursor is past limit, don't allow teleport to prevent gettign stuck in blocks
                    {
                        zirconiumCharged = true; //Activates the outline effect on the armour

                        if (GalacticMod.ArmourSpecialHotkey.JustPressed) //Activates when player presses button
                        {
                            Player.AddBuff(BuffType<Instability>(), 7 * 60);
                            Player.AddBuff(BuffType<ZirconiumDebuff>(), 7 * 60);

                            Player.grappling[0] = -1; //Remove grapple hooks
                            Player.grapCount = 0;
                            for (int p = 0; p < 1000; p++)
                            {
                                if (Main.projectile[p].active && Main.projectile[p].owner == Player.whoAmI && Main.projectile[p].aiStyle == 7)
                                {
                                    Main.projectile[p].Kill();
                                }
                            }
                            for (int i = 0; i < 30; i++) //Dust pre-teleport
                            {
                                var dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Sunflower);
                                dust.scale = 1.1f;
                                dust.velocity *= 2;
                                //dust.noGravity = true;
                            }
                            for (int i = 0; i < 30; i++)
                            {
                                var dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.AncientLight);
                                dust.scale = 1.5f;
                                dust.noGravity = true;
                                dust.fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
                            }
                            //X postion 
                            {
                                if (distanceX <= xWarplimit && distanceX >= -xWarplimit)
                                {
                                    Player.position.X = Main.MouseWorld.X - (Player.width / 2);
                                    //Main.NewText("Little mouse X", 0, 146, 0);
                                }
                                else
                                {
                                    if (distanceX < -xWarplimit)
                                    {
                                        Player.position.X = Main.MouseWorld.X - (Player.width / 2) + (distanceX + xWarplimit);
                                        //Main.NewText("Mouse it to the right", 146, 0, 0);
                                    }
                                    else if (distanceX > xWarplimit)
                                    {
                                        Player.position.X = Main.MouseWorld.X - (Player.width / 2) + (distanceX - xWarplimit);
                                        //Main.NewText("Mouse it to the left", 146, 0, 0);
                                    }
                                }
                            }
                            //Y postion 
                            {
                                if (distanceY <= yWarplimit && distanceY >= -yWarplimit)
                                {
                                    Player.position.Y = Main.MouseWorld.Y - Player.height;
                                    //Main.NewText("Little mouse Y", 0, 146, 0);
                                }
                                else
                                {
                                    if (distanceY < -yWarplimit)
                                    {
                                        Player.position.Y = Main.MouseWorld.Y - Player.height + (distanceY + yWarplimit);
                                        //Main.NewText("Mouse it to the down", 0, 0, 146);
                                    }
                                    else if (distanceY > yWarplimit)
                                    {
                                        Player.position.Y = Main.MouseWorld.Y - Player.height + (distanceY - yWarplimit);
                                        //Main.NewText("Mouse it to the up", 0, 0, 146);
                                    }
                                }
                            }

                            for (int i = 0; i < 30; i++) //Dust post-teleport
                            {
                                var dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.AncientLight);
                                dust.scale = 1.1f;
                                dust.velocity *= 2;
                            }
                            for (int i = 0; i < 30; i++)
                            {
                                var dust = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Sunflower);
                                dust.scale = 1.5f;
                                dust.noGravity = true;
                                dust.fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;

                            }
                            SoundEngine.PlaySound(SoundID.Item8 with { Volume = 2f, Pitch = -0.5f }, Player.Center);
                        }
                    }
                    else
                        zirconiumCharged = false;
                }
                else
                    zirconiumCharged = false; //Removes the outline effect if the player is unable to charge
            }

            if (NPC.AnyNPCs(NPCType<JormungandrHead>()))
            {
                Lighting.GlobalBrightness = .1f;
            }
        }

        public override void UpdateLifeRegen()
        {
            if (GetInstance<Gamemodes>().BloodMode)
            {
                Player.lifeRegenTime = 0;
                Player.lifeRegen = 0;
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit) //Hitting enemies with True Melee Only
        {
            if (SteelBonus)
            {
                if (crit)
                {
                    Player.AddBuff(BuffID.AmmoReservation, 2 * 60);
                }
            }

            if (GalacticAmulet)
            {
                if (Main.rand.NextBool(2))
                {
                    for (int j = 0; j < 1; j++)
                    {
                        float xpos = (Main.rand.NextFloat(-50, 50));

                        float ai = Main.rand.Next(100);

                        Vector2 rotation = -new Vector2(target.Center.X - xpos, target.Center.Y - 500) + target.Center;

                        int projID = Projectile.NewProjectile(null, new Vector2(target.Center.X - xpos, target.Center.Y - 500), new Vector2(xpos * 0.02f, 5),
                            ModContent.ProjectileType<Items.PostML.Galactic.GalacticAmuletLightning>(), damage, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                        //player.GetProjectileSource_SetBonus(0)
                        Main.projectile[projID].scale = 1;
                        Main.projectile[projID].penetrate = 2;
                        Main.projectile[projID].timeLeft = 600;
                        Main.projectile[projID].DamageType = DamageClass.Generic;
                        Main.projectile[projID].tileCollide = true;
                    }
                }
            }

            if (OsmiumDamage)
            {
                Player.AddBuff(BuffType<OsmiumStrength>(), 10 * 60);
                /*if (osmiumSouls == osmiumSoulsMax)
                {
                    Projectile.NewProjectile(null, new Vector2(target.Center.X, target.Center.Y), new Vector2(0, 0), ProjectileType<HellflameArmorProj>(), damage, 1, Player.whoAmI);
                }
                else
                {
                    osmiumSouls++;
                }*/
            }

            if (VanadiumHeal)
            {
                Player.AddBuff(BuffType<VanadiumHealing>(), 3 * 60);
            }

            if (ZirconiumSpeed)
            {
                Player.AddBuff(BuffType<ZirconiumRun>(), 7 * 60);
            }
        }

        public bool exemptProjs = false;

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit) //Hitting enemy with any projectile
        {
            if (shadeBonus && proj.DamageType == DamageClass.Throwing)
            {
                if (Main.rand.NextBool(2))
                {
                    Projectile.NewProjectile(null, new Vector2(target.Center.X, target.Center.Y), new Vector2(0, 0), ProjectileType<Shade>(), damage, knockback, Player.whoAmI);
                    //player.GetProjectileSource_SetBonus(0)
                }
            }

            if (SteelBonus)
            {
                if (crit)
                {
                    Player.AddBuff(93, 2 * 60); //Ammo Reservation
                }
            }

            if (GalacticAmulet && !exemptProjs)
            {
                if (Main.rand.NextBool(3))
                {
                    for (int j = 0; j < 1; j++)
                    {
                        float xpos = Main.rand.NextFloat(-50, 50);

                        float ai = Main.rand.Next(100);

                        Vector2 rotation = -new Vector2(target.Center.X - xpos, target.Center.Y - 500) + target.Center;

                        int projID = Projectile.NewProjectile(null, new Vector2(target.Center.X - xpos, target.Center.Y - 500), new Vector2(xpos * 0.02f, 5),
                            ModContent.ProjectileType<Items.PostML.Galactic.GalacticAmuletLightning>(), damage, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                        //player.GetProjectileSource_SetBonus(0)
                        Main.projectile[projID].scale = 1;
                        Main.projectile[projID].penetrate = 2;
                        Main.projectile[projID].timeLeft = 600;
                        Main.projectile[projID].DamageType = DamageClass.Generic;
                        Main.projectile[projID].tileCollide = true;
                    }
                }
            }

            if (OsmiumDamage)
            {
                Player.AddBuff(BuffType<OsmiumStrength>(), 10 * 60);
                /*if (osmiumSouls == osmiumSoulsMax)
                {
                    Projectile.NewProjectile(null, new Vector2(target.Center.X, target.Center.Y), new Vector2(0, 0), ProjectileType<HellflameArmorProj>(), damage, 1, Player.whoAmI);
                }
                else
                {
                    osmiumSouls++;
                }*/
            }

            if (GetInstance<GalacticPlayer>().VanadiumHeal)
            {
                //GetInstance<GalacticProjectile>().vanadiumHeal(damage, new Vector2(target.Center.X, target.Center.Y), target, proj);
                Player.AddBuff(BuffType<VanadiumHealing>(), 3 * 60);
            }

            if (ZirconiumSpeed)
            {
                Player.AddBuff(BuffType<ZirconiumRun>(), 7 * 60);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (Hellfire)
            {
                Player.lifeRegen = -26;
            }
            if (Player.bleed && GetInstance<Gamemodes>().BloodMode)
            {
                Player.lifeRegen = -4;
            }
            if (sandBlasted)
            {
                Player.lifeRegen = -10;
            }
            if (spiritCurse)
            {
                Player.lifeRegen = -22;
            }
            if (terraBurn)
            {
                Player.lifeRegen = -22;
            }
            if (asteroidBlaze)
            {
                Player.lifeRegen = -19;
            }
            if (iridiumPoisoning)
            {
                Player.lifeRegen = -22;
            }
            if (elementalBlaze)
            {
                Player.lifeRegen = -24;
            }
        }

        public void SpineScarf()
        {
            if (Main.myPlayer != Player.whoAmI)
            {
                return;
            }
            spineScarfCounter++;
            if (spineScarfCounter <= 40)
            {
                return;
            }
            spineScarfCounter = 0;
            int damage = 45;
            float knockBack = 7f;
            float num = 640f;
            NPC nPC = null;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC2 = Main.npc[i];
                if (nPC2 != null && nPC2.active && nPC2.CanBeChasedBy(Player) && Collision.CanHit(Player, nPC2))
                {
                    float num2 = Vector2.Distance(nPC2.Center, Player.Center);
                    if (num2 < num)
                    {
                        num = num2;
                        nPC = nPC2;
                    }
                }
            }
            if (nPC != null)
            {
                Vector2 v = nPC.Center - Player.Center;
                v = v.SafeNormalize(Vector2.Zero) * 12f;
                v.Y -= 1.3f;
                Projectile.NewProjectile(null, Player.Center.X, Player.Center.Y, v.X, v.Y, ProjectileType<Spine>(), damage, knockBack, Player.whoAmI);
            }
        }
    }
}