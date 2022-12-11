using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Input;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.PostML.Galactic
{
	[AutoloadEquip(EquipType.Neck)]
	public class GalacticAmulet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Attacks can release Lightning bolts");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = 10000;
			Item.rare = ItemRarityID.Cyan;
			Item.accessory = true;
			Item.expert = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<GalacticPlayer>().GalacticAmulet = true;
		}
	}
}