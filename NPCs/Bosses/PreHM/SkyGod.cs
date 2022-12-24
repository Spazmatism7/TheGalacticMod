using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Bestiary;
using GalacticMod.Items.PreHM.Star;
using GalacticMod.Projectiles.Boss;
using GalacticMod.Assets.Systems;
using Terraria.GameContent.ItemDropRules;
using GalacticMod.Items.Boss;

namespace GalacticMod.NPCs.Bosses.PreHM
{
    [AutoloadBossHead]
    public class SkyGod : ModNPC
    {
        int timerF;
        int timerS;
        int timerM;
        int minions;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.width = 196;
            NPC.height = 72;
            NPC.scale = 1.5f;

            NPC.boss = true;
            NPC.aiStyle = -1;
            NPC.npcSlots = 5;

            NPC.lifeMax = 3900;
            NPC.damage = 34;
            NPC.defense = 10;
            NPC.knockBackResist = 0f;

            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            if (!Main.dedServ)
            {
                Music = 70;
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
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("The undefeated God of the Heavens, from him, the stars were born")
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

            if (!player.dead)
            {
                Attacks(player); //All attacks are in this hook

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
            int projDamage = 25;
            timerF++;
            timerS++;
            timerM++;

            if (timerF > 6) //Attack One, Feather Spreads
            {
                float projectileSpeed = 21f;
                Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));

                int projIDl = Projectile.NewProjectile(null, new Vector2(NPC.Left.X + (NPC.width / 16), NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<SkyFeather>(),
                    projDamage, .5f, 0);
                int projIDr = Projectile.NewProjectile(null, new Vector2(NPC.Right.X - (NPC.width / 16), NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<SkyFeather>(),
                   projDamage, .5f, 0);

                Main.projectile[projIDl].scale = 1;
                Main.projectile[projIDr].scale = 1;

                timerF = 0;
            }

            if (timerS > 45)
            {
                float projectileSpeed = 10f;
                Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
                int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<SolarFlare>(),
                    projDamage, .5f, 0);
                Main.projectile[projID].scale = 1;

                timerS = 0;
            }

            if (timerM > 45)
            {
                SpawnMinions();

                minions = 0;
                timerM = 0;
            }

            /*if (NPC.ai[3] == 2) //Attack Two, Sun Bomb
            {
                sunTimer++;
                int sunCooldown = 4;

                if (sunTimer > sunCooldown && numSunBomb < 2)
                {
                    float projectileSpeed = 10f;
                    Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));
                    int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<SolarFlare>(),
                        projDamage, .5f, 0);
                    Main.projectile[projID].scale = 1;
                    numSunBomb++;

                    sunTimer = 0;
                }

                if (Main.rand.NextBool(4) && timer >= (sunCooldown * numSunBomb) + 4 || numSunBomb >= 2 && timer >= (sunCooldown * numSunBomb) + 4)
                {
                    NPC.ai[0] = 0;//Reset all ai values
                    NPC.ai[1] = 0;
                    NPC.ai[2] = 0;
                    numSunBomb = 0;
                    timer = 0;
                    sunTimer = 0;

                    NPC.ai[3]++;
                    NPC.localAI[0] = 0;
                    NPC.netUpdate = true;
                }
            }*/
        }

        private void SpawnMinions()
        {
            int count = 5;
            var entitySource = NPC.GetSource_FromAI();

            for (int i = 0; i < count; i++)
            {
                int type = NPCType<Skyling>();

                int index = NPC.NewNPC(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, type, NPC.whoAmI);
                minions += 1;
                NPC minionNPC = Main.npc[index];

                // Finally, syncing, only sync on server and if the NPC actually exists (Main.maxNPCs is the index of a dummy NPC, there is no point syncing it)
                if (Main.netMode == NetmodeID.Server && index < Main.maxNPCs)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: index);
                }
            }
        }

        int npcframe = 0;
        public override void FindFrame(int frameHeight)//Animations
        {
            NPC.frame.Y = npcframe * frameHeight;

            //NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter > 4)
            {
                npcframe++;
                NPC.frameCounter = 0;
            }
            if (npcframe >= 4)
            {
                npcframe = 0;
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (!Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<SkyEssence>(), 1, 14, 21));
                npcLoot.Add(ItemDropRule.OneFromOptions(2, ItemType<Cepheus>(), ItemType<Cassiopeia>(), ItemType<Pyxis>(), ItemType<Orion>(), ItemType<Sagitta>(), ItemType<CanesVenatici>(), ItemType<StarCaller>()));
                npcLoot.Add(ItemDropRule.OneFromOptions(2, ItemType<StarHelmet>(), ItemType<StarChestplate>(), ItemType<StarLeggings>()));
            }
            else
            {
                npcLoot.Add(ItemDropRule.BossBag(ItemType<SkyGodBag>()));
            }
        }

        public override void OnKill()
        {
            Downeds.DownedSkyGod = true;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_38").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_39").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_40").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                }
            }
        }
    }

    public class Skyling : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;

            NPC.width = 92;
            NPC.height = 74;
            NPC.npcSlots = 0f;

            NPC.aiStyle = 14;
            AIType = NPCID.Harpy;
            AnimationType = NPCID.FlyingSnake;

            NPC.lifeMax = 5;
            NPC.damage = 7;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;

            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath43;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
        }

        public override bool? CanFallThroughPlatforms() => true;

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

                new FlavorTextBestiaryInfoElement("The eternal servants of the Sky God")
            });
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_19").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_20").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                }
            }
        }
    }
}