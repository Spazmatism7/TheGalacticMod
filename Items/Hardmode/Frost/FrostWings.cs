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

namespace GalacticMod.Items.Hardmode.Frost
{
	[AutoloadEquip(EquipType.Wings)]
	public class FrostWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Allows flight and slow fall");
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(160, 8.5f, 2.2f);
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = 10000;
			Item.accessory = true;
			Item.rare = ItemRarityID.Pink;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
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
			Recipe recipe = Recipe.Create(ModContent.ItemType<FrostWings>());
			recipe.AddIngredient(ModContent.ItemType<FrostBar>(), 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
