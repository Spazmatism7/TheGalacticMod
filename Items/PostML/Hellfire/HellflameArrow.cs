using GalacticMod.Items.Hardmode.Mage;
using GalacticMod.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Items.PostML.Hellfire
{
    public class HellflameArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellflame Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2)? 0 : 10;
                d.noGravity = true;

                Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.Lava, Vector2.Zero);
                d2.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d2.noGravity = true;

                Dust d3 = Dust.NewDustPerfect(Projectile.Center, DustID.SolarFlare, Vector2.Zero);
                d3.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d3.noGravity = true;

                Dust d4 = Dust.NewDustPerfect(Projectile.Center, DustID.FlameBurst, Vector2.Zero);
                d4.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d4.noGravity = true;

                Dust d5 = Dust.NewDustPerfect(Projectile.Center, DustID.Flare, Vector2.Zero);
                d5.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d5.noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch);
                d.velocity *= 2;
                d.noGravity = true;

                Dust d2 = Dust.NewDustPerfect(Projectile.Center, DustID.Lava);
                d2.velocity *= 2;
                d2.noGravity = true;

                Dust d3 = Dust.NewDustPerfect(Projectile.Center, DustID.SolarFlare);
                d3.velocity *= 2;
                d3.noGravity = true;

                Dust d4 = Dust.NewDustPerfect(Projectile.Center, DustID.FlameBurst);
                d4.velocity *= 2;
                d4.noGravity = true;

                Dust d5 = Dust.NewDustPerfect(Projectile.Center, DustID.Flare);
                d5.velocity *= 2;
                d5.noGravity = true;
            }

            Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.velocity - Projectile.velocity, ProjectileType<FieryExplosion>(), 
                Projectile.damage, 10, Projectile.owner);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = (Texture2D)Request<Texture2D>("GalacticMod/Assets/Graphics/LightTrail_1");
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            SpriteEffects effects = (Projectile.spriteDirection == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            for (int k = 0; k < Projectile.oldPos.Length - 1; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                Color color = new Color(200 + k * 5, 150 - k * 10, 0, 50);
                spriteBatch.Draw(texture, drawPos, null, color * 0.45f, Projectile.oldRot[k] + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
                spriteBatch.Draw(texture, drawPos - Projectile.oldPos[k] * 0.5f + Projectile.oldPos[k + 1] * 0.5f, null, color * 0.45f, Projectile.oldRot[k] * 0.5f + Projectile.oldRot[k + 1] * 0.5f + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
            }
            return true;
        }
    }
}