using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Swords
{
	public class SpaceSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Boomerang!!");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Melee;
			Item.width = 52;
			Item.height = 52;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.rare = ItemRarityID.Green;
			Item.value = 1000; //sell value
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<SpaceSword>());
			recipe.AddIngredient(ItemID.MeteoriteBar, 20); //Meteorite Bars
			recipe.AddTile(TileID.Anvils); //Anvil
			recipe.Register();
		}
	}
}