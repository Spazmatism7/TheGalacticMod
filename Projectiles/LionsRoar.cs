using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Projectiles
{
	public class LionsRoar : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
            DisplayName.SetDefault("Lion's Roar");
        }

        public override void SetDefaults()
		{
			Projectile.width = 46;
			Projectile.height = 68;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 7;
			Projectile.timeLeft = 7 * 60;
			Projectile.light = .89f;
		}

        public override void AI()
        {
			Lighting.AddLight(Projectile.Center, Color.Yellow.ToVector3() * 0.78f);

			Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f) ? 1 : -1;

			Projectile.rotation = Projectile.velocity.ToRotation();
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
			for (int i = 0; i < 16; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.FireworkFountain_Yellow);
				d.frame.Y = 0;
				d.velocity *= 2;
			}
		}
	}
}