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
	public class WarriorRune : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("25% increased melee damage" +
				"\nIncreases melee knockback and inflicts fire damage on attack" +
				"\n10% increased melee speed");
		}

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.LightRed;
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			//Emblem
			player.GetDamage(DamageClass.Melee) += 0.15f;

			//Fire Gauntlet
			player.kbGlove = true;
			player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
			player.GetDamage(DamageClass.Melee) += 0.1f;
			player.magmaStone = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<WarriorRune>());
			recipe.AddIngredient(ItemID.WarriorEmblem); //Warrior Emblem
			recipe.AddIngredient(ItemID.FireGauntlet); //Fire Gauntlet
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}