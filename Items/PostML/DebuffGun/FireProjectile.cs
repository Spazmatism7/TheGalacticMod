using Terraria;
using Terraria.ID;
using GalacticMod.Projectiles;
using Terraria.ModLoader;
using GalacticMod.Assets.Systems;
using Microsoft.Xna.Framework;
using GalacticMod.Items.Hardmode.Mage;
using Terraria.Audio;

namespace GalacticMod.Items.PostML.DebuffGun
{
    public class FireProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Bolt");
        }

        public override void SetDefaults()
        {
            Projectile.height = 14;
            Projectile.width = 26;
            Projectile.aiStyle = -1;
            Projectile.light = 1f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.OrangeRed.ToVector3() * 0.78f);

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
            target.AddBuff(323, 60 * 30); //Hellfire
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

            Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.velocity - Projectile.velocity, ModContent.ProjectileType<FieryExplosion>(), Projectile.damage / 2, 0, Projectile.owner);
            //Projectile.GetProjectileSource_FromThis()
        }
    }
}