using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace GalacticMod.Projectiles.Boss
{
	public class GalacticBolt : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 22;
			Projectile.aiStyle = 1;
			Projectile.hostile = true;
			Projectile.light = 1f;
			Projectile.extraUpdates = 3;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 240;
		}
	}

    public class GalaxyLaserBarrage : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Laser Barrage");
        }

        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.aiStyle = 1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.alpha = 255;
            Projectile.extraUpdates = 2;
            Projectile.scale = 1f;
            Projectile.timeLeft = 600;
            Projectile.ignoreWater = true;
        }
    }

    public class ElectrifyingOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.timeLeft = 3600;
            Projectile.aiStyle = 0;
            Projectile.light = 1f;
            Projectile.hostile = true;
            Projectile.penetrate = 50;
            Projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Projectile.velocity.Y += Projectile.ai[0];
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                Projectile.ai[0] += 0.1f;
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                Projectile.velocity *= 0.75f;
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }
            return false;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            Projectile.ai[0] += 0.1f;
            Projectile.velocity *= 0.75f;

            target.AddBuff(BuffID.Electrified, 120);
        }
    }

    public class GalaxySphere : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Curse");
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 83;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 780; //13 seconds
            Projectile.tileCollide = false;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffID.Cursed, 240);
        }

        public override void Kill(int timeLeft)
        {
            Vector2 perturbedSpeed = new Vector2(0, -7).RotatedByRandom(MathHelper.ToRadians(360));
            Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<AncientBoom>(), Projectile.damage, 0, Projectile.owner);
        }
    }

    public class AncientBoom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Curse");

        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
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
            Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Clentaminator_Blue, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);

            if (Main.rand.NextBool(1))     //this defines how many dust to spawn
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Clentaminator_Blue, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);

                Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                Main.dust[dust].velocity *= 0.5f;
                int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Vortex, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);
            }
            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft == 3)
            {
                Projectile.velocity.X = 0f;
                Projectile.velocity.Y = 0f;
                Projectile.tileCollide = false;
                Projectile.position = Projectile.Center;
                Projectile.width = 100;
                Projectile.height = 100;
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
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Vortex);
                dust.noGravity = true;
                dust.scale = 2f;
                dust.velocity *= 5f;
                dust.fadeIn = 1f;
            }

            for (int i = 0; i < 50; i++)
            {
                float speedY = -2f;

                Vector2 perturbedSpeed = new Vector2(0, speedY).RotatedByRandom(MathHelper.ToRadians(360));
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Vortex, perturbedSpeed.X, perturbedSpeed.Y);

                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.velocity *= 4f;
                dust.fadeIn = 1f;
            }

            for (int i = 0; i < 50; i++)
            {
                Dust dust;
                Vector2 position = Projectile.position;
                dust = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, DustID.Vortex, 0f, 0f, 0, default, 1f)];
                dust.noGravity = true;
                dust.scale = 1f;
            }
        }
    }

    /*public class BluefireSphere : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 14;
            Projectile.timeLeft = 3600;
            Projectile.aiStyle = 0;
            Projectile.light = 1f;
            Projectile.hostile = true;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffID.Frostburn2, 7 * 60);
        }
    }*/
}