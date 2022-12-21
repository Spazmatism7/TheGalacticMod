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
using GalacticMod.Assets.Config;

namespace GalacticMod.Items.Accessories.Runes
{
	public class LifeInsignia : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Provides life regeneration and reduces the cooldown of healing potions by 25%" +
                "\nGrants 100 health");
		}

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Purple;
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			//Extra Health
			//player.statLifeMax += 100; //+100 max hp/sec
			player.statLifeMax2 += 100;

			//Charm of Myths
			player.pStone = true;
			player.lifeRegen += 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<LifeInsignia>());
			recipe.AddIngredient(ItemID.CharmofMyths); //Charm of Myths
			recipe.AddIngredient(ItemID.LifeCrystal, 5); //Life Crystal
			recipe.AddIngredient(ItemID.LifeFruit, 2); //Life Fruit
			recipe.AddIngredient(Mod, "SoulFragment");
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}