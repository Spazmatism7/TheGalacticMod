using GalacticMod.Tiles;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace GalacticMod.Items.Accessories
{
	[AutoloadEquip(EquipType.Shield)]
	public class RoyalShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants immunity to knockback" +
                "\nGrants 2 Defense");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.statDefense += 2;
			player.noKnockback = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<RoyalShield>());
			recipe.AddIngredient(ItemID.GoldBar, 20);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}