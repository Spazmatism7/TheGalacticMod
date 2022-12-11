using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.Audio;

namespace GalacticMod.Items.ThrowingClass.Weapons.Misc
{
	public class HellfireShuriken : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 38;
			Item.rare = ItemRarityID.Orange;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 8f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<HellfireShurikenP>();
			Item.DamageType = DamageClass.Throwing;
			Item.knockBack = 4.5f;

			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.autoReuse = false;
            Item.consumable = true;
            Item.maxStack = 999;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(copper: 75);
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<HellfireShuriken>(), 25);
			recipe.AddIngredient(ItemID.HellstoneBar);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}

    public class HellfireShurikenP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Shuriken");
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.light = 0.1f;
            Projectile.friendly = true;
            Projectile.CloneDefaults(3);
            AIType = 3;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
            {
                Projectile.tileCollide = false;
                // Set to transparent. This projectile technically lives as  transparent for about 3 frames
                Projectile.alpha = 255;
                // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
                Projectile.position = Projectile.Center;

                Projectile.width = 100;
                Projectile.height = 100;
                Projectile.Center = Projectile.position;

                Projectile.velocity.X = 0;
                Projectile.velocity.Y = 0;
                Projectile.knockBack = 3f;
            }
            else
            {
                if (Main.rand.NextBool())
                {
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Flare, 0f, 0f, 100, default, 1f);
                    Main.dust[dustIndex].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
                    Main.dust[dustIndex].noGravity = true;
                }
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.timeLeft > 3)
            {
                Projectile.timeLeft = 3;
            }
            if (Main.rand.NextBool(1)) //the chance
            {
                target.AddBuff(BuffID.OnFire, 600);

            }
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

            for (int i = 0; i < 50; i++)
            {
                Vector2 perturbedSpeed = new Vector2(0, -5f).RotatedByRandom(MathHelper.ToRadians(360));

                int dustIndex = Dust.NewDust(Projectile.Center, 0, 0, DustID.Torch, perturbedSpeed.X, perturbedSpeed.Y, 100, default, 2f);
                Main.dust[dustIndex].noGravity = true;
            }
            /*for (int i = 0; i < 30; i++) //Grey dust circle
            {
                Vector2 perturbedSpeed = new Vector2(0, -2f).RotatedByRandom(MathHelper.ToRadians(360));
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Smoke, perturbedSpeed.X, perturbedSpeed.Y);

                //dust = Main.dust[Terraria.Dust.NewDust(Projectile.Center, 0, 0, 31, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                dust.noGravity = true;
                dust.scale = 2f;
                dust.velocity *= 2f;
            }*/
            for (int i = 0; i < 30; i++) //Grey dust fade
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 0, default, 1f);
                Main.dust[dustIndex].scale = 0.1f + ((float)Main.rand.Next(5) * 0.1f);
                Main.dust[dustIndex].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
                Main.dust[dustIndex].noGravity = true;
            }
        }
    }
}
