using GalacticMod.Assets.Systems;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace GalacticMod.Items.Accessories
{
	[AutoloadEquip(EquipType.Shield)]
	public class HolyGuard : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants immunity to knockback, fire blocks, and many debuffs" +
				"\nGrants 6 Defense" +
				"\nGrants ability to dash" +
				"\nGives a chance to dodge attacks");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = 10000;
			Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.statDefense += 6;
			player.noKnockback = true;
			player.blackBelt = true;
			//player.GetModPlayer<GalacticPlayer>().modDash = 1;
			player.dashType = 5;
			player.buffImmune[46] = true;
			player.fireWalk = true;
			player.buffImmune[33] = true;
			player.buffImmune[36] = true;
			player.buffImmune[30] = true;
			player.buffImmune[20] = true;
			player.buffImmune[32] = true;
			player.buffImmune[31] = true;
			player.buffImmune[35] = true;
			player.buffImmune[23] = true;
			player.buffImmune[22] = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<HolyGuard>());
			recipe.AddIngredient(Mod, "RoyalShield");
			recipe.AddIngredient(ItemID.Tabi);
			recipe.AddIngredient(ItemID.BlackBelt);
			recipe.AddIngredient(ItemID.AnkhShield);
			recipe.AddIngredient(ItemID.Ectoplasm, 20);
			recipe.AddIngredient(ItemID.ShroomiteBar, 3);
			recipe.AddIngredient(ItemID.MeteoriteBar, 7);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.Register();
		}
	}
}