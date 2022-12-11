using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Projectiles;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Brightcore
{
	public class BrightcoreStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires a beam of bouncing energy");
			Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			Item.damage = 44;
			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.mana = 7;
			Item.width = 42;
			Item.height = 42;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 6;
			Item.rare = ItemRarityID.Pink;
			Item.crit = 1;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
			Item.shoot = ProjectileType<BrightcoreBolt>();
			Item.shootSpeed = 8f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<BrightcoreStaff>());
			recipe.AddIngredient(Mod, "Brightcore", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}