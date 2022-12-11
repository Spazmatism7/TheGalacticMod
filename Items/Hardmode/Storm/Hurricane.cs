using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Storm
{
	internal class Hurricane : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.noMelee = true;

			Item.useTime = 20;
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
			Item.crit = 1;

			Item.shoot = ProjectileType<HurricaneP>();
			Item.shootSpeed = 12f;
			Item.channel = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<Hurricane>());
			recipe.AddIngredient(Mod, "StormBar", 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
	}

	internal class HurricaneP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hurricane");
		}

		public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.CloneDefaults(182);
			AIType = 182;
		}
	}
}
