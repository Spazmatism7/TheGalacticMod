using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.ThrowingClass.Weapons.Axes
{
	internal class MeteorThrowingAxe : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 18;
			Item.rare = ItemRarityID.Green;
			Item.width = 30;
			Item.height = 30;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<MeteorAxe>();
			Item.shootSpeed = 8f;
			Item.DamageType = DamageClass.Throwing;

			// Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools
			Item.noUseGraphic = true;
			Item.consumable = true;
			Item.maxStack = 999;
			Item.noMelee = true;
			Item.autoReuse = true;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(copper: 75);
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<MeteorThrowingAxe>(), 15);
			recipe.AddRecipeGroup("Wood");
			recipe.AddIngredient(ItemID.MeteoriteBar);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}

	internal class MeteorAxe : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Meteor Throwing Axe");
		}

		public override void SetDefaults()
		{
			Projectile.width = 28;
			Projectile.height = 28;
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