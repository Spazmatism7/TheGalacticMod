using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Projectiles;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Rarities;
using Terraria.DataStructures;

namespace GalacticMod.Items.PostML.Hellfire
{
	public class HellfireBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This blade is made of pure flame.");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
            Item.width = 54;
            Item.height = 54;
			Item.scale = 1.25f;
            Item.damage = 202;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTime = 18;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 100000; //sell value
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
            Item.rare = RarityType<HellfireRarity>();
            Item.shootSpeed = 12f;
			Item.shoot = ProjectileType<HellfireWave>();
        }

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<HellfireBlade>());
			recipe.AddIngredient(Mod, "HellfireBar", 16);
			recipe.AddTile(TileID.LunarCraftingStation); //Ancient Manipulator
			recipe.Register();
		}
    }
}