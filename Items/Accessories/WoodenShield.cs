using GalacticMod.Tiles;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace GalacticMod.Items.Accessories
{
	[AutoloadEquip(EquipType.Shield)]
	public class WoodenShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants 2 Defense");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = 10000;
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.statDefense += 2;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<WoodenShield>());
			recipe.AddIngredient(ItemID.Wood, 20);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}