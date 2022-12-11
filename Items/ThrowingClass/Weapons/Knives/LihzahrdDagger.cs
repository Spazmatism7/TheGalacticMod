using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.ThrowingClass.Weapons.Knives
{
	public class LihzahrdDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 75;
			Item.rare = ItemRarityID.Lime;
			Item.width = 22;
			Item.height = 40;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<LihzahrdDaggerP>();
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
			Recipe recipe = Recipe.Create(ItemType<LihzahrdDagger>());
			recipe.AddIngredient(ItemID.PoisonedKnife, 50);
			recipe.AddIngredient(ItemID.LihzahrdBrick, 100);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class LihzahrdDaggerP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lihzahrd Dagger");
		}

		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 40;
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

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => target.AddBuff(BuffID.Poisoned, 600);
	}
}