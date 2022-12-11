using System.IO.Pipes;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using GalacticMod.Tiles.Bars;

namespace GalacticMod.Items.Hardmode.Brightcore
{
	public class Brightcore : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Glows with Power");
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.rare = ItemRarityID.LightPurple;
			Item.width = 30;
			Item.height = 20;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = TileType<BrightcorePlaced>();
            Item.consumable = true;
            Item.autoReuse = true;
        }

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<Brightcore>());
			recipe.AddIngredient(ItemID.CrystalShard, 3);
            recipe.AddIngredient(ItemID.SoulofLight);
            recipe.AddRecipeGroup("GalacticMod:AdamantiteBar");
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();
		}
	}
}