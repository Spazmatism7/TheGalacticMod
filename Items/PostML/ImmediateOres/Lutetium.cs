using System.IO.Pipes;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;
using GalacticMod.Tiles;
using Terraria.ID;
using GalacticMod.Tiles.Bars;

namespace GalacticMod.Items.PostML.ImmediateOres
{
	public class LutetiumBar : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Red;
			Item.width = 20;
			Item.height = 20;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = TileType<LutetiumBarPlaced>();
            Item.autoReuse = true;
			Item.consumable = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<LutetiumBar>());
			recipe.AddIngredient(Mod, "LutetiumOre", 4);
			recipe.AddTile(TileID.Autohammer); //Autohammer
			recipe.Register();
		}
	}

	public class LutetiumOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Glows with power");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.createTile = TileType<LutetiumOreT>();
			Item.width = 8;
			Item.height = 8;
			Item.value = 3000;
			Item.rare = ItemRarityID.Red;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = ItemUseStyleID.Swing;
		}
	}
}
