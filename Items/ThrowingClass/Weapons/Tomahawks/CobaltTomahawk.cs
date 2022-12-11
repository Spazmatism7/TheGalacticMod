﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.ThrowingClass.Weapons.Tomahawks
{
	internal class CobaltTomahawk : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 38;
			Item.rare = ItemRarityID.LightRed;
			Item.width = 44;
			Item.height = 38;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 20;
			Item.shoot = ProjectileType<CobaltAxe>();
			Item.shootSpeed = 8f;
			Item.DamageType = DamageClass.Throwing;

			// Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.autoReuse = true;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(gold:1);
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<CobaltTomahawk>());
			recipe.AddRecipeGroup("Wood", 20);
			recipe.AddIngredient(ItemID.CobaltBar, 25);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	internal class CobaltAxe : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cobalt Tomahawk");
		}

		public override void SetDefaults()
		{
			Projectile.width = 44;
			Projectile.height = 38;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.aiStyle = 0;
			Projectile.timeLeft = 600;
			Projectile.DamageType = DamageClass.Throwing;
		}

		public override void AI()
		{
			Projectile.rotation += 1.57f / 6;
            Projectile.velocity.Y += .075f;
        }
	}
}