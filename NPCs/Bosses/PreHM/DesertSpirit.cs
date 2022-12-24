using GalacticMod.Items.PreHM.Desert;
using GalacticMod.Projectiles.Boss;
using GalacticMod.Assets.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Bestiary;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using GalacticMod.Items.Boss;

namespace GalacticMod.NPCs.Bosses.PreHM
{
	[AutoloadBossHead]
	public class DesertSpirit : ModNPC
	{
		//AI Variables
		public int sandBlastCount;
		public int sandBlastTimer;
		public int timerSB;
		public int timerSC;
		public int timerM;
		public int minions;

		public int timer;

		private int hoverHeight = 200;
		private int hoverWidth = 250;
		private int hoverWidth2 = 250;
		private int speedSlow = 20;
		private int speedFast = 25;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Spirit");
			Main.npcFrameCount[NPC.type] = 1;
			NPCID.Sets.BossBestiaryPriority.Add(Type);
		}

		public override void SetDefaults()
		{
			NPC.width = 128;
			NPC.height = 296;

			NPC.boss = true;
			NPC.aiStyle = -1;
			NPC.npcSlots = 5;
			NPC.lifeMax = 5300;
			NPC.damage = 0;
			NPC.defense = 30;
			NPC.knockBackResist = 0f;

			NPC.lavaImmune = true;
			NPC.noTileCollide = true;
			NPC.noGravity = true;

			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/TheStormySands");

            NPC.value = Item.buyPrice(gold: 20);
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * bossLifeScale);
			NPC.damage = (int)(NPC.damage * 1.3);
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
			NPC.ai[3] = 0;

			//Immunities
			NPC.buffImmune[BuffID.Confused] = true;
			NPC.buffImmune[BuffID.OnFire] = true;

			NPC.buffImmune[BuffID.Confused] = true;

			if (!player.active || player.dead || !player.ZoneDesert)
			{
				NPC.TargetClosest(false);
				player = Main.player[NPC.target];
				if (!player.active || player.dead || !player.ZoneDesert)
				{
					NPC.velocity = new Vector2(0f, 10f);
					if (NPC.timeLeft > 10)
					{
						NPC.timeLeft = 10;
					}
					return;
				}
			}

			timer++;

			if (timer <= 300)
			{
				hoverWidth2 = -hoverWidth;
			}
			if (timer >= 300)
			{
				hoverWidth2 = hoverWidth;
			}
			if (timer >= 600)
			{
				timer = 0;
			}

			Movement(target);

			NPC.ai[0]++;

			timerSB++;
			timerSC++;
			timerM++;

			if (timerSB >= 2 * 60)
			{
				float knockBack = 5f;
				int rotationVar = 180;
				bool rotationBool = false;

				if (rotationBool)
                {
					if (rotationVar >= 180)
						rotationVar = 90;
					if (rotationVar <= 90)
						rotationVar = 180;
					rotationBool = false;
                }

				if (NPC.ai[0] >= 10 && !Main.expertMode)
				{
					int damageSandball = 15;
					float numberProjectiles = 5;
					float rotation = MathHelper.ToRadians(rotationVar);

					for (int i = 0; i < numberProjectiles; i++)
					{
						float speedX = 4f;
						float speedY = 0f;
						Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles)));
						Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<SandBlast>(), damageSandball, knockBack);
						//Projectile.GetProjectileSource_FromThis()
					}
					SoundEngine.PlaySound(SoundID.Item34, NPC.Center);
					rotationBool = true;
				}

				if (Main.expertMode && NPC.ai[0] >= 10)
				{
					int damageSandball = 18;
					float numberProjectiles = 6;
					float rotation = MathHelper.ToRadians(rotationVar);

					for (int i = 0; i < numberProjectiles; i++)
					{
						float speedX = 5f;
						float speedY = 0f;
						Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles)));
						Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<SandBlast>(), damageSandball, knockBack);
						//Projectile.GetProjectileSource_FromThis()
					}
					SoundEngine.PlaySound(SoundID.Item34, NPC.Center);
					rotationBool = true;
				}

				timerSB = 0;
			}

			if (timerSC >= 5 * 60)
			{
				float projectileSpeed = 21f;
				Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));

				int damageCloud = 20;
				int projID = Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<SandCloud>(), damageCloud, .5f, 0);
				//NPC.GetSpawnSource_ForProjectile()

				timerSC = 0;
			}

			if (timerM >= 10 * 60)
			{
				SpawnMinions();

				timerM = 0;
			}
		}

		private void Movement(Vector2 target)
        {
			// hover above player
			int distance = (int)Vector2.Distance(target, NPC.Center);
			target.Y -= hoverHeight;
			target.X -= /*/(left ? -hoverWidth : hoverWidth)*/ hoverWidth2;
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

		private void SpawnMinions()
		{
			int count = 5;
			var entitySource = NPC.GetSource_FromAI();

			for (int i = 0; i < count; i++)
			{
				int type = Main.rand.Next(new int[] { NPCID.LarvaeAntlion, NPCID.FlyingAntlion, NPCID.WalkingAntlion, NPCID.TombCrawlerHead, NPCID.Vulture, NPCID.Tumbleweed });

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

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
            npcLoot.Add(ItemDropRule.Common(ItemType<DesertSpiritTrophy>(), 10));

            if (Main.expertMode)
			{
				npcLoot.Add(ItemDropRule.BossBag(ItemType<DesertSpiritBag>()));
			}
			else
			{
				if (Main.rand.NextBool(7))
				{
					//Item.NewItem(npc.getRect(), ItemType<DesertSpiritMask>());
				}

				npcLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<Items.PreHM.Desert.Sandstorm>(), ItemType<SandShotgun>(), ItemType<StormSpear>(), ItemType<LightningCaller>(), ItemType<SandWhip>()));
			}
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,

				new FlavorTextBestiaryInfoElement("A mutated Sand Elemental banished from their society for his unusual practices")
			});
		}

		public override void OnKill()
		{
			Downeds.DownedDesertSpirit = true;
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;
		}

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_24").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_25").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_26").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_26").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_27").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                }
            }
        }
    }
}