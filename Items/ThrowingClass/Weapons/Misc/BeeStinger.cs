using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.Audio;

namespace GalacticMod.Items.ThrowingClass.Weapons.Misc
{
	public class BeeStinger : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Tooltip.SetDefault("Fires a bee stinger");
		}

		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.rare = ItemRarityID.Blue;
			Item.width = 24;
			Item.height = 26;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 8f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<BeeStingerP>();
			Item.DamageType = DamageClass.Throwing;
			Item.knockBack = 4.5f;

			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.autoReuse = false;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(copper: 75);
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<BeeStinger>());
			recipe.AddIngredient(ItemID.Stinger, 30);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}

	public class BeeStingerP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee Stinger");
		}

		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 20;
			//Projectile.aiStyle = 3;
			Projectile.friendly = true;
			Projectile.penetrate = 2;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.tileCollide = true;
			Projectile.light = .25f;
		}

		public override void AI()
		{
			Projectile.rotation += 1.57f / 6;
			Projectile.velocity.Y += .1f;

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

			Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();

			// Point towards where it is moving, applied offset for top right of the sprite respecting spriteDirection
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			if (Projectile.timeLeft > 3)
				Projectile.timeLeft = 3;

			return false;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Projectile.timeLeft > 3)
			{
				Projectile.timeLeft = 3;
			}
			if (Main.rand.NextBool(1))
				target.AddBuff(BuffID.Poisoned, 600);
		}
	}
}
