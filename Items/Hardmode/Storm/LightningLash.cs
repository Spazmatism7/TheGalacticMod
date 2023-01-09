using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent;
using GalacticMod.Buffs;
using Terraria.Utilities;

namespace GalacticMod.Items.Hardmode.Storm
{
    public class LightningLash : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons a bolt of lightning to strike struck enemies");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DefaultToWhip(ModContent.ProjectileType<LightningLashP>(), 50, 2, 4);

            Item.shootSpeed = 4;
            Item.rare = ItemRarityID.Pink;

            Item.channel = true;
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() => true;
    }

    public class LightningLashP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Lash");

            // This makes the projectile use whip collision detection and allows flasks to be applied to it.
            ProjectileID.Sets.IsAWhip[Type] = true;
        }

        public override void SetDefaults() =>
            // This method quickly sets the whip's properties.
            Projectile.DefaultToWhip(); // use these to change from the vanilla defaults// Projectile.WhipSettings.Segments = 20;// Projectile.WhipSettings.RangeMultiplier = 1f;

        private float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        private float ChargeTime
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        // This example uses PreAI to implement a charging mechanic.
        // If you remove this, also remove Item.channel = true from the item's SetDefaults.
        public override bool PreAI()
        {
            Player owner = Main.player[Projectile.owner];

            // Like other whips, this whip updates twice per frame (Projectile.extraUpdates = 1), so 120 is equal to 1 second.
            if (!owner.channel || ChargeTime >= 120)
                return true; // Let the vanilla whip AI run.

            if (++ChargeTime % 12 == 0) // 1 segment per 12 ticks of charge.
                Projectile.WhipSettings.Segments++;

            // Increase range up to 2x for full charge.
            Projectile.WhipSettings.RangeMultiplier += 1 / 120f;

            // Reset the animation and item timer while charging.
            owner.itemAnimation = owner.itemAnimationMax;
            owner.itemTime = owner.itemTimeMax;

            return false; // Prevent the vanilla whip AI from running.
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            float xpos = (Main.rand.NextFloat(-50, 50));
            float ai = Main.rand.Next(100);
            Vector2 rotation = -new Vector2(target.Center.X - xpos, target.Center.Y - 500) + target.Center;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(target.Center.X - xpos, target.Center.Y - 500), new Vector2(xpos * 0.02f, 5),
                ModContent.ProjectileType<LightningLashP2>(), damage, .5f, Main.myPlayer, rotation.ToRotation(), ai);

            for (int i = 0; i < 25; i++)
            {
                float speedY = -3f;
                Vector2 dustspeed = new Vector2(0, speedY).RotatedByRandom(MathHelper.ToRadians(360));

                int dust2 = Dust.NewDust(new Vector2(target.Center.X - xpos, target.Center.Y - 500), 0, 0, DustID.Electric, dustspeed.X, dustspeed.Y, 229, default, 1.5f);
                Main.dust[dust2].noGravity = true;
            }

            Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
            Projectile.damage = 3 * (Projectile.damage / 5); //40% multihit penalty
        }

        // This method draws a line between all points of the whip, in case there's empty space between the sprites.
        private void DrawLine(List<Vector2> list)
        {
            Texture2D texture = TextureAssets.FishingLine.Value;
            Rectangle frame = texture.Frame();
            Vector2 origin = new Vector2(frame.Width / 2, 2);

            Vector2 pos = list[0];
            for (int i = 0; i < list.Count - 1; i++)
            {
                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.White);
                Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, 0, SpriteEffects.None, 0);

                pos += diff;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, list);

            DrawLine(list);

            //Main.DrawWhip_WhipBland(Projectile, list);
            // The code below is for custom drawing.
            // If you don't want that, you can remove it all and instead call one of vanilla's DrawWhip methods, like above.
            // However, you must adhere to how they draw if you do.

            SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Main.instance.LoadProjectile(Type);
            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 pos = list[0];

            for (int i = 0; i < list.Count - 1; i++)
            {
                // These two values are set to suit this projectile's sprite, but won't necessarily work for your own.
                // You can change them if they don't!
                Rectangle frame = new Rectangle(0, 0, 22, 26);
                Vector2 origin = new Vector2(5, 8);
                float scale = 1;

                // These statements determine what part of the spritesheet to draw for the current segment.
                // They can also be changed to suit your sprite.
                if (i == list.Count - 2)
                {
                    frame.Y = 74;
                    frame.Height = 18;

                    // For a more impactful look, this scales the tip of the whip up when fully extended, and down when curled up.
                    Projectile.GetWhipSettings(Projectile, out float timeToFlyOut, out int _, out float _);
                    float t = Timer / timeToFlyOut;
                    scale = MathHelper.Lerp(0.5f, 1.5f, Utils.GetLerpValue(0.1f, 0.7f, t, true) * Utils.GetLerpValue(0.9f, 0.7f, t, true));
                }
                else if (i > 10)
                {
                    frame.Y = 58;
                    frame.Height = 16;
                }
                else if (i > 5)
                {
                    frame.Y = 42;
                    frame.Height = 16;
                }
                else if (i > 0)
                {
                    frame.Y = 26;
                    frame.Height = 16;
                }

                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2; // This projectile's sprite faces down, so PiOver2 is used to correct rotation.
                Color color = Lighting.GetColor(element.ToTileCoordinates());

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, flip, 0);

                pos += diff;
            }
            return false;
        }
    }

    public class LightningLashP2 : ModProjectile
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
            Projectile.DamageType = DamageClass.Summon;
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