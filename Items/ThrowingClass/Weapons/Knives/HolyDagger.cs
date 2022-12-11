using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.ThrowingClass.Weapons.Knives
{
	internal class HolyDagger : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 46;
			Item.rare = ItemRarityID.Pink;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 20;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 14f;
			Item.useAnimation = 20;
			Item.shoot = ModContent.ProjectileType<HolyDaggerProjectile>();
			Item.shootSpeed = 8f;
			Item.DamageType = DamageClass.Throwing;

			// Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools
			Item.noUseGraphic = true;
			Item.consumable = true;
			Item.maxStack = 999;
			Item.noMelee = true;
			Item.autoReuse = true;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(silver: 5);
		}

		public override void AddRecipes()
        {
			Recipe recipe = Recipe.Create(ModContent.ItemType<HolyDagger>());
			recipe.AddIngredient(ItemID.ThrowingKnife);
			recipe.AddIngredient(ItemID.UnicornHorn);
			recipe.AddIngredient(ItemID.PixieDust, 4);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	internal class HolyDaggerProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Holy Dagger");
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