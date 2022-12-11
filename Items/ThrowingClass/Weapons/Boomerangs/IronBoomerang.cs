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
	internal class IronBoomerang : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Throwing;

			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 9000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.noUseGraphic = true;
			Item.width = 18;
			Item.height = 32;

			//item.autoReuse = true;
			Item.shoot = ProjectileType<IronBoomerangProj>();
			Item.shootSpeed = 13f;
			Item.channel = true;
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<IronBoomerang>());
			recipe.AddIngredient(ItemID.IronBar, 12);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}

	internal class IronBoomerangProj : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.CloneDefaults(182);
			AIType = 182;
		}
	}
}
