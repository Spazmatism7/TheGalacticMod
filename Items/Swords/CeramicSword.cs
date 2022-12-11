using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Swords
{
	public class CeramicSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A Ceramic Sword, what did you think it was?");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Melee;
			Item.width = 56;
			Item.height = 64;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 1000; //sell value
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<CeramicSword>());
			recipe.AddIngredient(ItemID.ClayBlock, 10); //Clay
			recipe.AddTile(TileID.Furnaces); //Furnace
			recipe.Register();
		}
	}
}