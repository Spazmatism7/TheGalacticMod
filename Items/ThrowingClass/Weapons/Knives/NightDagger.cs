using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Items.ThrowingClass.Weapons.Misc;
using Microsoft.Xna.Framework;

namespace GalacticMod.Items.ThrowingClass.Weapons.Knives
{
	public class NightDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 46;
			Item.rare = ItemRarityID.Orange;
			Item.width = 10;
			Item.height = 24;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<NightDaggerP>();
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
			Recipe recipe = Recipe.Create(ItemType<NightDagger>());
			recipe.AddRecipeGroup("GalacticMod:EvilKnife", 200);
			recipe.AddIngredient(ItemType<BeeStinger>());
			recipe.AddIngredient(ItemID.BoneDagger, 300);
			recipe.AddIngredient(ItemType<HellfireShuriken>(), 400);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}

	public class NightDaggerP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night's Dagger");
		}

		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 30;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 600;
			Projectile.DamageType = DamageClass.Throwing;
		}

		public override void AI()
		{
			Projectile.rotation += 1.57f / 3;
			Projectile.velocity.Y += .1f;

			if (Main.rand.NextBool())
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Shadowflame, 0f, 0f, 100, default, 1f);
				Main.dust[dustIndex].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[dustIndex].noGravity = true;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => target.AddBuff(BuffID.ShadowFlame, 600);
	}
}