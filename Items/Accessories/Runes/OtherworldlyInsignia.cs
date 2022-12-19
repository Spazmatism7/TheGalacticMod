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
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.Accessories.Runes
{
	public class OtherworldlyInsignia : ModItem
	{
		public override string Texture => GetInstance<PersonalConfig>().NoEpilepsy ? base.Texture + "_NoEpilepsy" : base.Texture;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases pickup range for mana stars" +
                "\nGrants 4 defense" +
				"\n25% increased melee and ranged damage" +
				"\nIncreases melee knockback and inflicts fire damage on attack" +
				"\n10% increased melee speed" +
				"\nIncreases view range for guns (Right click to zoom out)" +
				"\n10% increased ranged critical strike chance" +
				"\nIncreases arrow damage by 10% and greatly increases arrow speed" +
				"\n20% chance to not consume arrows" +
                "\nLights Wooden Arrows ablaze" +
                "\n15% increased magic damage and 8% reduced mana usage" +
				"\nRestores mana when damaged" +
				"\nIncreases maximum mana by 20" +
				"\nAutomatically use mana potions when needed" +
				"\n30% increased summon damage" +
				"\nIncreases your max number of minions by 2" +
                "\nUsing weapons rains fire towards the cursor");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 2));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation
        }

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Purple;
			Item.width = 36;
			Item.height = 44;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
        }

		public override void UpdateEquip(Player player)
		{
			Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.2f, 0.8f, 0.9f); //Glow

            //Fire Totem
            player.GetModPlayer<GalacticPlayer>().fireTotem = true;

			//Warrior Rune
			//Emblem
			player.GetDamage(DamageClass.Melee) += 0.15f;

			//Fire Gauntlet
			player.kbGlove = true;
			player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
			player.GetDamage(DamageClass.Melee) += 0.1f;
			player.magmaStone = true;

			//Ranger Rune
			//Emblem
			player.GetDamage(DamageClass.Ranged) += 0.15f;

			//Sniper Scope
			player.scope = true;
			player.GetCritChance(DamageClass.Ranged) += 1;
			player.GetDamage(DamageClass.Ranged) += 0.1f;

			//Molten Quiver
			player.magicQuiver = true;
			player.arrowDamage += 0.1f;
            player.hasMoltenQuiver = true;

            //Sorcerer Rune
            //Emblem
            player.GetDamage(DamageClass.Magic) += 0.15f;

			//Celestial Cuffs
			player.manaMagnet = true;
			player.magicCuffs = true;

			//Mana Flower
			player.manaFlower = true;
			player.manaCost -= 0.08f;

			//Summoner Rune
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
			Recipe recipe = Recipe.Create(ItemType<OtherworldlyInsignia>());
			recipe.AddIngredient(Mod, "SoulFragment");
			recipe.AddIngredient(Mod, "WarriorRune");
			recipe.AddIngredient(Mod, "RangerRune");
			recipe.AddIngredient(Mod, "SorcererRune");
			recipe.AddIngredient(Mod, "SummonerRune");
            recipe.AddIngredient(Mod, "FireTotem");
            recipe.AddTile(Mod, "Infinity");
			recipe.Register();
		}
	}
}