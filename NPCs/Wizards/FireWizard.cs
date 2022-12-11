using GalacticMod.Items.Hardmode.Mage;
using GalacticMod.Projectiles;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using System.IO;
using Terraria.GameContent.Generation;
using Terraria.Localization;
using Terraria.ModLoader.IO;

namespace GalacticMod.NPCs.Wizards
{
    public class FireWizard : ModNPC
    {
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 5;
		}

		public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 54;
            NPC.damage = 100;
            NPC.defense = 30;
            NPC.lifeMax = 500;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.knockBackResist = 0.7f;
            NPC.value = 1500f;
            NPC.npcSlots = 2f;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,

				new FlavorTextBestiaryInfoElement("A Wizard practiced in the arts of fire")
			});
		}

		bool firing;

        public override void AI()
        {
			NPC.TargetClosest();
			NPC.velocity.X *= 0.93f;

			NPC.buffImmune[BuffID.OnFire] = true;

			if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1)
			{
				NPC.velocity.X = 0f;
			}
			if (NPC.ai[0] == 0f)
			{
				NPC.ai[0] = 500f;
			}
			/*if (NPC.alpha < 255)
			{
				NPC.alpha++;
			}*/
			if (NPC.justHit)
			{
				NPC.alpha = 0;
			}
			if (NPC.ai[2] != 0f && NPC.ai[3] != 0f)
			{
				NPC.position += NPC.netOffset;
				//NPC.alpha = 255;
				SoundEngine.PlaySound(SoundID.Item8, NPC.position);
				for (int num69 = 0; num69 < 50; num69++)
				{
					int num75 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Torch, 0f, 0f, 100, default(Color), 2.5f);
					Dust dust = Main.dust[num75];
					dust.velocity *= 3f;
					Main.dust[num75].noGravity = true;
				}
				NPC.position -= NPC.netOffset;
				NPC.position.X = NPC.ai[2] * 16f - (float)(NPC.width / 2) + 8f;
				NPC.position.Y = NPC.ai[3] * 16f - (float)NPC.height;
				NPC.netOffset *= 0f;
				NPC.velocity.X = 0f;
				NPC.velocity.Y = 0f;
				NPC.ai[2] = 0f;
				NPC.ai[3] = 0f;
				SoundEngine.PlaySound(SoundID.Item8, NPC.position);
				for (int num78 = 0; num78 < 50; num78++)
				{
					int num81 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Torch, 0f, 0f, 100, default(Color), 2.5f);
					Dust dust = Main.dust[num81];
					dust.velocity *= 3f;
					Main.dust[num81].noGravity = true;
				}
			}
			NPC.ai[0] += 1f;
			if (Main.rand.NextBool(1))
			{
				if (NPC.ai[0] == 75f || NPC.ai[0] == 150f || NPC.ai[0] == 225f || NPC.ai[0] == 300f || NPC.ai[0] == 375f || NPC.ai[0] == 450f)
				{
					NPC.ai[1] = 30f;
					NPC.netUpdate = true;
				}
			}
			else if (NPC.ai[0] == 100f || NPC.ai[0] == 200f || NPC.ai[0] == 300f)
			{
				NPC.ai[1] = 30f;
				NPC.netUpdate = true;
			}
			if (NPC.ai[0] >= 650f && Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.ai[0] = 1f;
				int num87 = (int)Main.player[NPC.target].position.X / 16;
				int num88 = (int)Main.player[NPC.target].position.Y / 16;
				int num89 = (int)NPC.position.X / 16;
				int num90 = (int)NPC.position.Y / 16;
				int num91 = 20;
				int num92 = 0;
				bool flag4 = false;
				if (Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) + Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 2000f)
				{
					num92 = 100;
					flag4 = true;
				}
				while (!flag4 && num92 < 100)
				{
					num92++;
					int num93 = Main.rand.Next(num87 - num91, num87 + num91);
					int num94 = Main.rand.Next(num88 - num91, num88 + num91);
					for (int num95 = num94; num95 < num88 + num91; num95++)
					{
						if ((num95 < num88 - 4 || num95 > num88 + 4 || num93 < num87 - 4 || num93 > num87 + 4) && (num95 < num90 - 1 || num95 > num90 + 1 || num93 < num89 - 1 || num93 > num89 + 1))
						{
							bool flag5 = true;
							if (flag5 && !Collision.SolidTiles(num93 - 1, num93 + 1, num95 - 4, num95 - 1))
							{
								NPC.ai[1] = 20f;
								NPC.ai[2] = num93;
								NPC.ai[3] = num95;
								flag4 = true;
								break;
							}
						}
					}
				}
				NPC.netUpdate = true;
			}
			if (NPC.ai[1] > 0f)
			{
				NPC.ai[1] -= 1f;
				if (NPC.ai[1] == 25f)
				{
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						float num110 = 10f;
						Vector2 vector14 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
						float num111 = Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f - vector14.X + (float)Main.rand.Next(-10, 11);
						float num112 = Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f - vector14.Y + (float)Main.rand.Next(-10, 11);
						float num113 = (float)Math.Sqrt(num111 * num111 + num112 * num112);
						num113 = num110 / num113;
						num111 *= num113;
						num112 *= num113;
						int num114 = 40;
						int num115 = ModContent.ProjectileType<BadHellBlaze>();
						int num116 = Projectile.NewProjectile(null, vector14.X, vector14.Y, num111, num112, num115, num114, 0f, Main.myPlayer);
						//NPC.GetSpawnSource_ForProjectile()
						Main.projectile[num116].timeLeft = 300;
						NPC.localAI[0] = 0f;

						firing = true;
					}

					else
						firing = false;
				}
			}
			NPC.position += NPC.netOffset;
			int num119 = 1;
			if (NPC.alpha == 255)
			{
				num119 = 2;
			}
			for (int num120 = 0; num120 < num119; num120++)
			{
				if (Main.rand.Next(255) > 255 - NPC.alpha)
				{
					int num121 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + 2f), NPC.width, NPC.height, DustID.RuneWizard, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default(Color), 1.2f);
					Main.dust[num121].noGravity = true;
					Main.dust[num121].velocity.X *= 0.1f + (float)Main.rand.Next(30) * 0.01f;
					Main.dust[num121].velocity.Y *= 0.1f + (float)Main.rand.Next(30) * 0.01f;
					Dust dust = Main.dust[num121];
					dust.scale *= 1f + (float)Main.rand.Next(6) * 0.1f;
				}
			}
			if (Main.rand.NextBool(2))
			{
				int num125 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y + 2f), NPC.width, NPC.height, DustID.Torch, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, default(Color), 2f);
				Main.dust[num125].noGravity = true;
				Main.dust[num125].velocity.X *= 1f;
				Main.dust[num125].velocity.Y *= 1f;
			}
			NPC.position -= NPC.netOffset;
			return;
		}

		int npcframe = 0;

		public override void FindFrame(int frameHeight)
		{
			if (!firing)
			{
				NPC.frame.Y = npcframe * frameHeight;
				NPC.frameCounter++;
				if (NPC.frameCounter > 0)
				{
					npcframe++;
					NPC.frameCounter = 0;
				}
				if (npcframe >= 0) //Cycles through frames 0-5 when  in phase 1
				{
					npcframe = 0;
				}
			}
			if (firing)
			{
				NPC.frame.Y = npcframe * frameHeight;
				NPC.frameCounter++;
				if (NPC.frameCounter > 5)
				{
					npcframe++;
					NPC.frameCounter = 0;
				}
				if (npcframe < 1 || npcframe >= 5)
				{
					npcframe = 1;
				}
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
                return SpawnCondition.Cavern.Chance * 0.05f;

			if (Main.hardMode)
				return SpawnCondition.Underworld.Chance * .5f;

			else
                return SpawnCondition.Cavern.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			npcLoot.Add(ItemDropRule.Common(ItemType<HellsBlaze>(), 10));
			npcLoot.Add(ItemDropRule.Common(ItemType<FireWizardHat>(), 100));
		}
    }
}