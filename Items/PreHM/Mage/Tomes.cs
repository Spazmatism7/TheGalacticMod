using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Projectiles;

namespace GalacticMod.Items.PreHM.Mage
{
	public class FireBolt : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Casts a ball of Flame");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 16;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 8;
			Item.width = 24;
			Item.height = 32;
			Item.noMelee = true;
			Item.useTurn = true; //player can turn while animation is happening
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 5;
			Item.value = 10000;
			Item.UseSound = SoundID.Item20;
			Item.rare = ItemRarityID.Green;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<FireBlast>();
			Item.shootSpeed = 16f;
		}

        public override void AddRecipes()
        {
			Recipe recipe = Recipe.Create(ItemType<FireBolt>());
			recipe.AddIngredient(ItemID.HellstoneBar, 6);
			recipe.AddIngredient(ItemID.Fireblossom, 4);
			recipe.AddIngredient(ItemID.Book);
			recipe.AddTile(TileID.Bookcases);
			recipe.Register();
		}
    }

	public class FrostBolt : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Casts a ball of Frost");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 8;
			Item.width = 24;
			Item.height = 32;
			Item.noMelee = true;
			Item.useTurn = true; //player can turn while animation is happening
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 5;
			Item.value = 10000;
			Item.UseSound = SoundID.Item20;
			Item.rare = ItemRarityID.Green;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<IceBlast>();
			Item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<FrostBolt>());
			recipe.AddIngredient(ItemID.IceBlock, 30);
			recipe.AddIngredient(ItemID.Shiverthorn, 4);
			recipe.AddIngredient(ItemID.Book);
			recipe.AddTile(TileID.Bookcases);
			recipe.Register();
		}
	}
}