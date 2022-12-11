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
using GalacticMod.Assets.Rarities;

namespace GalacticMod.Items.PostML.Hellfire
{
	[AutoloadEquip(EquipType.Wings)]
	public class HellfireWings : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Allows flight and slow fall");
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(275, 8.5f, 2.2f);
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;
			Item.value = 10000;
			Item.accessory = true;
			Item.rare = ModContent.RarityType<HellfireRarity>();
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
	}
}
