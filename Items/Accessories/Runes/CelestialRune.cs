using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.ID;

namespace GalacticMod.Items.Accessories.Runes
{
	public class CelestialRune : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Minor increase to damage, melee speed, critical strike chance, life regeneration, defense, mining speed, and minion knockback" +
				"\nTurns the holder into a werewolf at night and a merfolk when entering water" +
				"\nIncreases pickup range for mana stars");
		}

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.LightPurple;
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;

			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.lifeRegen += 2;
			player.statDefense += 4;
			player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
			player.GetDamage(DamageClass.Generic) += 0.1f;
			player.GetCritChance(DamageClass.Generic) += 2;
			player.pickSpeed -= 0.15f;
			player.GetKnockback(DamageClass.Summon) += 0.5f;

			player.GetDamage(DamageClass.Magic) += 0.5f;
			player.manaMagnet = true;
		}

		public void VanillaUpdateAccessory(int i, Item item, bool hideVisual, ref bool flag, ref bool flag2, ref bool flag3, Player player)
        {
			player.accMerman = true;
			player.wolfAcc = true;
			if (hideVisual)
			{
				player.hideMerman = true;
				player.hideWolf = true;
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<CelestialRune>());
			recipe.AddIngredient(ItemID.CelestialShell);
			recipe.AddIngredient(ItemID.CelestialEmblem);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}