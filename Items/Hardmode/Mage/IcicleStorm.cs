using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Audio;

namespace GalacticMod.Items.Hardmode.Mage
{
	public class IcicleStorm : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 45;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 12;
			Item.width = 26;
			Item.height = 26;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.knockBack = 8;
			Item.value = Item.sellPrice(silver: 50);
			Item.rare = ItemRarityID.LightRed;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item9;
			Item.shootSpeed = 13f;
			Item.shoot = ProjectileType<IcicleDart>();
		}
	}

	public class IcicleDart : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 28;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = true;
			Projectile.light = 1f;
			Projectile.timeLeft = 100;
		}

		public override void AI()
		{
			Projectile.velocity.Y += Projectile.ai[0];

			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);

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
			for (int i = 0; i < 16; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, 16);
				d.frame.Y = 0;
				d.velocity *= 2;
			}

			for (int i = 0; i < 16; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, 56);
				d.frame.Y = 0;
				d.velocity *= 2;
			}

			Vector2 perturbedSpeed = new Vector2(0, -7).RotatedByRandom(MathHelper.ToRadians(360));
			Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<IceChunks>(), Projectile.damage - 10, 0, Projectile.owner);
			//Projectile.GetProjectileSource_FromThis()
		}
	}

	public class IceChunks : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = 1;
			Projectile.light = 0.1f;
			Projectile.friendly = true;
			Projectile.timeLeft = 3;
			Projectile.penetrate = -1;
			Projectile.tileCollide = true;
			Projectile.scale = 1f;
			Projectile.knockBack = 0;
			Projectile.aiStyle = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		public override void AI()
		{
			Projectile.velocity.X = 0;
			Projectile.velocity.Y = 0;
			Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Cloud, Projectile.velocity.X * 1f, Projectile.velocity.Y * .75f, 130, default, 1.5f);

			if (Main.rand.NextBool(1))     //this defines how many dust to spawn
			{
				int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueFairy, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);

				Main.dust[dust].noGravity = true; //this make so the dust has no gravity
				Main.dust[dust].velocity *= 0.1f;
				int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueFairy, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);
			}
			if (Projectile.owner == Main.myPlayer && Projectile.timeLeft == 3)
			{
				Projectile.velocity.X = 0f;
				Projectile.velocity.Y = 0f;
				Projectile.tileCollide = false;

				// change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
				Projectile.position = Projectile.Center;

				Projectile.width = 200;
				Projectile.height = 200;
				Projectile.Center = Projectile.position;
				Projectile.knockBack = 6f;
			}
		}

		public override bool? CanDamage()
		{
			if (Projectile.timeLeft > 3)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item, Projectile.position);

			for (int i = 0; i < 50; i++)
			{
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Cloud);
				dust.noGravity = true;
				dust.scale = 2f;
				dust.velocity *= 2.5f;
				dust.fadeIn = 1f;
			}

			for (int i = 0; i < 50; i++)
			{
				float speedY = -2f;

				Vector2 perturbedSpeed = new Vector2(0, speedY).RotatedByRandom(MathHelper.ToRadians(360));
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.BlueFairy, perturbedSpeed.X, perturbedSpeed.Y);

				dust.noGravity = true;
				dust.scale = 1.5f;
				dust.velocity *= 2f;
				dust.fadeIn = 1f;
			}

			for (int i = 0; i < 50; i++)
			{
				Dust dust;
				Vector2 position = Projectile.position;
				dust = Main.dust[Terraria.Dust.NewDust(position, Projectile.width, Projectile.height, DustID.Cloud, 0f, 0f, 0, default, 1f)];
				dust.noGravity = true;
				dust.scale = 1f;
			}
		}
	}
}