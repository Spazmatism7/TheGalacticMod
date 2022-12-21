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
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.Accessories.Runes
{
	public class WarriorRune : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increases melee knockback and size" +
                "\n27% increased melee damage and speed" +
                "\nEnables auto swing for melee weapons" +
                "\nMelee attacks inflict fire, frostburn, shadowflame, and poison");
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

			//Elemental Gauntlet
			player.kbGlove = true;
			player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
			player.GetDamage(DamageClass.Melee) += 0.1f;
			player.magmaStone = true;
            player.GetModPlayer<GalacticPlayer>().shadowflame = true;
            player.GetModPlayer<GalacticPlayer>().elementalGauntlet = true;
        }

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<WarriorRune>());
			recipe.AddIngredient(ItemID.WarriorEmblem);
			recipe.AddIngredient(Mod, "ElementalGauntlet");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}