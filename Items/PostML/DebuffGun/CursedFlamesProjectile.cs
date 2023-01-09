using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using GalacticMod.Assets.Systems;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Projectiles;
using Terraria.Audio;

namespace GalacticMod.Items.PostML.DebuffGun
{
    public class CursedFlamesProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Flames Bolt");
        }

        public override void SetDefaults()
        {
            Projectile.height = 14;
            Projectile.width = 26;
            Projectile.aiStyle = -1;
            Projectile.light = 1f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.extraUpdates = 2;
			Projectile.penetrate = 2;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.LimeGreen.ToVector3() * 0.78f);

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

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.localNPCImmunity[target.whoAmI] = -1;
            target.immune[Projectile.owner] = 0;
            target.AddBuff(BuffID.CursedInferno, 60 * 30);

            Vector2 perturbedSpeed = new Vector2(0, -7).RotatedByRandom(MathHelper.ToRadians(360));
            int terraSplosion = Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<CursedBoom>(), damage, 0, Projectile.owner);
            Main.projectile[terraSplosion].timeLeft *= 2;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 16; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 74);
                d.frame.Y = 0;
                d.velocity *= 2;
            }
            Vector2 perturbedSpeed = new Vector2(0, -7).RotatedByRandom(MathHelper.ToRadians(360));
            Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<CursedBoom>(), Projectile.damage, 0, Projectile.owner);
        }
    }

	public class CursedBoom : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.aiStyle = 1;
			Projectile.light = 0.1f;
			Projectile.friendly = true;
			Projectile.timeLeft = 6;
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
			Lighting.AddLight(Projectile.Center, ((200 - Projectile.alpha) * 0.1f) / 200f, ((200 - Projectile.alpha) * 0.1f) / 200f, ((200 - Projectile.alpha) * 0.1f) / 200f);   //this is the light colors


			if (Main.rand.NextBool(1))     //this defines how many dust to spawn
			{
				int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TerraBlade, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);
				Main.dust[dust].noGravity = true; //this make so the dust has no gravity
				Main.dust[dust].velocity *= 0.5f;

				int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Clentaminator_Green, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);
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
			SoundEngine.PlaySound(SoundID.Item74, Projectile.position);

			for (int i = 0; i < 50; i++)
			{
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.TerraBlade);
				dust.noGravity = true;
				dust.scale = 2f;
				dust.velocity *= 5f;
				dust.fadeIn = 1f;
			}

			for (int i = 0; i < 50; i++)
			{
				float speedY = -2f;

				Vector2 perturbedSpeed = new Vector2(0, speedY).RotatedByRandom(MathHelper.ToRadians(360));
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Clentaminator_Green, perturbedSpeed.X, perturbedSpeed.Y);

				dust.noGravity = true;
				dust.scale = 1.5f;
				dust.velocity *= 4f;
				dust.fadeIn = 1f;
			}

			for (int i = 0; i < 50; i++)
			{
				Dust dust;
				Vector2 position = Projectile.position;
				dust = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, DustID.TerraBlade, 0f, 0f, 0, default, 1f)];
				dust.noGravity = true;
				dust.scale = 1f;
			}
		}
	}
}