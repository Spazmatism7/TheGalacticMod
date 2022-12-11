using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace GalacticMod.Items.Hardmode.Asteroid
{
	public class AsteroidRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 78;
			Item.height = 32;
			Item.useAnimation = 10;
			Item.useTime = 10;
			Item.mana = 7;
			Item.autoReuse = true;
			Item.damage = 50;
			Item.DamageType = DamageClass.Magic;
			Item.knockBack = 4f;
			Item.noMelee = true;
			
			Item.UseSound = SoundID.Item11;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.value = Item.buyPrice(gold: 1);
			Item.rare = ItemRarityID.LightRed;

			Item.shootSpeed = 33f;
			Item.shoot = ModContent.ProjectileType<AsteroidRifleBeam>();
		}

        public override Vector2? HoldoutOffset() => new Vector2(-2f, -2f);

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<AsteroidRifle>());
			recipe.AddIngredient(Mod, "AsteroidBar", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

    public class AsteroidRifleBeam : ModProjectile
    {
		public override void SetDefaults()
		{
			Projectile.width = 80;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 200 * 60;
			Projectile.light = 1f;
		}

		public override void AI()
		{
			Projectile.velocity.Y += Projectile.ai[0];

			Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
			}
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation += MathHelper.Pi;
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 5; i++)
			{
				var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, default, 1f);
				var dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MeteorHead, 0, 0, 130, default, 0.5f);
			}
		}
	}
}