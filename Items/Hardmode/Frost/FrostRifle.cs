using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Frost
{
	public class FrostRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Brrr");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 77;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 60;
			Item.height = 28;
			Item.useTime = 33;
			Item.useAnimation = 33;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder; 
			Item.shootSpeed = 50f;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<FrostRifle>());
			recipe.AddIngredient(Mod, "FrostBar", 18);
			recipe.AddIngredient(ItemID.Musket);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();

            recipe = Recipe.Create(ItemType<FrostRifle>());
            recipe.AddIngredient(Mod, "FrostBar", 18);
            recipe.AddIngredient(ItemID.TheUndertaker);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
	}
}