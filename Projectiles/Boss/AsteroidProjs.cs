using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Buffs;
using Microsoft.Xna.Framework;

namespace GalacticMod.Projectiles.Boss
{
    public class BehemothFireball : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asteroid Fireball");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 42;
            Projectile.timeLeft = 3600;
            Projectile.aiStyle = 0;
            Projectile.light = 1f;
            Projectile.hostile = true;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffType<AsteroidBlaze>(), 7 * 60);
        }
    }

    public class BehemothLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asteroid Beam");
        }

        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 8;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
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
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, default, 1f);
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MeteorHead, 0, 0, 130, default, 0.5f);
            }
        }
    }
}