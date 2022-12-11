using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Tiles.Bars;

namespace GalacticMod.Items.Hardmode.Asteroid
{
	public class AsteroidBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("The gift of the heavens");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 24;
			Item.maxStack = 999;
			Item.rare = ItemRarityID.LightRed;
            Item.useAnimation = 15;
            Item.useTime = 10;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = TileType<AsteroidBarPlaced>();
            Item.consumable = true;
            Item.autoReuse = true;
        }
	}
}
