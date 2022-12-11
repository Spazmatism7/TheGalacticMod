using System.IO.Pipes;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using GalacticMod.Tiles.Bars;

namespace GalacticMod.Items.Hardmode.Storm
{
	public class StormBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Pink;
			Item.width = 30;
			Item.height = 24;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = TileType<StormBarPlaced>();
            Item.consumable = true;
            Item.autoReuse = true;
        }

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<StormBar>());
			recipe.AddIngredient(Mod, "StormOre", 4);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();
		}
	}
}