using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Blood
{
	public class BloodBlade : ModItem
	{
		public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Bloody Saber");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 27;
			Item.DamageType = DamageClass.Melee;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 19;
			Item.useAnimation = 19;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 1);
			Item.useStyle = ItemUseStyleID.Swing;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
        {
			Recipe recipe = Recipe.Create(ItemType<BloodBlade>());
			recipe.AddIngredient(Mod, "BloodyDrop", 12);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
