using GalacticMod.Items;
using GalacticMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System;
using System.IO;
using GalacticMod.Assets.Systems;
using Terraria.Audio;
using GalacticMod;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;
using GalacticMod.Items.PostML.Hellfire;
using GalacticMod.Projectiles.Boss;
using Terraria.GameContent.ItemDropRules;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalacticMod.Assets;
using GalacticMod.Items.Boss;
using GalacticMod.Items.Boss.Master;

namespace GalacticMod.NPCs.Bosses.PostML
{
    [AutoloadBossHead]
    public class HellfireDragon : ModNPC
    {
        private bool halfHealth = false;
        private bool enraged = false;

        private enum phases 
        {
            shooting = 400,
            dashing = 800,
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Dragon");
            Main.npcFrameCount[NPC.type] = 9;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                // Influences how the NPC looks in the Bestiary
                Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 418;
            NPC.height = 382;

            NPC.boss = true;
            NPC.aiStyle = -1;
            NPC.npcSlots = 5;
            NPC.lifeMax = 1400420;
            NPC.damage = 100;
            NPC.defense = 75;
            NPC.knockBackResist = 0f;

            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/RagingFire");

            NPC.value = Item.buyPrice(gold: 20);
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale) 
        {
            NPC.lifeMax = 175000;
            NPC.lifeMax = (int)(NPC.lifeMax * bossLifeScale);
        }

        public override void AI() 
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;
            NPC.rotation = 0.0f;
            NPC.netAlways = true;
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;

            //Immunities
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Oiled] = true;
            NPC.buffImmune[BuffID.BoneJavelin] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffType<Buffs.HellfireDebuff>()] = true;
            NPC.buffImmune[BuffType<Buffs.SpiritCurse>()] = true;

            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead)
                {
                    NPC.velocity = new Vector2(0f, 10f);
                    if (NPC.timeLeft > 10)
                    {
                        NPC.timeLeft = 10;
                    }
                    return;
                }
            }

            if (!player.ZoneUnderworldHeight)
            {
                enraged = true;
                NPC.defense = 999999;
                NPC.damage = 999999;
            }

            if (NPC.life < NPC.lifeMax / 2)
            {
                halfHealth = true;
            }

            if (Main.player[NPC.target].position.X < NPC.position.X)
            {
                if (NPC.velocity.X > -8) NPC.velocity.X -= 0.22f;
            }

            if (Main.player[NPC.target].position.X > NPC.position.X)
            {
                if (NPC.velocity.X < 8) NPC.velocity.X += 0.22f;
            }

            if (Main.player[NPC.target].position.Y < NPC.position.Y + 300)
            {
                if (NPC.velocity.Y < 0) // <--
                {
                    if (NPC.velocity.Y > -4) NPC.velocity.Y -= 0.7f;
                }
                else NPC.velocity.Y -= 0.8f;
            }

            if (Main.player[NPC.target].position.Y > NPC.position.Y + 300)
            {
                if (NPC.velocity.Y > 0) // <--
                {
                    if (NPC.velocity.Y < 4) NPC.velocity.Y += 0.7f;
                }
                else NPC.velocity.Y += 0.8f;
            }

            NPC.ai[0]++;

            if (Main.rand.NextBool(10))
            {
                if (NPC.ai[0] >= 10 && !Main.expertMode)
                {
                    if (enraged)
                    {
                        float projectileSpeed = 21f;
                        Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));

                        int damageFireball = 9999999;
                        int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<Fireball>(), damageFireball, .5f, 0);
                        //NPC.GetSpawnSource_ForProjectile()
                    }
                    else if (halfHealth)
                    {
                        float projectileSpeed = 21f;
                        Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));

                        int damageFireball = 75;
                        int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<Fireball>(), damageFireball, .5f, 0);
                        //NPC.GetSpawnSource_ForProjectile()
                    }
                    else
                    {
                        float projectileSpeed = 20f;
                        Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));

                        int damageFireball = 50;
                        int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<Fireball>(), damageFireball, .5f, 0);
                        //NPC.GetSpawnSource_ForProjectile()

                    }
                    SoundEngine.PlaySound(SoundID.Item34, NPC.Center);
                }

                if (Main.expertMode && NPC.ai[0] >= 10)
                {
                    float projectileSpeed = 20f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
                    if (enraged)
                    {
                        int damageFireball = 9999999;
                        int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<Fireball>(), damageFireball, .5f, 0);
                        //NPC.GetSpawnSource_ForProjectile()
                    }
                    else if (halfHealth)
                    {
                        int damageExpertFireball = 75;
                        int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<Fireball>(), damageExpertFireball, .5f, 0);
                        //NPC.GetSpawnSource_ForProjectile()
                    }
                    else
                    {
                        int damageExpertFireball = 100;
                        int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<Fireball>(), damageExpertFireball, .5f, 0);
                        //NPC.GetSpawnSource_ForProjectile()
                    }
                    SoundEngine.PlaySound(SoundID.Item34, NPC.Center);
                }
            }

            if (Main.rand.NextBool(240))
            {
                if (NPC.ai[0] >= 10)
                {
                    float projectileSpeed = 30f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
                    int damageFirenado = 200;
                    SoundEngine.PlaySound(SoundID.Item34, NPC.Center);

                    int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y),
                    ModContent.ProjectileType<FirenadoBolt>(), damageFirenado, .5f, 0);
                    //NPC.GetSpawnSource_ForProjectile()
                }
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

                new FlavorTextBestiaryInfoElement("The Dragon of the long missing Inferno Elemental, the most powerful of the Elementals")
            });
        }

        /*public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit)
        {
            if (projectile.type == 504 || projectile.type == 399 || projectile.type == 400 || projectile.type == 401 || projectile.type == 402 || projectile.type == 375 || projectile.type == 2 || projectile.type == 163 || projectile.type == 310 || projectile.type == 15 || projectile.type == 19 || projectile.type == 34 || projectile.type == 948 || projectile.type == 35 || projectile.type == 85 || projectile.type == 545 || projectile.type == 553 || projectile.type == 295 || projectile.type == 296 || projectile.type == 705 || projectile.type == 706)
            {
                damage /= 2;
            }
        }*/

        int npcframe = 0;
        public override void FindFrame(int frameHeight)//Animations
        {
            NPC.frame.Y = npcframe * frameHeight;

            NPC.frameCounter++;
            if (NPC.frameCounter > 9)
            {
                npcframe++;
                NPC.frameCounter = 0;
            }
            if (npcframe >= 9)
            {
                npcframe = 0;
            }
        }

        private void MoveTowards(NPC NPC, Vector2 target, float speed, float turnResistance) 
        {
            var move = target - NPC.Center;
            float length = move.Length();
            if (length > speed) {
                move *= speed / length;
            }
            move = (NPC.velocity * turnResistance + move) / (turnResistance + 1f);
            length = move.Length();
            if (length > speed) {
                move *= speed / length;
            }
            NPC.velocity = move;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (Main.masterMode)
                npcLoot.Add(ItemDropRule.Common(ItemType<HellfireDragonRelic>()));

            npcLoot.Add(ItemDropRule.Common(ItemType<HellfireDragonTrophy>(), 10));

            if (Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.BossBag(ItemType<HellfireDragonBag>()));
            }
            else
            {
                if (Main.rand.NextBool(7))
                {
                    //npcLoot.Add(ItemDropRule.Common(ItemType<HellfireMask>()));
                }

                npcLoot.Add(ItemDropRule.OneFromOptions(7, ItemType<HellflameHelmet>(), ItemType<HellflameChestplate>(), ItemType<HellflameGreaves>()));

                npcLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<HellfireWings>(), ItemID.HellfireTreads, ItemID.FireGauntlet));
                npcLoot.Add(ItemDropRule.Common(ItemType<SigilCore>()));
                npcLoot.Add(ItemDropRule.Common(ItemType<Items.ThrowingClass.Weapons.Explosives.HellfireGrenade>(), 1, 50, 58));
                npcLoot.Add(ItemDropRule.Common(ItemType<HellfireBar>(), 1, 25, 25));
            }
        }

        public override void OnKill()
        {
            Downeds.DownedHellDragonBoss = true;
        }

        public override void BossLoot(ref string name, ref int potionType) {
            potionType = ItemID.SuperHealingPotion;
        }
    }
}