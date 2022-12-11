using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace GalacticMod.Items.Hardmode.Asteroid
{
	public class AsteroidBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Uses Asteroid Shards as ammo" +
                "\nFires homing Asteroid Shards");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 24;

			Item.autoReuse = true; 
			Item.damage = 50;
			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 4f;
			Item.noMelee = true;
			Item.rare = ItemRarityID.LightRed;
			Item.shootSpeed = 10f;
			Item.useAnimation = 35;
			Item.useTime = 35;
			Item.UseSound = SoundID.Item11;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.value = Item.buyPrice(gold: 1);

			Item.shoot = ModContent.ProjectileType<AsteroidShard>();
			Item.useAmmo = ModContent.ItemType<AsteroidShardItem>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<AsteroidBlaster>());
			recipe.AddIngredient(Mod, "AsteroidBar", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}