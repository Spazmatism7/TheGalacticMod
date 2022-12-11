/*using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.ID;

namespace GalacticMod.Items.Accessories.Runes
{
	public class ExpertRune2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Supreme Rune of the Powerhungry");
			Tooltip.SetDefault("Grants abilities of Spore Sac and Shiny Stone");
		}

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Pink;
			Item.width = 80;
			Item.height = 80;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.sporeSac = true;
			player.shinyStone = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<ExpertRune2>());
			recipe.AddIngredient(ItemID.SporeSac);
			recipe.AddIngredient(ItemID.ShinyStone);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}*/