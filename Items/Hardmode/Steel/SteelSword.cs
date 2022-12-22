using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Steel
{
	public class SteelSword : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 82;
			Item.DamageType = DamageClass.Melee;
			Item.width = 54;
			Item.height = 54;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.crit = 2;
            Item.useStyle = ItemUseStyleID.Swing;
        }

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<SteelSword>());
			recipe.AddIngredient(Mod, "SteelBar", 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}