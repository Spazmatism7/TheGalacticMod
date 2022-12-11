using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Enums;

namespace GalacticMod.Projectiles
{
    public class EyeSwordProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye of the Eater");
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.light = 0.3f;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 180;
            Projectile.aiStyle = 2;
            Projectile.scale = 1f;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            DrawOffsetX = -1;
            DrawOriginOffsetY = -1;
        }

        int rotate;

        public override void AI()
        {
            rotate += 1;
            Projectile.rotation = rotate * (0.1f * Projectile.direction);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood);
            Projectile.spriteDirection = Projectile.direction;
        }


        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.Center);

            for (int i = 0; i < 30; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood);
            }
        }
    }
}