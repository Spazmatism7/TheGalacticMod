using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;

namespace GalacticMod.Items.PreHM.Truffle
{
	public class TruffleBow : ModItem
	{
		public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Converts basic arrows into exploding Truffle arrows" +
                "\nTruffle arrows only deal 2/3 damage, but explode into truffles");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 18;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 28;
			Item.height = 70;
			Item.useTime = 20; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 20; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = 10000; // how much the item sells for (measured in copper)
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true; // if you can hold click to automatically use it again
			Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
			Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly || type == ProjectileID.UnholyArrow)
            {
                type = ProjectileType<TruffleArrow>();
                damage = 2 * (damage / 3);
            }
        }

        public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<TruffleBow>());
			recipe.AddIngredient(ItemID.GlowingMushroom, 30);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}

    public class TruffleArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Arrow");
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.width = 16;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();

            return false;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;

            for (int i = 0; i < 2; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X - 5, Projectile.Center.Y - 5), 10, 10, DustID.UnusedWhiteBluePurple, 0f, 0f, 100, default, 1.5f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 0.5f;
                Main.dust[dustIndex].fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
            }
            int dustIndex2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default, 1f);
            Main.dust[dustIndex2].scale = 0.1f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex2].fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex2].noGravity = true;
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            for (int i = 0; i < 25; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Smoke);
                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.velocity *= 4f;
                dust.fadeIn = 1f;
            }

            for (int i = 0; i < 25; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.UnusedWhiteBluePurple);
                dust.noGravity = true;
                dust.scale = 2f;
                dust.velocity *= 4f;
                dust.fadeIn = 1f;
            }

            if (Projectile.owner == Main.myPlayer)
            {
                int numberProjectiles = 3 + Main.rand.Next(3);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(0, -12).RotatedByRandom(MathHelper.ToRadians(360));

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y),
                        ProjectileType<Truffle>(), (int)(Projectile.damage * 0.4f), 1, Projectile.owner);
                }
            }
        }
    }

    public class Truffle : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Arrow");
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.CloneDefaults(131);
            AIType = 131;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 50;
            Projectile.tileCollide = false;
        }

        int damagetime;

        public override void AI()
        {
            Projectile.penetrate = 1;
            damagetime++;
        }

        public override bool? CanDamage()
        {
            if (damagetime < 8)
                return false;
            else
                return true;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple);
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Color.White;
            color.A = 50;
            return color;
        }
    }
}
