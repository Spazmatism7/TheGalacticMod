using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Projectiles;
using Terraria.DataStructures;
using Terraria.Audio;

namespace GalacticMod.Items.Hardmode.Terra
{
	public class TerraBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Tooltip.SetDefault("The Balance of Light and Dark");
		}

		public override void SetDefaults()
		{
			Item.damage = 78;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 38;
			Item.height = 86;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Lime;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.VilePowder;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Arrow;
        }

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<TerraBow>());
			recipe.AddIngredient(Mod, "Shadowshot");
			recipe.AddIngredient(Mod, "Aslan");
			recipe.AddIngredient(Mod, "BarOfLife", 7);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.UnholyArrow || type == ProjectileID.WoodenArrowFriendly || type == ProjectileID.JestersArrow || type == ProjectileID.IchorArrow
                || type == ProjectileID.HellfireArrow)
            {
                type = ProjectileType<TerraArrow>();
            }
            if (type == ProjectileID.FireArrow || type == ProjectileID.CursedArrow || type == ProjectileID.FrostArrow || type == ProjectileID.ShadowFlameArrow)
            {
                type = ProjectileType<TerraBurnArrow>();
            }
        }
	}
}