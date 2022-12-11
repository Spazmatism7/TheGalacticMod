using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Items.PreHM.Star;
using Terraria.ModLoader.Utilities;

namespace GalacticMod.NPCs.Sky
{
	public class EnragedStar : ModNPC
	{
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;

            NPC.width = 22;
            NPC.height = 24;

            NPC.aiStyle = 86;
            NPC.noGravity = true;
            AnimationType = NPCID.FlyingSnake;

            NPC.lifeMax = 100;
            NPC.damage = 35;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;

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
            NPC.velocity.X *= 0.97f;
            NPC.velocity.Y *= 0.97f;

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
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

				new FlavorTextBestiaryInfoElement("A star imbuned with pure rage")
			});
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)].ZoneSkyHeight)
				return SpawnCondition.Sky.Chance * 0.25f;
            if (NPC.AnyNPCs(NPCType<EnragedStar>()))
                return SpawnCondition.Sky.Chance * 5f;
            else
				return SpawnCondition.Sky.Chance * 0f;
        }

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemType<Stardust>(), 1, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.FallenStar, 2));
        }
	}
}
