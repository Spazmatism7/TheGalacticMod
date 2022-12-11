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
	public class FrostStar : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Tooltip.SetDefault("Fires a shattering frost star");
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.rare = ItemRarityID.Blue;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 8f;
			Item.useAnimation = 20;
			Item.shoot = ModContent.ProjectileType<FrostStarP>();
			Item.DamageType = DamageClass.Throwing;
			Item.knockBack = 4.5f;

			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.autoReuse = false;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(copper: 75);
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<FrostStar>());
			recipe.AddIngredient(ItemID.FallenStar, 20);
			recipe.AddIngredient(ItemID.IceBlock, 30);
			recipe.AddRecipeGroup("GalacticMod:EvilBar");
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}

	public class FrostStarP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frost Star");
		}

		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 24;
			Projectile.aiStyle = 2;
			Projectile.friendly = true;
			Projectile.penetrate = 3;
			Projectile.DamageType = DamageClass.Throwing;
		}

		public override void Kill(int timeLeft)
		{
			float numberProjectiles = 5;
			float rotation = MathHelper.ToRadians(180);
			//position += Vector2.Normalize(new Vector2(speedX, speedY)) * 30f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				float speedX = 4f;
				float speedY = 0f;
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles)));
				Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<FrostShards>(), (int)(Projectile.damage), Projectile.knockBack, Projectile.owner);
				//Projectile.GetProjectileSource_FromThis()
			}
			SoundEngine.PlaySound(SoundID.Item, Projectile.Center);
		}
	}

	public class FrostShards : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.tileCollide = true;
			Projectile.aiStyle = 2;
        }
	}
}
