using GalacticMod.Projectiles;
using GalacticMod.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Items.PreHM.Mage
{
	public class MahoganyStave : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 12;
			Item.width = 38;
			Item.height = 38;
			Item.rare = ItemRarityID.Blue;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5;
			Item.value = 10000;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<MahoganyFlower>();
			Item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<MahoganyStave>());
			recipe.AddIngredient(ItemID.RichMahogany, 20);
			recipe.AddIngredient(ItemID.NaturesGift);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}