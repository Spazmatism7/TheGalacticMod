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

namespace GalacticMod.Projectiles
{
    public class SpinningRainbow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.height = 36;
			Projectile.width = 36;
            Projectile.aiStyle = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
			Projectile.timeLeft = 100;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            Projectile.alpha -= 40;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }

            if (++Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }

             Projectile.localAI[0] += 1f;
             for (int num66 = 0; num66 < 1; num66++)
             {
                 Vector2 spinningpoint4 = Utils.RandomVector2(Main.rand, -0.5f, 0.5f) * new Vector2(20f, 80f);
                 spinningpoint4 = spinningpoint4.RotatedBy(Projectile.velocity.ToRotation());
             }
            Lighting.AddLight(Projectile.Center, 1.1f, 0.3f, 0.4f);
        }
    }
}