using GalacticMod.Tiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using GalacitcMod.Items;

namespace GalacticMod.Items.ThrowingClass.Armor.Steel
{
	[AutoloadEquip(EquipType.Legs)]
	public class SteelBoots : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increased Throwing Critical Strike Chance");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = Item.sellPrice(gold: 7);
			Item.rare = ItemRarityID.Pink;
			Item.defense = 15;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

		public override void UpdateEquip(Player Player)
		{
			Player.GetCritChance(DamageClass.Throwing) += 2;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<SteelBoots>());
			recipe.AddIngredient(Mod, "SteelBar", 23);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}