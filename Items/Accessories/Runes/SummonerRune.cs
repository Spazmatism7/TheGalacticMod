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
	public class SummonerRune : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("30% increased summon damage" +
				"\nIncreases your max number of minions by 2" +
				"\nIncreases the knockback of your minions");
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
			player.GetDamage(DamageClass.Summon) += 0.15f;

			//Pygmy Necklace
			player.maxMinions++;

			//Papyrus Scarab
			//player.minionKB += 2f;
			player.GetDamage(DamageClass.Summon) += 0.15f;
			player.maxMinions++;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<SummonerRune>());
			recipe.AddIngredient(ItemID.SummonerEmblem); //Summoner Emblem
			recipe.AddIngredient(ItemID.PygmyNecklace); //Pygmy Necklace
			recipe.AddIngredient(ItemID.PapyrusScarab); //Papyrus Scarab
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}