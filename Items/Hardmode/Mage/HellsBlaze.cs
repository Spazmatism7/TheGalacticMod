using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using GalacticMod.Projectiles;

namespace GalacticMod.Items.Hardmode.Mage
{
	public class HellsBlaze : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hell's Blaze");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 10;
			Item.width = 26;
			Item.height = 26;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.knockBack = 8;
			Item.value = Item.sellPrice(silver: 50);
			Item.rare = ItemRarityID.LightRed;
			Item.autoReuse = false;
			Item.UseSound = SoundID.Item9;
			Item.shootSpeed = 10f;
			Item.shoot = ProjectileType<HellsBlazeProj>();
		}
	}

	public class HellsBlazeProj : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
            DisplayName.SetDefault("Hell's Blaze");
        }

        public override void SetDefaults()
		{
			Projectile.width = 24;
			Projectile.height = 14;
			Projectile.aiStyle = 28;
			Projectile.penetrate = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = true;
			Projectile.light = 1f;
            Projectile.timeLeft = 80;
		}

		public override void AI()
		{
			Projectile.velocity.Y += Projectile.ai[0];

			Lighting.AddLight(Projectile.Center, Color.Orange.ToVector3() * 0.78f);

			Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
			}
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation += MathHelper.Pi;
			}

            if (++Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }

            if (Main.rand.NextBool(2))     //this defines how many dust to spawn
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.FlameBurst, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                Main.dust[dust].velocity *= -0.3f;
                int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                Main.dust[dust2].noGravity = true; //this make so the dust has no gravity
                Main.dust[dust2].velocity *= -0.3f;
            }
        }

		public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            for (int i = 0; i < 16; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch);
                d.frame.Y = 0;
                d.velocity *= 2;
            }

            for (int i = 0; i < 16; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Lava);
                d.frame.Y = 0;
                d.velocity *= 2;
            }

            Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.velocity - Projectile.velocity, ProjectileType<FieryExplosion>(), Projectile.damage / 2, 0, Projectile.owner);
            //Projectile.GetProjectileSource_FromThis()
        }
    }

    public class FieryExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.friendly = true;
            Projectile.timeLeft = 20;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.scale = 1.5f;
            Projectile.aiStyle = -1;
            Projectile.alpha = 0;
            DrawOffsetX = 25;
            DrawOriginOffsetY = 25;
            Projectile.light = 0.9f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 2) // This will change the sprite every 8 frames (0.13 seconds). Feel free to experiment.
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Color.SandyBrown;
            return color;
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 420);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 420);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}