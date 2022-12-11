using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria;

namespace GalacticMod.Items.PostML.Celestial
{
	public class GalaxyFragment : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
			Tooltip.SetDefault("A fragment of the Universe");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 22;
			Item.maxStack = 999;
			Item.value = 100;
			Item.rare = ItemRarityID.Red;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<GalaxyFragment>());
			recipe.AddIngredient(ItemID.FragmentSolar);
			recipe.AddIngredient(ItemID.FragmentNebula);
			recipe.AddIngredient(ItemID.FragmentStardust);
			recipe.AddIngredient(ItemID.FragmentVortex);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}