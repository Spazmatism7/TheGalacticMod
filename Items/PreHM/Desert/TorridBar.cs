using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace GalacticMod.Items.PreHM.Desert
{
	public class TorridBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Feels grainy");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.rare = ItemRarityID.Green;
			Item.width = 20;
			Item.height = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.consumable = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<TorridBar>());
			recipe.AddIngredient(Mod, "TorridOre", 3);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();
		}
	}
}