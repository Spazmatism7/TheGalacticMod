using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GalacticMod.Buffs;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.Audio;
using GalacticMod.Items.Hardmode.Terra;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticMod.Projectiles
{
	public class HellfireWave : ModProjectile
	{
        bool changeFrame = false;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
            DisplayName.SetDefault("Hellfire Wave");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
		{
            Projectile.width = 60;
            Projectile.height = 162;
            Projectile.aiStyle = 191;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.alpha = 150;
            Projectile.timeLeft = 90;
            Projectile.ownerHitCheck = true;
            Projectile.ownerHitCheckDistance = 300f;

            Projectile.ArmorPenetration = 20;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);

            Projectile.alpha -= 40;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }

            if (Main.rand.NextBool(3))
            {
                Dust dt = Dust.NewDustPerfect(Projectile.Top, DustID.Torch);
                dt.frame.Y = 0;
                dt.velocity *= 2;
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch);
                d.frame.Y = 0;
                d.velocity *= 2;
                Dust db = Dust.NewDustPerfect(Projectile.Bottom, DustID.Torch);
                db.frame.Y = 0;
                db.velocity *= 2;
            }

            if (++Projectile.frame >= Main.projFrames[Projectile.type] - 1 && changeFrame)
            {
                Projectile.frame = 0;
                changeFrame = false;
            }
            else
            {
                changeFrame = true;
            }

            Lighting.AddLight(Projectile.Center, 1.1f, 0.3f, 0.4f);
        }

        public override void Kill(int timeLeft)
        {
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
                spriteBatch.Draw(texture, drawPos - Projectile.oldPos[k] * 0.5f + Projectile.oldPos[k + 1] * 0.5f, null, color * 0.45f, Projectile.oldRot[k] * 0.5f + Projectile.oldRot[k + 1] *
                    0.5f + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
            }
            return true;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            target.AddBuff(BuffType<HellfireDebuff>(), 1500);
		}
	}
}