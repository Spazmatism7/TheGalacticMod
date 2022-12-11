using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using GalacticMod.Projectiles;

namespace GalacticMod.Items.Swords.CosmicEdgePath
{
	public class SacredTide : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Burning Power of the Ocean");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 52;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.autoReuse = true;
			Item.crit = -99;
			Item.DamageType = DamageClass.Melee;
			Item.damage = 10;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 5);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<MockRazorblade>();
			Item.shootSpeed = 2f; // Speed of the projectiles the sword will shoot
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<SacredTide>());
			recipe.AddIngredient(Mod, "NautilusBlade");
			recipe.AddIngredient(ItemID.Muramasa);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}