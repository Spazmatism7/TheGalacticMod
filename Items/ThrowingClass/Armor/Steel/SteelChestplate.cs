using GalacticMod.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacitcMod.Items;

namespace GalacticMod.Items.ThrowingClass.Armor.Steel
{
	[AutoloadEquip(EquipType.Body)]
	public class SteelChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Steel Chestplate");
			Tooltip.SetDefault("10% Increased Throwing Damage");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = Item.sellPrice(gold: 7);
			Item.rare = ItemRarityID.Pink;
			Item.defense = 25;
            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;

        }

		public override void UpdateEquip(Player Player)
		{
			Player.GetDamage(DamageClass.Throwing) += 0.1f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<SteelChestplate>());
			recipe.AddIngredient(Mod, "SteelBar", 26);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();

		}
	}
}