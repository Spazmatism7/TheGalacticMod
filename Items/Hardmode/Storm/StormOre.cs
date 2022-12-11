using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Storm
{
	public class StormOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Zap Zap");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.createTile = TileType<Tiles.StormOreT>();
			Item.width = 16;
			Item.height = 16;
			Item.useTime = 15;
			Item.value = 3000;
			Item.rare = ItemRarityID.Pink;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
		}
	}
}