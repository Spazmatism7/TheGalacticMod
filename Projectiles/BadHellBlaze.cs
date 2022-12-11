using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace GalacticMod.Projectiles
{
	public class BadHellBlaze : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
            DisplayName.SetDefault("Hell's Blaze");
        }

        public override void SetDefaults()
		{
			Projectile.width = 24;
			Projectile.height = 14;
			Projectile.aiStyle = 28;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.tileCollide = false;
			Projectile.light = 1f;
			Projectile.timeLeft = 6;
		}

		public override void AI()
		{
			Projectile.velocity.Y += Projectile.ai[0];

			Lighting.AddLight(Projectile.Center, Color.Orange.ToVector3() * 0.78f);

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

            if (++Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            for (int i = 0; i < 16; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch);
				d.frame.Y = 0;
				d.velocity *= 2;
			}

			for (int i = 0; i < 16; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Lava);
				d.frame.Y = 0;
				d.velocity *= 2;
			}

            Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.velocity - Projectile.velocity, ProjectileType<BadFieryExplosion>(), Projectile.damage / 2, 0, Projectile.owner);
            //Projectile.GetProjectileSource_FromThis()
        }
    }

    public class BadFieryExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.hostile = true;
            Projectile.timeLeft = 20;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.scale = 1.5f;
            Projectile.aiStyle = -1;
            Projectile.alpha = 0;
            DrawOffsetX = 25;
            DrawOriginOffsetY = 25;
            Projectile.light = 0.9f;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 2) // This will change the sprite every 8 frames (0.13 seconds). Feel free to experiment.
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Color.SandyBrown;
            return color;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 420);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 50; i++)
            {
                float speedY = -2f;

                Vector2 perturbedSpeed = new Vector2(0, speedY).RotatedByRandom(MathHelper.ToRadians(360));
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Pixie, perturbedSpeed.X, perturbedSpeed.Y);

                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.velocity *= 4f;
                dust.fadeIn = 1f;
            }

            for (int i = 0; i < 50; i++)
            {
                Dust dust;
                Vector2 position = Projectile.position;
                dust = Main.dust[Terraria.Dust.NewDust(position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 0, default, 1f)];
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }

    public class BadIcicle : ModProjectile
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
			Projectile.penetrate = -1;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.tileCollide = true;
			Projectile.light = 1f;
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
			Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<BadIceChunks>(), 80, 0, Projectile.owner);
			//Projectile.GetProjectileSource_FromThis()
		}
	}

    public class BadIceChunks : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Chunks");
		}

		public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 1;
            Projectile.light = 0.1f;
            Projectile.hostile = true;
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
            Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Cloud, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);

            if (Main.rand.NextBool(1))     //this defines how many dust to spawn
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueFairy, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);

                Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                Main.dust[dust].velocity *= 0.5f;
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
			SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);

			for (int i = 0; i < 50; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Cloud);
                dust.noGravity = true;
                dust.scale = 2f;
                dust.velocity *= 5f;
                dust.fadeIn = 1f;
            }

            for (int i = 0; i < 50; i++)
            {
                float speedY = -2f;

                Vector2 perturbedSpeed = new Vector2(0, speedY).RotatedByRandom(MathHelper.ToRadians(360));
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.BlueFairy, perturbedSpeed.X, perturbedSpeed.Y);

                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.velocity *= 4f;
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