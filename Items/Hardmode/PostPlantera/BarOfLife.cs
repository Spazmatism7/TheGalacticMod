using System.IO.Pipes;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using GalacticMod.Tiles.Bars;

namespace GalacticMod.Items.Hardmode.PostPlantera
{
	public class BarOfLife : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("The purest of metals, protected by the Jungle's guardian");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.rare = ItemRarityID.Lime;
			Item.width = 30;
			Item.height = 20;
			Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = TileType<BarofLifePlaced>();
            Item.consumable = true;
            Item.autoReuse = true;
        }
	}

	public class LifeBarDrop : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.type == NPCID.Plantera && !Main.expertMode)
			{
				npcLoot.Add(ItemDropRule.Common(ItemType<BarOfLife>(), 1, 10, 10));
			}
		}
	}
}