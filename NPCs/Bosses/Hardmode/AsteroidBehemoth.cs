using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Bestiary;
using GalacticMod.Items.Hardmode.Asteroid;
using GalacticMod.Projectiles.Boss;
using GalacticMod.Assets.Systems;
using GalacticMod.Buffs;
using Terraria.GameContent.ItemDropRules;
using GalacticMod.Items.Boss;
using GalacticMod.Items.Boss.Master;

namespace GalacticMod.NPCs.Bosses.Hardmode
{
    [AutoloadBossHead]
    public class AsteroidBehemoth : ModNPC
    {
        //AI VARIABLES
        public int servant;
        public int numFireballs;
        public int aiType = 0;
        public bool secondForm = false;

        //OLD HD VARIABLES
        private int hoverHeight = 350;
        private int hoverWidth = 600;
        private int speedSlow = 50;
        private int speedFast = 60;
        private bool left = true;
        private int distance;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 166;
            NPC.height = 138;

            NPC.boss = true;
            NPC.aiStyle = -1;
            NPC.npcSlots = 5;

            NPC.lifeMax = 35000;
            NPC.damage = 58;
            NPC.defense = 38;
            NPC.knockBackResist = 0f;

            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            if (!Main.dedServ)
            {
                Music = 83;
            }

            NPC.value = Item.buyPrice(gold: 10);
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * bossLifeScale);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Meteor,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A raging hunk of meteorite empowered by an extraterrestrial energy, and imbuned with iridium")
            });
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;
            NPC.netAlways = true;
            NPC.TargetClosest(true);

            //Immunities
            NPC.buffImmune[BuffID.BoneJavelin] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;

            if (NPC.life < NPC.life / 2 && Main.expertMode)
                secondForm = true;

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

            if (aiType == 1)
            {
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
            }
            else if (aiType == 1)
            {
                Movement(target);
            }

            NPC.ai[0]++;

            if (!player.dead)
            {
                Attacks(player); //All attacks are in this hook

                NPC.rotation = (player.MountedCenter - NPC.Center).ToRotation();

                if (NPC.ai[3] >= 4) //go back to first attack when cycle is complete 
                {
                    NPC.ai[3] = 1;
                }

                if (NPC.ai[3] <= 0) //go back to first attack when cycle is complete 
                {
                    NPC.ai[3] = 1;
                }
            }
        }

        private void Attacks(Player player)
        {
            int projDamage = 58;

            if (NPC.ai[3] == 1) //Attack One, Fireballs
            {
                if (NPC.ai[0] >= 10 && Main.rand.NextBool(14))
                {
                    float projectileSpeed = 21f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
                    int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<BehemothFireball>(), 
                        projDamage, .5f, 0);
                    Main.projectile[projID].scale = 1;
                    numFireballs++;
                    aiType = 0;
                }

                if (Main.rand.NextBool(200) || numFireballs > 14)
                {
                    NPC.ai[0] = 0;//Reset all ai values
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    numFireballs = 0;

                    NPC.ai[3]++;
                    NPC.localAI[0] = 0;
                    NPC.netUpdate = true;
                }
            }

            if (NPC.ai[3] == 2) //Attack 2, Spawn Servants
            {
                SpawnServants();
                if (servant >= 5 || Main.rand.NextBool(100))
                {
                    NPC.ai[0] = 0;//Reset all ai values
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    NPC.ai[3]++;
                    NPC.localAI[0] = 0;
                    NPC.netUpdate = true;
                    servant = 0;
                }
            }

            if (NPC.ai[3] == 3) //Attack Three, Lasers
            {
                if (NPC.ai[0] >= 10 && Main.rand.NextBool(14))
                {
                    float projectileSpeed = 30f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
                    int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<BehemothLaser>(),
                        projDamage + 10, .5f, 0);
                    Main.projectile[projID].scale = 1;
                    numFireballs++;
                    aiType = 1;
                }

                if (Main.rand.NextBool(200) || numFireballs > 20)
                {
                    NPC.ai[0] = 0;//Reset all ai values
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    numFireballs = 0;

                    NPC.ai[3]++;
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

        private void SpawnServants()
        {
            int count = MinionCount();
            var entitySource = NPC.GetSource_FromAI();

            for (int i = 0; i < count; i++)
            {
                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<AsteroidServant>(), NPC.whoAmI);
                servant += 1;
                NPC minionNPC = Main.npc[index];

                // Finally, syncing, only sync on server and if the NPC actually exists (Main.maxNPCs is the index of a dummy NPC, there is no point syncing it)
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

        private void Movement(Vector2 target)
        {
            // hover above player
            int distance = (int)Vector2.Distance(target, NPC.Center);
            target.Y -= hoverHeight;
            target.X -= left ? -hoverWidth : hoverWidth;
            MoveTowards(NPC, target, (distance > 300 ? speedFast : speedSlow), 30f);
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

        int npcframe = 0;
        public override void FindFrame(int frameHeight)//Animations
        {
            NPC.frame.Y = npcframe * frameHeight;
            NPC.frameCounter++;

            if (!secondForm) //above half health
            {
                if (NPC.frameCounter > 3)
                {
                    npcframe++;
                    NPC.frameCounter = 0;
                }
                if (npcframe >= 3) //Cycles through frames 0-3 for non attacking
                {
                    npcframe = 0;
                }
            }
            if (secondForm)
            {
                if (NPC.frameCounter > 3)
                {
                    npcframe++;
                    NPC.frameCounter = 0;
                }
                if (npcframe <= 3 || npcframe >= 7) //Cycles through frames 4-6 on last phase
                {
                    npcframe = 8;
                }
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (Main.masterMode)
                npcLoot.Add(ItemDropRule.Common(ItemType<AsteroidBehemothRelic>()));

            if (Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.BossBag(ItemType<AsteroidBehemothBag>()));
            }
            else
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<IridiumShard>(), 1 , 28, 32));
                npcLoot.Add(ItemDropRule.Common(ItemType<AsteroidBar>(), 1, 1, 5));
                npcLoot.Add(ItemDropRule.Common(ItemType<AsteroidShardItem>(), 5, 10, 20));

                npcLoot.Add(ItemDropRule.OneFromOptions(2, ItemType<IridiumBlaster>(), ItemType<IridiumLauncher>(), ItemType<IridiumSword>()));
                npcLoot.Add(ItemDropRule.OneFromOptions(5, ItemType<IridiumHelm>(), ItemType<IridiumBreastplate>(), ItemType<IridiumPants>()));
            }
        }

        public override void OnKill()
        {
            Downeds.DownedAsteroidBoss = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_42").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_43").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_44").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_45").Type, 1f);
            }
        }
    }

    public class AsteroidServant : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 7;

            NPC.width = 40;
            NPC.height = 54;

            NPC.aiStyle = 74;
            AIType = NPCID.SolarCorite;
            AnimationType = NPCID.FlyingSnake;

            NPC.lifeMax = 300;
            NPC.damage = 100;
            NPC.defense = 40;
            NPC.knockBackResist = 80f;

            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath43;
            NPC.value = Item.buyPrice(0, 0, 25, 0);

            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.OnFire3] = true;
            NPC.buffImmune[BuffID.Confused] = true;
        }

        public override bool? CanFallThroughPlatforms() => true;

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;
            float distanceX = player.Center.X - NPC.Center.X;
            float distanceY = player.Center.Y - NPC.Center.Y;
            float distance = (float)System.Math.Sqrt((double)(distanceX * distanceX + distanceY * distanceY));
            NPC.velocity.X *= 0.95f;
            NPC.velocity.Y *= 0.95f;

            NPC.spriteDirection = NPC.direction;

            NPC.TargetClosest();

            NPC.spriteDirection = NPC.direction;

            if (Main.rand.NextBool(4))
            {
                var dust2 = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.YellowStarDust);
                dust2.noGravity = true;
                dust2.scale = 1.5f;
                dust2.velocity *= 2;
            }

            if (!Main.dedServ)
            {
                Lighting.AddLight(NPC.Center, Color.WhiteSmoke.ToVector3() * 0.5f * Main.essScale);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Meteor,

                new FlavorTextBestiaryInfoElement("A species of Asteroid Heads specialized for serving their master")
            });
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit) => target.AddBuff(BuffType<AsteroidBlaze>(), 7 * 60);

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_41").Type, 1f);
            }
        }
    }
}