using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;

namespace GalacticMod.Items.Hardmode.Frost
{
	public class ChristmasCutter : ModItem
	{
		int shootNow;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Merry Christmas!!!" +
                "\nDrops a present from the sky every third swing");
			//Tooltip.SetDefault("Launches a present from the sky the explodes into a random projectile");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 62;
			Item.DamageType = DamageClass.Melee; //melee weapon
			Item.width = 30;
			Item.height = 38;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 1000; //sell value
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
			Item.shoot = ProjectileType<Present>();
			Item.shootSpeed = 8f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (shootNow == 2)
			{
				Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
				float ceilingLimit = target.Y;
				if (ceilingLimit > player.Center.Y - 200f)
				{
					ceilingLimit = player.Center.Y - 200f;
				}
				position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
				Vector2 heading = target - position;

				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}

				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}

				heading.Normalize();
				heading *= velocity.Length();
				heading.Y += Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, position, heading, type, damage, knockback, player.whoAmI, 1f, ceilingLimit);

				shootNow = 0;
			}
			else
				shootNow++;

			return false;
		}

        public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<ChristmasCutter>());
			recipe.AddIngredient(Mod, "FrostBar", 17);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class Present : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Present");
		}

		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 24;
			Projectile.aiStyle = 2;
			Projectile.friendly = true;
			Projectile.penetrate = 3;
			Projectile.DamageType = DamageClass.Melee;
		}

		int type;

		public void ModifyShootStats()
		{
			type = Main.rand.Next(new int[] { ProjectileID.Bullet, ProjectileID.ExplosiveBullet, ProjectileID.ExplosiveBunny, ProjectileID.Explosives, ProjectileID.ConfettiMelee });
		}

		public override void Kill(int timeLeft)
		{
			float numberProjectiles = 1;
			float rotation = MathHelper.ToRadians(180);

			for (int i = 0; i < numberProjectiles; i++)
			{
				float speedX = 4f;
				float speedY = 0f;
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles)));
				Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, (int)(Projectile.damage), Projectile.knockBack, Projectile.owner);
			}
			
			SoundEngine.PlaySound(SoundID.Item, Projectile.position);
		}
	}
}