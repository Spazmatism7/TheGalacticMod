using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Desert
{
	public class SereBow : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 28;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 28;
			Item.height = 70;
			Item.useTime = 20; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 20; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = 10000; // how much the item sells for (measured in copper)
			Item.rare = ItemRarityID.Green; // the color that the item's name will be in-game
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true; // if you can hold click to automatically use it again
			Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
			Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<SereBow>());
			recipe.AddIngredient(Mod, "TorridBar", 14);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
