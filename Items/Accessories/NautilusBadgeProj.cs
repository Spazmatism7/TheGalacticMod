using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using System;
using GalacticMod.Assets.Systems;
using GalacticMod.Buffs;

namespace GalacticMod.Projectiles
{
    public class NautilusBadgeProj : ModProjectile //orbit
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nautilus Orbit");
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.light = 0.1f;
            Projectile.friendly = true;
            Projectile.timeLeft = 999999999;
            Projectile.penetrate = -1;

            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.knockBack = 0;

            //Projectile.usesIDStaticNPCImmunity = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        bool lineOfSight;
        public override void AI()
        {

            Projectile.velocity.X = 0;
            Projectile.velocity.Y = 0;
            if (!Main.dedServ)
            {
                Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0.1f) / 255f);   //this is the light colors
            }
            var player = Main.player[Projectile.owner];

            if (Main.rand.NextBool(2))     //this defines how many dust to spawn
            {
                int dust = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.Water, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.75f);
                Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                Main.dust[dust].velocity *= 0.5f;
                Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), 0, 0, DustID.WaterCandle, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);
            }

            Projectile.rotation += Projectile.direction * -0.2f;
            //Making player variable "p" set as the projectile's owner

            //Factors for calculations
            double deg = ((double)Projectile.ai[1] + 90) * -5; //The degrees, you can multiply Projectile.ai[1] to make it orbit faster, may be choppy depending on the value
            double rad = deg * (Math.PI / 180); //Convert degrees to radians
            double dist = 50; //Distance away from the player

            /*Position the player based on where the player is, the Sin/Cos of the angle times the /
            /distance for the desired distance away from the player minus the projectile's width   /
            /and height divided by two so the center of the projectile is at the right place.     */

            Projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;

            //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
            Projectile.ai[1] += 1f;

            if (player.GetModPlayer<GalacticPlayer>().nautilusBadge == false || player.dead)
            {
                Projectile.Kill();
            }

            lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, player.position, player.width, player.height);

        }
        public override bool? CanDamage()
        {
            if (!lineOfSight)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<SandBlasted>(), 180);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<SandBlasted>(), 180);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void Kill(int timeLeft) { }
    }
}