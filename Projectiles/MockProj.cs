using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace GalacticMod.Projectiles
{
	public class Starfury2 : ModProjectile
	{
		public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Starfury}";

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Starfury);
			AIType = ProjectileID.Starfury;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 20 * 60;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }
	}

	public class Starfury3 : ModProjectile
	{
		public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.StarWrath}";

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.StarWrath);
			AIType = ProjectileID.StarWrath;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 20 * 60;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }
	}

	public class MockRazorblade : ModProjectile
	{
		public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Typhoon}";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Typhoon");
			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Typhoon);
			AIType = ProjectileID.Typhoon;
			Projectile.timeLeft = 20 * 60;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
	}

	public class RockBoulder : ModProjectile
	{
		public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Boulder}";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boulder");
		}

		public override void SetDefaults()
        {
            Projectile.width = 31;
            Projectile.height = 31;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = 1;
            Projectile.light = 0.1f;

            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 300;
            Projectile.penetrate = 4;

            Projectile.tileCollide = true;
            Projectile.aiStyle = 14;
        }

		public override bool OnTileCollide(Vector2 oldVelocity) => false;
	}

	public class FrostBlast : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 48;
			Projectile.height = 48;
			Projectile.timeLeft = 15 * 60;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.light = 1f;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 5;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Frostburn, 420);
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Frostburn, 420);
		}
	}

	public class FrostBlastM : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frost Blast");
		}

		public override void SetDefaults()
		{
			Projectile.width = 48;
			Projectile.height = 48;
			Projectile.timeLeft = 7 * 60;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.light = 1f;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 5;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Frostburn, 420);
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Frostburn, 420);
		}
	}

	public class MockFireball : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fireball");
		}

		public override void SetDefaults()
		{
			Projectile.width = 48;
			Projectile.height = 48;
			Projectile.timeLeft = 7 * 60;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = false;
			Projectile.light = 1f;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.penetrate = 5;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 420);
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 420);
		}
	}
}