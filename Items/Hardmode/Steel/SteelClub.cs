using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Steel
{
	public class SteelClub : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 85;
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 48;
			Item.knockBack = 12;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.crit = 6;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 30;
			Item.useTime = 30;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<SteelClub>());
			recipe.AddIngredient(Mod, "SteelBar", 14);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}