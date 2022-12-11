using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.ThrowingClass.Weapons.Misc
{
	internal class Shotput : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
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
			Item.shoot = ProjectileType<ShotputP>();
			Item.DamageType = DamageClass.Throwing;

			Item.noUseGraphic = true;
			Item.consumable = true;
			Item.maxStack = 999;
			Item.noMelee = true;
			Item.autoReuse = false;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(copper: 75);
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<Shotput>(), 10);
			recipe.AddRecipeGroup("IronBar");
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}

	internal class ShotputP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shotput");
		}

		public override void SetDefaults()
		{
			Projectile.width = 22;
			Projectile.height = 22;
			Projectile.aiStyle = 2;
			Projectile.friendly = true;
			Projectile.penetrate = 4;
			Projectile.DamageType = DamageClass.Throwing;
		}
	}
}
