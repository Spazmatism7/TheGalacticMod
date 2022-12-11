using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.ThrowingClass.Weapons.Knives
{
	public class LuminiteDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 100;
			Item.rare = ItemRarityID.Lime;
			Item.width = 10;
			Item.height = 24;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<LuminiteDaggerP>();
			Item.shootSpeed = 8f;
			Item.DamageType = DamageClass.Throwing;

			// Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.autoReuse = true;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(silver: 5);
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<LuminiteDagger>());
			recipe.AddIngredient(ItemID.ThrowingKnife, 100);
			recipe.AddIngredient(ItemID.LunarBar, 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class LuminiteDaggerP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Luminite Dagger");
		}

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 600;
			Projectile.DamageType = DamageClass.Throwing;
		}

		public override void AI()
		{
			Projectile.rotation += 1.57f / 6;
			Projectile.velocity.Y += .1f;
		}
    }
}