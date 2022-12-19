using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace GalacticMod.Projectiles
{
    public class FireTotemProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Fire Blast");
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = 0;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 15;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            AnimateProjectile();

            if (Main.rand.NextFloat() < 1f)
            {
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Projectile.position;
                Dust dust = Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 1, Projectile.velocity.Y * 1, 0, new Color(255, 255, 255), 1f);
                dust.noGravity = true;
            }
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position2 = Projectile.Center;
            Dust dust2 = Dust.NewDustPerfect(position2, DustID.LavaMoss, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
            dust2.noGravity = true;
            dust2.noLight = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Item86, Projectile.Center);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.Torch);
                }
            }
            {
                int numberProjectiles = 4 + Main.rand.Next(2); //This defines how many projectiles to shot.

                for (int i = 0; i < numberProjectiles; i++)
                {
                    float speedX = 0f;
                    float speedY = -6f;

                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(170));
                    float scale = 1f - (Main.rand.NextFloat() * .7f);
                    perturbedSpeed = perturbedSpeed * scale;
                    int ProjID = Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<FireTotemProj2>(), (int)(Projectile.damage * 0.33f), 1, Projectile.owner);
                    Main.projectile[ProjID].ArmorPenetration = 5;
                }
            }
        }

        public void AnimateProjectile() // Call this every frame, for example in the AI method.
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4) // This will change the sprite every 8 frames (0.13 seconds). Feel free to experiment.
            {
                Projectile.frame++;
                Projectile.frame %= 5; // Will reset to the first frame if you've gone through them all.
                Projectile.frameCounter = 0;
            }
        }
    }

    public class FireTotemProj2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Small Fire Bolt");
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60;
            Projectile.aiStyle = 14;
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 15;
            //drawOffsetX = -2;
            //drawOriginOffsetY = -2;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Generic;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(2))
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = Projectile.position;
                dust = Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 1, Projectile.velocity.Y * 1, 0, new Color(255, 255, 255), 1f);
                dust.noGravity = true;
            }

            Projectile.rotation += Projectile.direction * -0.2f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                SoundEngine.PlaySound(SoundID.Item85, Projectile.Center);

                for (int i = 0; i < 10; i++)
                {
                    var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
                    dust.scale = 1.5f;
                }
            }
        }
    }
}