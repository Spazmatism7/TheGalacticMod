using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.ThrowingClass.Weapons.Boomerangs
{
	internal class SteelBoomerang : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Space Sword");
			DisplayName.SetDefault("Boomerang!!");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 69;
			Item.noMelee = true;

			Item.useTime = 40;
			Item.DamageType = DamageClass.Throwing;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 9000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.noUseGraphic = true;
			Item.width = 18;
			Item.height = 32;

			Item.autoReuse = true;
			Item.shoot = ProjectileType<SteelBoomerangProj>();
			Item.shootSpeed = 12f;
			Item.channel = true;
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<SteelBoomerang>());
			recipe.AddIngredient(Mod, "SteelBar", 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	internal class SteelBoomerangProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boomerang!!");
		}

		public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.CloneDefaults(52);
			AIType = 52;
		}
	}
}
