using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Utilities;
using GalacticMod.NPCs.Bosses;
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.PostML.Galactic
{
    public class GalacticLightning : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning");
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 4;
            Projectile.timeLeft = 450;
            Projectile.scale = 0.75f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 75;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.localAI[1] < 1f)
            {
                Projectile.localAI[1] += 2f;
                Projectile.position += Projectile.velocity;
                Projectile.velocity = Vector2.Zero;
            }
            return false;
        }

        public override bool? Colliding(Rectangle myRect, Rectangle targetRect)
        {
            for (int i = 0; i < Projectile.oldPos.Length && (Projectile.oldPos[i].X != 0f || Projectile.oldPos[i].Y != 0f); i++)
            {
                myRect.X = (int)Projectile.oldPos[i].X;
                myRect.Y = (int)Projectile.oldPos[i].Y;
                if (myRect.Intersects(targetRect))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (!Main.dedServ)
            {
                Color color = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
                Vector2 end = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
                Projectile.GetAlpha(color);
                Vector2 vector = new Vector2(Projectile.scale) / 2f;
                for (int i = 0; i < 2; i++)
                {
                    float num = ((Projectile.localAI[1] == -1f || Projectile.localAI[1] == 1f) ? (-0.2f) : 0f);
                    if (i == 0)
                    {
                        vector = new Vector2(Projectile.scale) * (0.5f + num);
                        DelegateMethods.c_1 = new Color(113, 251, 255, 0) * 0.5f;
                    }
                    else
                    {
                        vector = new Vector2(Projectile.scale) * (0.3f + num);
                        DelegateMethods.c_1 = new Color(255, 255, 255, 0) * 0.5f;
                    }
                    DelegateMethods.f_1 = 1f;
                    for (int j = Projectile.oldPos.Length - 1; j > 0; j--)
                    {
                        if (!(Projectile.oldPos[j] == Vector2.Zero))
                        {
                            Vector2 start = Projectile.oldPos[j] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                            Vector2 end2 = Projectile.oldPos[j - 1] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                            Utils.DrawLaser(Main.spriteBatch, tex, start, end2, vector, DelegateMethods.LightningLaserDraw);
                        }
                    }
                    if (Projectile.oldPos[0] != Vector2.Zero)
                    {
                        DelegateMethods.f_1 = 1f;
                        Vector2 start2 = Projectile.oldPos[0] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                        Utils.DrawLaser(Main.spriteBatch, tex, start2, end, vector, DelegateMethods.LightningLaserDraw);
                    }
                }
            }
            return false;
        }

        public override void AI()
        {
            if (Projectile.scale > 0.05f)
            {
                Projectile.scale -= 0.005f;
            }
            else
            {
                Projectile.Kill();
            }
            if (Projectile.localAI[1] == 0f && Projectile.ai[0] >= 900f)
            {
                Projectile.ai[0] -= 1000f;
                Projectile.localAI[1] = -1f;
            }
            int frameCounter = Projectile.frameCounter;
            Projectile.frameCounter = frameCounter + 1;
            Lighting.AddLight(Projectile.Center, 0.3f, 0.45f, 0.5f);
            if (Projectile.velocity == Vector2.Zero)
            {
                if (Projectile.frameCounter >= Projectile.extraUpdates * 2)
                {
                    Projectile.frameCounter = 0;
                    bool flag = true;
                    for (int i = 1; i < Projectile.oldPos.Length; i++)
                    {
                        if (Projectile.oldPos[i] != Projectile.oldPos[0])
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        Projectile.Kill();
                        return;
                    }
                }
                if (Main.rand.NextBool(Projectile.extraUpdates))
                {
                    for (int j = 0; j < 2; j++)
                    {
                        float num = Projectile.rotation + ((Main.rand.NextBool(2)) ? (-1f) : 1f) * ((float)Math.PI / 2f);
                        float num2 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                        Vector2 vector = new Vector2((float)Math.Cos(num) * num2, (float)Math.Sin(num) * num2);
                        int num3 = Dust.NewDust(Projectile.Center, 0, 0, DustID.Electric, vector.X, vector.Y);
                        Main.dust[num3].noGravity = true;
                        Main.dust[num3].scale = 1.2f;
                    }
                    if (Main.rand.NextBool(5))
                    {
                        Vector2 vector2 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
                        int num4 = Dust.NewDust(Projectile.Center + vector2 - Vector2.One * 4f, 8, 8, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
                        Dust dust = Main.dust[num4];
                        dust.velocity *= 0.5f;
                        Main.dust[num4].velocity.Y = 0f - Math.Abs(Main.dust[num4].velocity.Y);
                    }
                }
            }
            else
            {
                if (Projectile.frameCounter < Projectile.extraUpdates * 2)
                {
                    return;
                }
                Projectile.frameCounter = 0;
                float num5 = Projectile.velocity.Length();
                UnifiedRandom unifiedRandom = new UnifiedRandom((int)Projectile.ai[1]);
                int num6 = 0;
                Vector2 spinningpoint = -Vector2.UnitY;
                while (true)
                {
                    int num7 = unifiedRandom.Next();
                    Projectile.ai[1] = num7;
                    num7 %= 100;
                    float f = (float)num7 / 100f * ((float)Math.PI * 2f);
                    Vector2 vector3 = f.ToRotationVector2();
                    if (vector3.Y > 0f)
                    {
                        vector3.Y *= -1f;
                    }
                    bool flag2 = false;
                    if (vector3.Y > -0.02f)
                    {
                        flag2 = true;
                    }
                    if (vector3.X * (float)(Projectile.extraUpdates + 2) * 2f * num5 + Projectile.localAI[0] > 40f)
                    {
                        flag2 = true;
                    }
                    if (vector3.X * (float)(Projectile.extraUpdates + 2) * 2f * num5 + Projectile.localAI[0] < -40f)
                    {
                        flag2 = true;
                    }
                    if (flag2)
                    {
                        if (num6++ >= 100)
                        {
                            Projectile.velocity = Vector2.Zero;
                            /*if (Projectile.localAI[1] < 1f)
							{
								Projectile.localAI[1] += 2f;
							}*/
                            Projectile.localAI[1] = 1f;
                            break;
                        }
                        continue;
                    }
                    spinningpoint = vector3;
                    break;
                }
                if (Projectile.velocity != Vector2.Zero)
                {

                    Projectile.localAI[0] += spinningpoint.X * (float)(Projectile.extraUpdates + 1) * 2f * num5;
                    Projectile.velocity = spinningpoint.RotatedBy(Projectile.ai[0] + (float)Math.PI / 2f) * num5;
                    Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            if (Main.rand.NextBool(50))
            {
                float num = Projectile.rotation + ((Main.rand.NextBool(2)) ? (-1f) : 1f) * ((float)Math.PI / 2f);
                float num2 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                Vector2 vector = new Vector2((float)Math.Cos(num) * num2, (float)Math.Sin(num) * num2);
                int num3 = Dust.NewDust(Projectile.Center, 0, 0, DustID.Electric, vector.X, vector.Y);
                Main.dust[num3].noGravity = true;
                Main.dust[num3].scale = 1.2f;
            }
        }
    }

    public class GalacticAmuletLightning : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning");
            ProjectileID.Sets.SentryShot[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = 88;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 4;
            Projectile.timeLeft = 120;
            Projectile.scale = 0.75f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 75;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 60;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.localAI[1] < 1f)
            {
                Projectile.localAI[1] += 2f;
                Projectile.position += Projectile.velocity;
                Projectile.velocity = Vector2.Zero;
            }
            return false;
        }

        public override bool? Colliding(Rectangle myRect, Rectangle targetRect)
        {
            for (int i = 0; i < Projectile.oldPos.Length && (Projectile.oldPos[i].X != 0f || Projectile.oldPos[i].Y != 0f); i++)
            {
                myRect.X = (int)Projectile.oldPos[i].X;
                myRect.Y = (int)Projectile.oldPos[i].Y;
                if (myRect.Intersects(targetRect))
                {
                    return true;
                }
            }
            return false;
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Color color = Lighting.GetColor((int)((double)Projectile.position.X + (double)Projectile.width * 0.5) / 16, (int)(((double)Projectile.position.Y + (double)Projectile.height * 0.5) / 16.0));
            Vector2 end = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Projectile.GetAlpha(color);
            Vector2 vector = new Vector2(Projectile.scale) / 2f;
            for (int i = 0; i < 2; i++)
            {
                float num = ((Projectile.localAI[1] == -1f || Projectile.localAI[1] == 1f) ? (-0.2f) : 0f);
                if (i == 0)
                {
                    vector = new Vector2(Projectile.scale) * (0.5f + num);
                    DelegateMethods.c_1 = new Color(113, 251, 255, 0) * 0.5f;
                }
                else
                {
                    vector = new Vector2(Projectile.scale) * (0.3f + num);
                    DelegateMethods.c_1 = new Color(255, 255, 255, 0) * 0.5f;
                }
                DelegateMethods.f_1 = 1f;
                for (int j = Projectile.oldPos.Length - 1; j > 0; j--)
                {
                    if (!(Projectile.oldPos[j] == Vector2.Zero))
                    {
                        Vector2 start = Projectile.oldPos[j] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                        Vector2 end2 = Projectile.oldPos[j - 1] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                        Utils.DrawLaser(Main.spriteBatch, tex, start, end2, vector, DelegateMethods.LightningLaserDraw);
                    }
                }
                if (Projectile.oldPos[0] != Vector2.Zero)
                {
                    DelegateMethods.f_1 = 1f;
                    Vector2 start2 = Projectile.oldPos[0] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                    Utils.DrawLaser(Main.spriteBatch, tex, start2, end, vector, DelegateMethods.LightningLaserDraw);
                }
            }
            return false;
        }

        public override void AI()
        {
            if (Projectile.scale > 0.1f)
            {
                Projectile.scale -= 0.005f;
            }
            else
            {
                Projectile.Kill();
            }

            if (Projectile.localAI[1] == 0f && Projectile.ai[0] >= 900f)
            {
                Projectile.ai[0] -= 1000f;
                Projectile.localAI[1] = -1f;
            }
            int frameCounter = Projectile.frameCounter;
            Projectile.frameCounter = frameCounter + 1;
            if (!Main.dedServ)
            {
                Lighting.AddLight(Projectile.Center, 0.3f, 0.45f, 0.5f);
            }
            if (Projectile.velocity == Vector2.Zero)
            {
                if (Projectile.frameCounter >= Projectile.extraUpdates * 2)
                {
                    Projectile.frameCounter = 0;
                    bool flag = true;
                    for (int i = 1; i < Projectile.oldPos.Length; i++)
                    {
                        if (Projectile.oldPos[i] != Projectile.oldPos[0])
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        Projectile.Kill();
                        return;
                    }
                }
                if (Main.rand.NextBool(Projectile.extraUpdates))
                {
                    for (int j = 0; j < 2; j++)
                    {
                        float num = Projectile.rotation + ((Main.rand.NextBool(2)) ? (-1f) : 1f) * ((float)Math.PI / 2f);
                        float num2 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                        Vector2 vector = new Vector2((float)Math.Cos(num) * num2, (float)Math.Sin(num) * num2);
                        int num3 = Dust.NewDust(Projectile.Center, 0, 0, DustID.Electric, vector.X, vector.Y);
                        Main.dust[num3].noGravity = true;
                        Main.dust[num3].scale = 1.2f;
                    }
                    if (Main.rand.NextBool(5))
                    {
                        Vector2 vector2 = Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
                        int num4 = Dust.NewDust(Projectile.Center + vector2 - Vector2.One * 4f, 8, 8, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
                        Dust dust = Main.dust[num4];
                        dust.velocity *= 0.5f;
                        Main.dust[num4].velocity.Y = 0f - Math.Abs(Main.dust[num4].velocity.Y);
                    }
                }
            }
            else
            {
                if (Projectile.frameCounter < Projectile.extraUpdates * 2)
                {
                    return;
                }
                Projectile.frameCounter = 0;
                float num5 = Projectile.velocity.Length();
                UnifiedRandom unifiedRandom = new UnifiedRandom((int)Projectile.ai[1]);
                int num6 = 0;
                Vector2 spinningpoint = -Vector2.UnitY;
                while (true)
                {
                    int num7 = unifiedRandom.Next();
                    Projectile.ai[1] = num7;
                    num7 %= 100;
                    float f = (float)num7 / 100f * ((float)Math.PI * 2f);
                    Vector2 vector3 = f.ToRotationVector2();
                    if (vector3.Y > 0f)
                    {
                        vector3.Y *= -1f;
                    }
                    bool flag2 = false;
                    if (vector3.Y > -0.02f)
                    {
                        flag2 = true;
                    }
                    if (vector3.X * (Projectile.extraUpdates + 2) * 2f * num5 + Projectile.localAI[0] > 40f)
                    {
                        flag2 = true;
                    }
                    if (vector3.X * (Projectile.extraUpdates + 2) * 2f * num5 + Projectile.localAI[0] < -40f)
                    {
                        flag2 = true;
                    }
                    if (flag2)
                    {
                        if (num6++ >= 100)
                        {
                            Projectile.velocity = Vector2.Zero;
                            Projectile.localAI[1] = 1f;
                            break;
                        }
                        continue;
                    }
                    spinningpoint = vector3;
                    break;
                }
                if (Projectile.velocity != Vector2.Zero)
                {

                    Projectile.localAI[0] += spinningpoint.X * (Projectile.extraUpdates + 1) * 2f * num5;
                    Projectile.velocity = spinningpoint.RotatedBy(Projectile.ai[0] + (float)Math.PI / 2f) * num5;
                    Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.damage = Projectile.damage * 8 / 10;
        }

        public override void Kill(int timeLeft)
        {
            if (Main.rand.NextBool(50))
            {
                float num = Projectile.rotation + ((Main.rand.NextBool(2)) ? (-1f) : 1f) * ((float)Math.PI / 2f);
                float num2 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                Vector2 vector = new Vector2((float)Math.Cos(num) * num2, (float)Math.Sin(num) * num2);
                int num3 = Dust.NewDust(Projectile.Center, 0, 0, DustID.Electric, vector.X, vector.Y);
                Main.dust[num3].noGravity = true;
                Main.dust[num3].scale = 1.2f;
            }
        }
    }
}
