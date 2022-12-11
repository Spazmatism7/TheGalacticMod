using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace GalacticMod.Items.ThrowingClass.Accessories
{
	public class KnifeSheath : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("10% chance not to consume ammo");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 38;
			Item.value = 10000;
			Item.rare = ItemRarityID.White;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.huntressAmmoCost90 = true;
		}
	}
}