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
	internal class LeadBoomerang : ModItem
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
			Item.useStyle = 5;
			Item.knockBack = 0;
			Item.value = 9000;
			Item.rare = 1;
			Item.UseSound = SoundID.Item1;
			Item.noUseGraphic = true;
			Item.width = 18;
			Item.height = 32;

			//item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<LeadBoomerangProj>();
			Item.shootSpeed = 13f;
			Item.channel = true;
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<LeadBoomerang>());
			recipe.AddIngredient(ItemID.LeadBar, 12);
			recipe.AddTile(16);
			recipe.Register();
		}
	}

	internal class LeadBoomerangProj : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.CloneDefaults(182);
			// projectile.aiStyle = 3; This line is not needed since CloneDefaults sets it.
			AIType = 182;
		}
	}
}
