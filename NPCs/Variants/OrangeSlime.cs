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
using GalacticMod.Assets.Config;
using GalacticMod.Items.PostML.Hellfire;

namespace GalacticMod.NPCs.Variants
{
	public class OrangeSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 2;
		}

		public override void SetDefaults()
		{
			NPC.width = 29;
			NPC.height = 22;
			NPC.damage = 13;
			NPC.defense = 4;
			NPC.lifeMax = 37;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 1;
			AnimationType = 1;
			AIType = 1;
			Banner = Item.NPCtoBanner(1);
			BannerItem = Item.BannerToItem(Banner);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (!GetInstance<GalacticModConfig>().NoWeirdoBlobs)
			{
				return SpawnCondition.OverworldDaySlime.Chance * 0.1f;
			}
			else
			{
				return SpawnCondition.OverworldDaySlime.Chance * 0f;
			}
		}

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.SlimeStaff, 10000));
        }
    }
}