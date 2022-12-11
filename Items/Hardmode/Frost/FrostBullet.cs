using GalacticMod.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Frost
{
	public class FrostBullet : ModItem
	{
		public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Reflects off tiles");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 1.5f;
			Item.rare = ItemRarityID.Pink;
			Item.shoot = ProjectileType<Projectiles.FrostBulletProjectile>();   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 16f;                  //The speed of the projectile
			Item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<FrostBullet>(), 50);
			recipe.AddIngredient(ItemID.MusketBall, 50);
			recipe.AddIngredient(Mod, "FrostBar");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}