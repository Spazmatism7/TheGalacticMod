using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using GalacticMod.Items.PostML.Celestial;

namespace GalacticMod.Items.Accessories
{
	[AutoloadEquip(EquipType.Wings)]
	public class ConstellationWeavers : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Insane Speed!!" +
				"\nAllows flight and slow fall" +
				"\nGrants extra mobility on ice" +
				"\nProvides the ability to walk on water, honey & lava" +
				"\nGrants immunity to fire blocks and 7 seconds of immunity to lava" +
				"\nIncreases jump speed" +
				"\nIncreases fall resistance");
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(225, 8.5f, 2.2f);
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Red;
			Item.width = 46;
			Item.height = 36;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.jumpSpeedBoost += 2.4f;
			player.extraFall += 15;

			player.waterWalk = true;
			player.fireWalk = true;
			player.lavaMax += 420;
			player.accRunSpeed = 12f;
			player.moveSpeed += 0.75f;
			player.iceSkate = true;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 2f;
			ascentWhenRising = 0.3f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 2.2f;
			constantAscend = 0.15f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<ConstellationWeavers>());
			recipe.AddIngredient(Mod, "HolyWeavers");
			recipe.AddRecipeGroup("GalacticMod:Wings");
			recipe.AddIngredient(Mod, "GalaxyFragment", 8);
			recipe.AddIngredient(ItemID.LunarBar, 16);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}