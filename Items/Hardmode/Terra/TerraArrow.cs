using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Buffs;

namespace GalacticMod.Items.Hardmode.Terra
{
    public class TerraArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.width = 14;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = 4;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.TerraBlade, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d.noGravity = true;
            }
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Clentaminator_Green, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d.noGravity = true;
            }
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Chlorophyte, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d.noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.TerraBlade);
                d.velocity *= 2;
                d.noGravity = true;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Clentaminator_Green);
                d.velocity *= 2;
                d.noGravity = true;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Chlorophyte);
                d.velocity *= 2;
                d.noGravity = true;
            }

            int terraSplosion = Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.velocity - Projectile.velocity, ProjectileType<TerraSplosion>(), Projectile.damage, 0, Projectile.owner);
        }
    }

    public class TerraBurnArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.width = 14;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = 4;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.TerraBlade, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d.noGravity = true;
            }
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Clentaminator_Green, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d.noGravity = true;
            }
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Chlorophyte, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d.noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.TerraBlade);
                d.velocity *= 2;
                d.noGravity = true;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Clentaminator_Green);
                d.velocity *= 2;
                d.noGravity = true;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Chlorophyte);
                d.velocity *= 2;
                d.noGravity = true;
            }      
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffType<TerraBurn>(), 15 * 60);
        }

        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit) => target.AddBuff(BuffType<TerraBurn>(), 15 * 60);
    }
}