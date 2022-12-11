using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using GalacticMod.Tiles.Bars;

namespace GalacticMod.Items.Hardmode.Frost
{
	public class FrostBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Brrrr");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.rare = ItemRarityID.Pink;
			Item.width = 20;
			Item.height = 20;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = TileType<FrostBarPlaced>();
            Item.consumable = true;
            Item.autoReuse = true;
        }

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<FrostBar>());
			recipe.AddIngredient(Mod, "FrostOre", 4);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();
		}
	}
}