using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.PostML.DebuffGun
{
    public class IceProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frostburn Bolt");
        }

        public override void SetDefaults()
        {
            Projectile.height = 14;
            Projectile.width = 26;
            Projectile.aiStyle = -1;
            Projectile.light = 1f;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            ModContent.GetInstance<GalacticPlayer>().exemptProjs = true;
            Projectile.extraUpdates = 2;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.LightBlue.ToVector3() * 0.78f);

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
            target.AddBuff(BuffID.Frostburn, 60 * 30);
            target.AddBuff(BuffID.Frozen, 60 * 15);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 16; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Ice);
                d.frame.Y = 0;
                d.velocity *= 2;
            }

            for (int i = 0; i < 16; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Frost);
                d.frame.Y = 0;
                d.velocity *= 2;
            }
        }
    }
}