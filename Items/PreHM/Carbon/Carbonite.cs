using GalacticMod.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Carbon
{
	public class Carbonite : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.createTile = TileType<CarboniteT>();
			Item.width = 8;
			Item.height = 8;
			Item.value = 3000;
			Item.rare = ItemRarityID.Orange;
		}
	}
}