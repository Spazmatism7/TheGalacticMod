using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using GalacticMod.Tiles.Bars;

namespace GalacticMod.Items.Hardmode.Amaza
{
	public class AmazaBar : ModItem
	{
		public override string Texture => GetInstance<Assets.Config.PersonalConfig>().ClassicAmaza ? base.Texture + "_Old" : base.Texture;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("I don't have any friends...");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.rare = ItemRarityID.LightPurple;
			Item.width = 30;
			Item.height = 24;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = TileType<AmazaBarPlaced>();
            Item.consumable = true;
            Item.autoReuse = true;
        }

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<AmazaBar>());
			recipe.AddIngredient(Mod, "AmazaOre", 3);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();
		}
	}
}