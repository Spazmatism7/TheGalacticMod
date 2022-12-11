using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.PostML.DebuffGun
{
    public class VenomProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venom Bolt");
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
            Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 0.78f);

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
            target.AddBuff(BuffID.Venom, 60 * 30);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 16; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Venom);
                d.frame.Y = 0;
                d.velocity *= 2;
            }
            int venomspeed = 10;
            int distance = 200;

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X - distance, Projectile.Center.Y - distance), new Vector2(+venomspeed, +venomspeed), 
                ModContent.ProjectileType<VenomBolts>(), (int)(Projectile.damage * 0.8f), Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X + distance, Projectile.Center.Y - distance), new Vector2(-venomspeed, +venomspeed), 
                ModContent.ProjectileType<VenomBolts>(), (int)(Projectile.damage * 0.8f), Projectile.knockBack, Projectile.owner);

            Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X + distance, Projectile.Center.Y + distance), new Vector2(-venomspeed, -venomspeed), 
                ModContent.ProjectileType<VenomBolts>(), (int)(Projectile.damage * 0.8f), Projectile.knockBack, Projectile.owner);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X - distance, Projectile.Center.Y + distance), new Vector2(+venomspeed, -venomspeed), 
                ModContent.ProjectileType<VenomBolts>(), (int)(Projectile.damage * 0.8f), Projectile.knockBack, Projectile.owner);
        }
    }

    public class VenomBolts : ModProjectile
    {
        int timer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venom Bolt");
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 40;
            Projectile.aiStyle = -1;
            Projectile.light = 0.5f;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            ModContent.GetInstance<GalacticPlayer>().exemptProjs = true;
            Projectile.alpha = 175;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 1f);

            for (int i = 0; i < 2; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Venom, 0, 0);
                dust.noGravity = true;
                dust.scale = 2f;
            }

            timer++;

            if (timer == 1)
            {
                for (int i = 0; i < 25; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X - Projectile.width / 2, Projectile.position.Y - Projectile.height / 2), Projectile.width * 2, Projectile.height * 2, DustID.Venom, 0f, 0f, 0, default, 2f);

                    Main.dust[dustIndex].noGravity = true;
                    Main.dust[dustIndex].velocity *= 3f;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.localNPCImmunity[target.whoAmI] = -1;
            target.immune[Projectile.owner] = 0;
            target.AddBuff(BuffID.Venom, 60 * 30);
        }
    }
}