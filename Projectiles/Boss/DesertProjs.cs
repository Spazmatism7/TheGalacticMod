using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Buffs;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using Terraria.DataStructures;

namespace GalacticMod.Projectiles.Boss
{
    public class SandBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Blast");
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.aiStyle = 0;
            Projectile.light = 1f;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            if (Projectile.timeLeft <= 210)
                Projectile.velocity -= Projectile.velocity;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffType<SandBlasted>(), 5 * 60);
        }
    }

    public class SandCloud : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstorm");
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            Projectile.light = 1f;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
            Projectile.aiStyle = -1;
        }

        readonly int homerandom = Main.rand.Next(60, 180);

        public override void AI()
        {
            var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Sandstorm, 0, 0);
            dust.noGravity = true;
            dust.scale = 0.75f;

            Projectile.ai[0]++;

            if (Projectile.ai[0] <= homerandom)
            {
                Projectile.velocity.X *= 0.9f;
                Projectile.velocity.Y *= 0.9f;
            }

            if (Projectile.ai[0] == homerandom)
            {
                for (int i = 0; i < 20; i++)
                {
                    int dust2 = Dust.NewDust(Projectile.Center - Projectile.velocity, Projectile.width, Projectile.height, DustID.Sandnado, 0f, 0f, 50, default, 1f);
                    Main.dust[dust2].noGravity = true;
                }
            }

            if (Projectile.ai[0] >= homerandom && Projectile.ai[0] <= homerandom + 45)
            {
                for (int i = 0; i < 200; i++)
                {
                    Player target = Main.player[i];
                    //If the npc is hostile

                    //Get the shoot trajectory from the Projectile and target
                    float shootToX = target.Center.X - Projectile.Center.X;
                    float shootToY = target.Center.Y - Projectile.Center.Y;
                    float distance = (float)Math.Sqrt((double)(shootToX * shootToX + shootToY * shootToY));

                    //If the distance between the live targeted npc and the Projectile is less than 480 pixels
                    if (distance < 2000f && target.active)
                    {
                        distance = 0.5f / distance;

                        //Multiply the distance by a multiplier proj faster
                        shootToX *= distance * 10f;
                        shootToY *= distance * 10f;

                        //Set the velocities to the shoot values
                        Projectile.velocity.X = shootToX;
                        Projectile.velocity.Y = shootToY;
                    }
                }
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffType<SandBlasted>(), 5 * 60);
        }
    }

    public class SandDrip : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand");
        }

        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200 * 60;
        }

        public override void AI()
        {
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Sand, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            Main.dust[dust].noGravity = true; //this make so the dust has no gravity
            Main.dust[dust].velocity *= -0.3f;

            int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Sandnado, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 2f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            Main.dust[dust2].noGravity = true; //this make so the dust has no gravity
            Main.dust[dust2].velocity *= -0.3f;

            int dust3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Sandstorm, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            Main.dust[dust3].noGravity = true; //this make so the dust has no gravity
            Main.dust[dust3].velocity *= -0.3f;

            int dust4 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.SandstormInABottle, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            Main.dust[dust4].noGravity = true; //this make so the dust has no gravity
            Main.dust[dust4].velocity *= -0.3f;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffType<SandBlasted>(), 5 * 60);
        }
    }
}