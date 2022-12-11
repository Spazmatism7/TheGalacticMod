using GalacticMod.Tiles;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using GalacticMod.Assets.Systems;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace GalacticMod.Items.PreHM.Desert
{
	public class TorridOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Feels grainy to the touch");
			DisplayName.SetDefault("Torrid Shard");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SetDefaults()
		{
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.maxStack = 999;
			Item.width = 12;
			Item.height = 12;
			Item.rare = ItemRarityID.Green;
			Item.value = 3000;
		}
	}

	public class TorridShard : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (Downeds.DownedDesertSpirit)
			{
				if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDesert)
				{
					if (Main.rand.NextBool(7))
					{
						npcLoot.Add(ItemDropRule.Common(ItemType<TorridOre>(), 1, 2));
					}
				}
			}
		}
	}
}