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
	public class SorcererRune : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("15% increased magic damage" +
				"\nIncreases pickup range for mana stars" +
				"\nRestores mana when damaged" +
                "\nIncreases maximum mana by 20" +
				"\n8% reduced mana usage" +
				"\nAutomatically use mana potions when needed");
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
			player.GetDamage(DamageClass.Magic) += 0.15f;

			//Celestial Cuffs
			player.manaMagnet = true;
			player.magicCuffs = true;

			//Mana Flower
			player.manaFlower = true;
			player.manaCost -= 0.08f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<SorcererRune>());
			recipe.AddIngredient(ItemID.SorcererEmblem);
			recipe.AddIngredient(ItemID.CelestialCuffs);
			recipe.AddIngredient(ItemID.ManaFlower);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();

            recipe = Recipe.Create(ItemType<SorcererRune>());
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddIngredient(ItemID.MagicCuffs);
            recipe.AddIngredient(ItemID.MagnetFlower);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

            recipe = Recipe.Create(ItemType<SorcererRune>());
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddIngredient(ItemID.MagicCuffs);
            recipe.AddIngredient(ItemID.ManaFlower);
            recipe.AddIngredient(ItemID.CelestialMagnet);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
	}
}