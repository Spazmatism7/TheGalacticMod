using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace GalacticMod.Items.ThrowingClass.Weapons.Knives
{
	public class AsteroidKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 54;
			Item.rare = ItemRarityID.LightPurple;
			Item.width = 18;
			Item.height = 32;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<AsteroidKnifeP>();
			Item.shootSpeed = 8f;
			Item.DamageType = DamageClass.Throwing;

			// Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools
			Item.noUseGraphic = true;
			Item.consumable = true;
			Item.maxStack = 999;
			Item.noMelee = true;
			Item.autoReuse = true;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(silver: 5);
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<AsteroidKnife>(), 25);
			recipe.AddIngredient(ItemID.ThrowingKnife, 25);
			recipe.AddIngredient(Mod, "AsteroidBar");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class AsteroidKnifeP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Asteroid Knife");
		}

		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 600;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.light = 1f;
		}

		public override void AI()
		{
			Projectile.rotation += 1.57f / 6;
			Projectile.velocity.Y += .1f;

			Lighting.AddLight(Projectile.Center, Color.Orange.ToVector3() * 0.78f);
		}

        public override void Kill(int timeLeft)
        {
			int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.MeteorHead, 0f, 0f, 100, default, 1f);
			Main.dust[dustIndex].scale = 1f + Main.rand.Next(5) * 0.1f;
			Main.dust[dustIndex].noGravity = true;
			int dustIndex2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 1f);
			Main.dust[dustIndex2].scale = 1f + Main.rand.Next(5) * 0.1f;
			Main.dust[dustIndex2].noGravity = true;
		}
    }
}