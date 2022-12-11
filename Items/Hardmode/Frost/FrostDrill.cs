using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Projectiles;

namespace GalacticMod.Items.Hardmode.Frost
{
	public class FrostDrill : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Brrrr");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Melee;
            Item.knockBack = 0.5f;
            Item.width = 56;
			Item.height = 18;

			Item.useTime = 3;
			Item.useAnimation = 20;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.pick = 190;
			Item.tileBoost++;
            Item.autoReuse = true;

            Item.useStyle = ItemUseStyleID.Thrust;
			Item.value = Item.buyPrice(0, 22, 50, 0);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item23;
			Item.shoot = ProjectileType<FrostDrillProjectile>();
			Item.shootSpeed = 40f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<FrostDrill>());
			recipe.AddIngredient(Mod, "FrostBar", 16);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}