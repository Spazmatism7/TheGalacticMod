using System.IO.Pipes;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria;
using Terraria.GameContent.Creative;
using GalacticMod.Tiles.Bars;
using GalacticMod.Assets.Rarities;

namespace GalacticMod.Items.Hardmode.Ancient
{
	public class AncientBar : ModItem
	{
		public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Used to craft legacy items");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 99;
			Item.rare = RarityType<LegacyRarity>();
			Item.width = 20;
			Item.height = 20;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = TileType<AncientBarPlaced>();
            Item.consumable = true;
            Item.autoReuse = true;
        }
	}
}
