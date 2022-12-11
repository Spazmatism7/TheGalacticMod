using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Rarities;
using GalacticMod.Tiles.Bars;

namespace GalacticMod.Items.PostML.Hellfire
{
	public class HellfireBar : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 999;
			Item.value = 100;
			Item.rare = ModContent.RarityType<HellfireRarity>();
            Item.useAnimation = 15;
            Item.useTime = 10;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = ModContent.TileType<HellfireBarPlaced>();
            Item.consumable = true;
            Item.autoReuse = true;
        }
	}
}