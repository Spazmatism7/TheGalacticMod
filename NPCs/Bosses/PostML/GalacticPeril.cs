using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Bestiary;
using GalacticMod.Items.PostML.Galactic;
using GalacticMod.Projectiles.Boss;
using GalacticMod.Assets.Systems;
using GalacticMod.Buffs;
using Terraria.GameContent.ItemDropRules;
using GalacticMod.Items.Boss;
using GalacticMod.Items.ThrowingClass.Weapons.Explosives;
using GalacticMod.Items.Boss.Master;
using GalacticMod.Items.Accessories.Runes;

namespace GalacticMod.NPCs.Bosses.PostML
{
    [AutoloadBossHead]
    public class GalacticPeril : ModNPC
	{
        //AI VARIABLES
        public int phase = 1;
        public int dashers;
        public int howMuchLightning;
        public int numAncientCurse;
        public int numBolts;
        public int numBalls;

        //OLD HD VARIABLES
        private int hoverHeight = 350;
        private int hoverWidth = 500;
        private int speedSlow = 40;
        private int speedFast = 50;
        private bool left = true;
#pragma warning disable IDE0044 // Add readonly modifier
        private int distance;
#pragma warning restore IDE0044 // Add readonly modifier

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Galactic Peril");
            Main.npcFrameCount[NPC.type] = 1;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.width = 54;
            NPC.height = 92;

            NPC.boss = true;
            NPC.aiStyle = -1;
            NPC.npcSlots = 5;
            
            NPC.lifeMax = 181000;
            NPC.damage = 200;
            NPC.defense = 90;
            NPC.knockBackResist = 0f;

            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/TheUniverseIsInGalacticPeril");

            NPC.value = Item.buyPrice(gold: 10);
        }
        
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * bossLifeScale);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A terrifying cosmic being with enough power to extinguish 100 stars, " +
                " almost nothing can stop this abomination")
            });
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;
            NPC.rotation = 0.0f;
            NPC.netAlways = true;
            NPC.TargetClosest(true);

            if (NPC.life <= NPC.lifeMax / 20 && Main.expertMode)
                phase = 6;
            else if (NPC.life <= NPC.lifeMax / 5)
                phase = 5;
            else if (NPC.life <= 2 * (NPC.lifeMax / 5))
                phase = 4;
            else if (NPC.life <= 3 * (NPC.lifeMax / 5))
                phase = 3;
            else if (NPC.life <= 4 * (NPC.lifeMax / 5))
                phase = 2;
            else if (NPC.life >= 4 * (NPC.lifeMax / 5))
                phase = 1;

            //Immunities
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Oiled] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.BoneJavelin] = true;
            NPC.buffImmune[BuffID.DryadsWardDebuff] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffType<TerraBurn>()] = true;

            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead)
                {
                    NPC.velocity = new Vector2(0f, 10f);
                    if (NPC.timeLeft > 20)
                    {
                        NPC.timeLeft = 20;
                    }
                    return;
                }
            }

            if (NPC.AnyNPCs(NPCType<TheStorm>()))
            {
                NPC.dontTakeDamage = true;
                NPC.life = NPC.lifeMax;

                target.Y -= hoverHeight;
                target.X -= (left ? -hoverWidth : hoverWidth);
                MoveTowards(NPC, target, (distance > 300 ? speedFast : speedSlow), 30f);

                if (Main.rand.NextBool(3))
                {
                    int damageBolt = NPC.damage - 165;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X + 10 * NPC.direction, NPC.Center.Y), new Vector2(Main.rand.Next(-5, 5), -3),
                                   ProjectileType<GalacticBolt>(), damageBolt, 1);
                }
            }

            else if(!NPC.AnyNPCs(NPCType<TheStorm>()))
            {
                NPC.dontTakeDamage = false;

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
            }

            if (!player.dead && !NPC.AnyNPCs(NPCType<TheStorm>()))
            {
                Attacks(player); //All attacks are in this hook

                if (phase < 4)
                    if (NPC.ai[3] >= 5) //go back to first attack when cycle is complete
                        NPC.ai[3] = 1;
                
                if (phase == 4)
                    if (NPC.ai[3] >= 6) //go back to first attack when cycle is complete
                        NPC.ai[3] = 1;

                if (phase == 5)
                    if (NPC.ai[3] >= 7) //go back to first attack when cycle is complete
                        NPC.ai[3] = 1;

                if (phase == 6)
                    if (NPC.ai[3] >= 8) //go back to first attack when cycle is complete
                        NPC.ai[3] = 1;

                if (NPC.ai[3] <= 0) //go back to first attack when cycle is complete 
                    NPC.ai[3] = 1;
            }
        }

        private void Attacks(Player player)
        {
            int projDamage = NPC.damage / 2;

            if (phase == 1)
            {
                if (NPC.ai[3] == 1) //Attack One, Lightning
                {
                    if (NPC.ai[0] >= 10 && Main.rand.NextBool(14))
                    {
                        int damageLightning = 50;

                        float projectileSpeed = 21f;
                        Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                        Vector2 rotation = -NPC.Center + player.Center;
                        float ai = Main.rand.Next(100);
                        int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                            ModContent.ProjectileType<GalaxyBossLightning>(), damageLightning, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                        Main.projectile[projID].scale = 1;
                        howMuchLightning++;
                    }

                    if (Main.rand.NextBool(200) || howMuchLightning > 50)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        howMuchLightning = 0;

                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 2) //Attack 2, Spawn Dashers
                {
                    SpawnDashers();
                    if (dashers >= 5 || Main.rand.NextBool(100))
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                        dashers = 0;
                    }
                }

                if (NPC.ai[3] == 3) //Attack Three, Ancient Curses
                {
                    int damageCurse = 125;

                    float projectileSpeed = 21f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                    Vector2 rotation = -NPC.Center + player.Center;
                    float ai = Main.rand.Next(100);
                    int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID].scale = 1;

                    int projID_ = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID_].scale = 1;

                    NPC.ai[0] = 0;//Reset all ai values
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    numAncientCurse = 0;
                    NPC.ai[3]++;
                    NPC.localAI[0] = 0;
                    NPC.netUpdate = true;
                }

                if (NPC.ai[3] == 4) //Attack Four, Galactic Bolts
                {
                    int damageBolt = NPC.damage / 2;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X + 10 * NPC.direction, NPC.Center.Y), new Vector2(Main.rand.Next(-5, 5), -3),
                                   ModContent.ProjectileType<GalacticBolt>(), damageBolt, 1);
                    numBolts++;

                    if (numBolts > 16)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numBolts = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }
            }

            else if (phase == 2) //slightly spammier
            {
                if (NPC.ai[3] == 1) //Attack One, Lightning
                {
                    if (NPC.ai[0] >= 10 && Main.rand.NextBool(13))
                    {
                        int damageLightning = 50;

                        float projectileSpeed = 25f;
                        Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                        Vector2 rotation = -NPC.Center + player.Center;
                        float ai = Main.rand.Next(100);
                        int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                            ModContent.ProjectileType<GalaxyBossLightning>(), damageLightning, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                        Main.projectile[projID].scale = 1;
                        howMuchLightning++;
                    }

                    if (Main.rand.NextBool(200) || howMuchLightning > 50)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        howMuchLightning = 0;

                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 2) //Attack 2, Spawn Dashers
                {
                    SpawnDashers();
                    if (dashers >= 10 || Main.rand.NextBool(110))
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                        dashers = 0;
                    }
                }

                if (NPC.ai[3] == 3) //Attack Three, Ancient Curses
                {
                    int damageCurse = 125;

                    float projectileSpeed = 22f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                    Vector2 rotation = -NPC.Center + player.Center;
                    float ai = Main.rand.Next(100);
                    int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID].scale = 1.05f;
                    numAncientCurse++;

                    int projID_ = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID_].scale = 1.05f;
                    numAncientCurse++;

                    if (numAncientCurse > 4)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numAncientCurse = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 4) //Attack Four, Galactic Bolts
                {
                    int damageBolt = NPC.damage / 2;
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X + 10 * NPC.direction, NPC.Center.Y), new Vector2(Main.rand.Next(-5, 5), -3), 
                        ProjectileType<GalacticBolt>(), damageBolt, 1);
                    numBolts++;

                    if (numBolts > 16)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numBolts = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }
            }

            else if (phase == 3) //chonk
            {
                if (NPC.ai[3] == 1) //Attack One, Lightning
                {
                    if (NPC.ai[0] >= 10 && Main.rand.NextBool(13))
                    {
                        int damageLightning = 50;

                        float projectileSpeed = 25f;
                        Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                        Vector2 rotation = -NPC.Center + player.Center;
                        float ai = Main.rand.Next(100);
                        int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                            ProjectileType<GalaxyBossLightning>(), damageLightning, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                        Main.projectile[projID].scale = 2;
                        howMuchLightning++;
                    }

                    if (Main.rand.NextBool(200) || howMuchLightning > 50)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        howMuchLightning = 0;

                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 2) //Attack 2, Spawn Dashers
                {
                    SpawnDashers();
                    if (dashers >= 10 || Main.rand.NextBool(110))
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                        dashers = 0;
                    }
                }

                if (NPC.ai[3] == 3) //Attack Three, Ancient Curses
                {
                    int damageCurse = 125;

                    float projectileSpeed = 22f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                    Vector2 rotation = -NPC.Center + player.Center;
                    float ai = Main.rand.Next(100);
                    int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID].scale = 1.25f;
                    numAncientCurse++;

                    int projID_ = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID_].scale = 1.25f;
                    numAncientCurse++;

                    if (numAncientCurse > 4)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numAncientCurse = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 4) //Attack Four, Galactic Bolts
                {
                    int damageBolt = NPC.damage / 2;
                    int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X + 10 * NPC.direction, NPC.Center.Y), new Vector2(Main.rand.Next(-5, 5), -3),
                        ProjectileType<GalacticBolt>(), damageBolt, 1);
                    Main.projectile[projID].scale = 2;
                    numBolts++;

                    if (numBolts > 16)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numBolts = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }
            }

            else if (phase == 4) //Ancient Demon
            {
                if (NPC.ai[3] == 1) //Attack One, Lightning
                {
                    if (NPC.ai[0] >= 10 && Main.rand.NextBool(13))
                    {
                        int damageLightning = 50;

                        float projectileSpeed = 25f;
                        Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                        Vector2 rotation = -NPC.Center + player.Center;
                        float ai = Main.rand.Next(100);
                        int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                            ProjectileType<GalaxyBossLightning>(), damageLightning, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                        Main.projectile[projID].scale = 2;
                        howMuchLightning++;
                    }

                    if (Main.rand.NextBool(200) || howMuchLightning > 50)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        howMuchLightning = 0;

                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 2) //Attack 2, Spawn Dashers
                {
                    SpawnDashers();
                    if (dashers >= 10 || Main.rand.NextBool(110))
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                        dashers = 0;
                    }
                }

                if (NPC.ai[3] == 3) //Attack Three, Ancient Curses
                {
                    int damageCurse = 125;

                    float projectileSpeed = 22f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                    Vector2 rotation = -NPC.Center + player.Center;
                    float ai = Main.rand.Next(100);
                    int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID].scale = 1.25f;
                    numAncientCurse++;

                    int projID_ = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID_].scale = 1.25f;
                    numAncientCurse++;

                    if (numAncientCurse > 4)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numAncientCurse = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 4) //Attack Four, Galactic Bolts
                {
                    int damageBolt = NPC.damage / 2;
                    int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X + 10 * NPC.direction, NPC.Center.Y), new Vector2(Main.rand.Next(-5, 5), -3),
                        ProjectileType<GalacticBolt>(), damageBolt, 1);
                    Main.projectile[projID].scale = 2;
                    numBolts++;

                    if (numBolts > 16)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numBolts = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 5) //Attack 5, Spawn Ancient Demon
                {
                    SpawnDemon();

                    NPC.ai[0] = 0;//Reset all ai values
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 1;
                    NPC.localAI[0] = 0;
                    NPC.netUpdate = true;
                }
            }

            else if (phase == 5) //Cosmic Wyvern
            {
                if (NPC.ai[3] == 1) //Attack One, Lightning
                {
                    if (NPC.ai[0] >= 10 && Main.rand.NextBool(13))
                    {
                        int damageLightning = 50;

                        float projectileSpeed = 25f;
                        Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                        Vector2 rotation = -NPC.Center + player.Center;
                        float ai = Main.rand.Next(100);
                        int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                            ProjectileType<GalaxyBossLightning>(), damageLightning, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                        Main.projectile[projID].scale = 2;
                        howMuchLightning++;
                    }

                    if (Main.rand.NextBool(200) || howMuchLightning > 50)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        howMuchLightning = 0;

                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 2) //Attack 2, Spawn Dashers
                {
                    SpawnDashers();
                    if (dashers >= 10 || Main.rand.NextBool(110))
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                        dashers = 0;
                    }
                }

                if (NPC.ai[3] == 3) //Attack 3, Ancient Curses
                {
                    int damageCurse = 125;

                    float projectileSpeed = 22f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                    Vector2 rotation = -NPC.Center + player.Center;
                    float ai = Main.rand.Next(100);
                    int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID].scale = 1.25f;
                    numAncientCurse++;

                    int projID_ = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID_].scale = 1.25f;
                    numAncientCurse++;

                    if (numAncientCurse > 4)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numAncientCurse = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 4) //Attack 4, Spawn Wyvern
                {
                    SpawnWyvern();

                    NPC.ai[0] = 0;//Reset all ai values
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3]++;
                    NPC.localAI[0] = 0;
                    NPC.netUpdate = true;
                }

                if (NPC.ai[3] == 5) //Attack 5, Galactic Bolts
                {
                    int damageBolt = NPC.damage / 2;
                    int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X + 10 * NPC.direction, NPC.Center.Y), new Vector2(Main.rand.Next(-5, 5), -3),
                        ProjectileType<GalacticBolt>(), damageBolt, 1);
                    Main.projectile[projID].scale = 2;
                    numBolts++;

                    if (numBolts > 16)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numBolts = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 6) //Attack 6, Spawn Ancient Demon
                {
                    SpawnDemon();

                    NPC.ai[0] = 0;//Reset all ai values
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 1;
                    NPC.localAI[0] = 0;
                    NPC.netUpdate = true;
                }
            }

            else if (phase == 6) //Expert Only
            {
                if (NPC.ai[3] == 1) //Attack One, Lightning
                {
                    if (NPC.ai[0] >= 10 && Main.rand.NextBool(11))
                    {
                        int damageLightning = 75;

                        float projectileSpeed = 30f;
                        Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                        Vector2 rotation = -NPC.Center + player.Center;
                        float ai = Main.rand.Next(100);
                        int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                            ProjectileType<GalaxyBossLightning>(), damageLightning, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                        Main.projectile[projID].scale = 2;
                        howMuchLightning++;
                    }

                    if (Main.rand.NextBool(200) || howMuchLightning > 50)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        howMuchLightning = 0;

                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 2) //Attack 2, Spawn Dashers
                {
                    SpawnDashers();
                    if (dashers >= 12 || Main.rand.NextBool(110))
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                        dashers = 0;
                    }
                }

                if (NPC.ai[3] == 3) //Attack 3, Ancient Curses
                {
                    int damageCurse = 150;

                    float projectileSpeed = 22f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                    Vector2 rotation = -NPC.Center + player.Center;
                    float ai = Main.rand.Next(100);
                    int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID].scale = 1.5f;
                    numAncientCurse++;

                    int projID_ = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-perturbedSpeed.X, 0),
                        ProjectileType<GalaxySphere>(), damageCurse, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                    Main.projectile[projID_].scale = 1.5f;
                    numAncientCurse++;

                    if (numAncientCurse > 4)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numAncientCurse = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 4) //Attack 4, Spawn Wyvern
                {
                    SpawnWyvern();

                    NPC.ai[0] = 0;//Reset all ai values
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3]++;
                    NPC.localAI[0] = 0;
                    NPC.netUpdate = true;
                }

                if (NPC.ai[3] == 5) //Attack 5, Galactic Bolts
                {
                    int damageBolt = NPC.damage / 2;
                    int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X + 10 * NPC.direction, NPC.Center.Y), new Vector2(Main.rand.Next(-5, 5), -3),
                        ProjectileType<GalacticBolt>(), damageBolt, 1);
                    Main.projectile[projID].scale = 2;
                    numBolts++;

                    if (numBolts > 16)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numBolts = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 6) //Attack 6, Bluefire Sphere
                {
                    int damage = 150;

                    float projectileSpeed = 22f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                    //Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0), ProjectileType<BluefireSphere>(), damage, .5f);
                    numBalls++;

                    if (numBalls > 4)
                    {
                        NPC.ai[0] = 0;//Reset all ai values
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        numBalls = 0;
                        NPC.ai[3]++;
                        NPC.localAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                }

                if (NPC.ai[3] == 7) //Attack 7, Spawn Ancient Demon
                {
                    SpawnDemon();

                    NPC.ai[0] = 0;//Reset all ai values
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 1;
                    NPC.localAI[0] = 0;
                    NPC.netUpdate = true;
                }
            }
        }

        public static int MinionCount()
        {
            int count = 5;

            return count;
        }

        private void SpawnDashers()
        {
            int count = MinionCount();
            var entitySource = NPC.GetSource_FromAI();

            for (int i = 0; i < count; i++)
            {
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<GalacticDasher>(), NPC.whoAmI);
                dashers += 1;
                NPC minionNPC = Main.npc[index];

                // Finally, syncing, only sync on server and if the NPC actually exists (Main.maxNPCs is the index of a dummy NPC, there is no point syncing it)
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

        private void SpawnDemon()
        {
            var entitySource = NPC.GetSource_FromAI();

            int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<AncientDemon>(), NPC.whoAmI);
            NPC minionNPC = Main.npc[index];

            // Finally, syncing, only sync on server and if the NPC actually exists (Main.maxNPCs is the index of a dummy NPC, there is no point syncing it)
            if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
            {
                NetMessage.SendData(MessageID.SyncNPC, number: index);
            }
        }

        private void SpawnWyvern()
        {
            var entitySource = NPC.GetSource_FromAI();
            if (!NPC.AnyNPCs(NPCType<CosmicWyvernHead>()))
            {
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<CosmicWyvernHead>(), NPC.whoAmI);
                NPC minionNPC = Main.npc[index];

                // Finally, syncing, only sync on server and if the NPC actually exists (Main.maxNPCs is the index of a dummy NPC, there is no point syncing it)
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

        private void MoveTowards(NPC npc, Vector2 target, float speed, float turnResistance)
        {
            var move = target - npc.Center;
            float length = move.Length();
            if (length > speed)
            {
                move *= speed / length;
            }
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            length = move.Length();
            if (length > speed)
            {
                move *= speed / length;
            }
            npc.velocity = move;
        }


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (Main.masterMode)
                npcLoot.Add(ItemDropRule.Common(ItemType<GalacticPerilRelic>()));

            if (Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.BossBag(ItemType<GalacticPerilBag>()));
            }
            else
            {
                npcLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<GalacticFireRod>(), ItemType<GalaxyBow>(), ItemType<GalaxyBlade>(), ItemType<GalaxyArrowStaff>(), ItemType<GalacticGrenade>()));
                npcLoot.Add(ItemDropRule.Common(ItemType<SoulFragment>()));
            }
        }

        public override void OnKill()
        {
            Downeds.DownedGalacticPeril = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }
    }

    [AutoloadBossHead]
    public class TheStorm : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Storm");
        }

        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 77;

            NPC.boss = true;
            NPC.aiStyle = 49;
            NPC.npcSlots = 5;

            NPC.lifeMax = 50000;
            NPC.damage = 175;
            NPC.defense = 80;
            NPC.knockBackResist = 0f;

            NPC.stepSpeed = 20;

            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            if (!Main.dedServ)
            {
                //Music = MusicID.Boss2;
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/TheUniverseIsInGalacticPeril");
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * bossLifeScale);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("The raging storm of Galactic Doom, " +
                " created by the unfatomable power of the Galactic Peril")
            });
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            NPC.spriteDirection = NPC.direction;

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

            //Immunities
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Oiled] = true;
            NPC.buffImmune[BuffID.Venom] = false;
            NPC.buffImmune[BuffID.BoneJavelin] = true;
            NPC.buffImmune[BuffID.DryadsWardDebuff] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;

            NPC.ai[0]++;

            if (NPC.ai[0] >= 10 && Main.rand.NextBool(14))
            {
                float projectileSpeed = 21f;
                Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                Vector2 rotation = -NPC.Center + player.Center;
                int damageLightning = NPC.damage - 75;
                float ai = Main.rand.Next(100);
                int projID = Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, 0),
                    ModContent.ProjectileType<GalaxyBossLightning>(), damageLightning, .5f, Main.myPlayer, rotation.ToRotation(), ai);
                Main.projectile[projID].scale = 1;
            }
        }

        public override void OnKill() => Message();

        private void Message()
        {
            string key = "The Storm Clouds part";
            Color messageColor = Color.DimGray;
            if (Main.netMode == NetmodeID.Server) // Server
            {
                Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
            }
            else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
            {
                Main.NewText(Language.GetTextValue(key), messageColor);
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.Heart;
        }
    }

    public class GalacticDasher : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 22;
            NPC.height = 36;

            NPC.aiStyle = 74;
            NPC.noGravity = true;

            NPC.lifeMax = 1000;
            NPC.damage = 200;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;

            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath43;
        }

        public override bool? CanFallThroughPlatforms() => true;

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;
            float distanceX = player.Center.X - NPC.Center.X;
            float distanceY = player.Center.Y - NPC.Center.Y;
            float distance = (float)System.Math.Sqrt((double)(distanceX * distanceX + distanceY * distanceY));
            NPC.velocity.X *= 0.97f;
            NPC.velocity.Y *= 0.97f;

            //Immunities
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.OnFire3] = true;
            NPC.buffImmune[BuffID.Confused] = true;

            NPC.spriteDirection = NPC.direction;

            NPC.TargetClosest();

            NPC.spriteDirection = NPC.direction;

            if (Main.rand.NextBool(5))     //this defines how many dust to spawn
            {
                var dust2 = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.BlueCrystalShard);
                dust2.noGravity = true;
                dust2.scale = 1.5f;
                dust2.velocity *= 2;
            }
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Vortex, NPC.velocity.X, NPC.velocity.Y, 0, default, 0.5f);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("The suicide bombers of the universe, " +
                " servants of the Galactic Peril")
            });
        }
    }

    public class AncientDemon : ModNPC
    {
        int timer;
        int fireTime = 3;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            AnimationType = NPCID.RedDevil;

            NPC.width = 100;
            NPC.height = 64;

            NPC.aiStyle = 14;
            AIType = 156;
            NPC.noGravity = true;

            NPC.lifeMax = 8000;
            NPC.damage = 200;
            NPC.defense = 10;
            NPC.knockBackResist = 0f;

            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath43;
        }

        public override bool? CanFallThroughPlatforms() => true;

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;

            //Immunities
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.OnFire3] = true;
            NPC.buffImmune[BuffID.Confused] = true;

            NPC.spriteDirection = NPC.direction;

            NPC.TargetClosest();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("An ancient being from a realm long gone")
            });
        }
    }
}