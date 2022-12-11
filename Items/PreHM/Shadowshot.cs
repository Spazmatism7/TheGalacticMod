using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Projectiles;

namespace GalacticMod.Items.PreHM
{
	public class Shadowshot : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Tooltip.SetDefault("Holds the Darkest of Souls");
		}

		public override void SetDefaults()
		{
			Item.damage = 32;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 22;
			Item.height = 44;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.VilePowder;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Arrow;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<Shadowshot>());
			recipe.AddRecipeGroup("GalacticMod:EvilBow");
			recipe.AddIngredient(ItemID.BeesKnees);
			recipe.AddIngredient(ItemID.MoltenFury);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.UnholyArrow || type == ProjectileID.WoodenArrowFriendly)
			{
				type = ProjectileID.ShadowFlameArrow;
			}
			if (type == ProjectileID.FireArrow || type == ProjectileID.JestersArrow || type == ProjectileID.FrostArrow)
            {
				type = ProjectileID.HellfireArrow;
			}
		}
	}
}