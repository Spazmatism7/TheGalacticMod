using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;

namespace GalacticMod.Projectiles.Dev
{
    public class ColorArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Paint Arrow");
            Main.projFrames[Projectile.type] = 7;
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
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 219, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2)? 0 : 10;
                d.noGravity = true;
            }
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 220, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2)? 0 : 10;
                d.noGravity = true;
            }
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 221, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2)? 0 : 10;
                d.noGravity = true;
            }
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 222, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2)? 0 : 10;
                d.noGravity = true;
            }
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 223, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2)? 0 : 10;
                d.noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 219);
                d.velocity *= 2;
                d.noGravity = true;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 220);
                d.velocity *= 2;
                d.noGravity = true;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 221);
                d.velocity *= 2;
                d.noGravity = true;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 222);
                d.velocity *= 2;
                d.noGravity = true;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 223);
                d.velocity *= 2;
                d.noGravity = true;
            }
        }
    }
}