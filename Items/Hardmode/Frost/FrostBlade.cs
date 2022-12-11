using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Frost
{
	public class FrostBlade: ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Brr");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 58;
			Item.DamageType = DamageClass.Melee; //melee weapon
			Item.width = 58;
			Item.height = 64;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 1000; //sell value
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<FrostBlade>());
			recipe.AddIngredient(Mod, "FrostBar", 17);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}