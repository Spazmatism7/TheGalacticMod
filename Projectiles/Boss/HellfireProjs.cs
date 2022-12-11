using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalacticMod.Projectiles.Boss
{
    public class Fireball : ModProjectile 
    {
        public override void SetStaticDefaults() 
        {
            DisplayName.SetDefault("Fireball");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults() 
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.timeLeft = 3600;
            Projectile.aiStyle = 0;
            Projectile.light = 1f;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffType<HellfireDebuff>(), 7 * 60);
        }
    }

    public class FirenadoBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firenado Bolt");
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.timeLeft = 3600;
            Projectile.aiStyle = 0;
            Projectile.light = 1f;
            Projectile.hostile = true;
            Projectile.damage = 200;
            Projectile.timeLeft = 4 * 60;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 6)
                    Projectile.frame = 0;
            }
        }

        public override void Kill(int timeLeft) =>
            // summon a tornado
            SummonWave(Projectile.position);

        private async void SummonWave(Vector2 position)
        {
            float width = 160f;
            float height = 42;
            float newHeight = height * 0.6f;
            float newWidth = width * 0.6f;
            float baseHeight = (float)(newHeight * 2 + (16 * (Math.Floor(position.Y / 16))));
            int[] projectiles = new int[10];
            for (int i = 1; i <= 10; i++)
            {
                Main.NewText(i, Color.Red);
                var p = Projectile.NewProjectile(null, position.X - width / 2, baseHeight, 0, 0, ProjectileType<Firenado>(), Projectile.damage, 5, Projectile.owner);
                //Projectile.GetProjectileSource_FromThis()
                projectiles[i - 1] = p;
                Main.projectile[p].scale = 0.1f * i + 0.6f;
                Main.projectile[p].ai[1] = (2) * 10f;
                Main.projectile[p].ai[0] = -1;
                baseHeight -= (i * 0.1f + 0.6f) * height;
                newWidth = (i * 0.1f + 0.6f) * width;
                await Task.Delay(50);
            }
            for (int i = 0; i < 10; i++)
            {
                Main.projectile[projectiles[i]].ai[0] = i * 5;
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(ModContent.BuffType<HellfireDebuff>(), 10 * 60);
        }
    }

    public class Firenado : ModProjectile
    {

        private int time = 360;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firenado");
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 42;
            Projectile.timeLeft = 3600;
            Projectile.aiStyle = -1;
            Projectile.light = 1f;
            Projectile.hostile = true;
            Projectile.damage = 200;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 6)
                {
                    Projectile.frame = 0;
                }
            }

            double waveX = Projectile.ai[0] * Math.PI / 180;

            if (Projectile.ai[0] >= 0)
            {
                Projectile.position.X += (float)Math.Sin(waveX) * Projectile.ai[1] / 10;

                if (Projectile.ai[0] > time)
                {
                    Projectile.ai[0] = 0;
                }
                else
                {
                    Projectile.ai[0] += 4;
                }
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(ModContent.BuffType<HellfireDebuff>(), 10 * 60);
        }
    }
}