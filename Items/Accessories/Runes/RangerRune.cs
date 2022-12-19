using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using GalacticMod.Items.Accessories;
using GalacticMod.Items.CraftingStations;
using Microsoft.Xna.Framework.Input;

namespace GalacticMod.Items.Accessories.Runes
{
	public class RangerRune : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("25% increased ranged damage" +
				"\nIncreases view range for guns (Right click to zoom out)" +
				"\n10% increased ranged critical strike chance" +
				"\nIncreases arrow damage by 10% and greatly increases arrow speed" +
				"\n20% chance to not consume arrows" +
                "\nLights Wooden Arrows ablaze");
		}

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.LightRed;
			Item.width = 20;
			Item.height = 20;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
			Item.accessory = true;
		}

		public override void UpdateEquip(Player player)
		{
			//Emblem
			player.GetDamage(DamageClass.Ranged) += 0.15f;

			//Sniper Scope
			player.scope = true;
			player.GetCritChance(DamageClass.Ranged) += 1;
			player.GetDamage(DamageClass.Ranged) += 0.1f;

			//Magic Quiver
			player.magicQuiver = true;
			player.arrowDamage += 0.1f;
            player.hasMoltenQuiver = true;
        }

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<RangerRune>());
			recipe.AddIngredient(ItemID.RangerEmblem); //Ranger Emblem
			recipe.AddIngredient(ItemID.SniperScope); //Sniper Scope
			recipe.AddIngredient(ItemID.MoltenQuiver); //Magic Quiver
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}