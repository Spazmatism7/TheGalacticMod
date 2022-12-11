using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Nautilus
{
	public class NautilusBow : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Converts wooden arrows to coral arrows");
        }

		public override void SetDefaults()
		{
			Item.damage = 21;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 30;
			Item.height = 42;
			Item.useTime = 20; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 20; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = 10000; // how much the item sells for (measured in copper)
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true; // if you can hold click to automatically use it again
			Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 16f; // the speed of the projectile (measured in pixels per frame)
			Item.useAmmo = AmmoID.Arrow; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<NautilusBow>());
			recipe.AddIngredient(ItemID.Seashell, 3); //Seashells
			recipe.AddIngredient(ItemID.Coral); //Coral
			recipe.AddIngredient(ItemID.Starfish); //Starfish
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = ProjectileType<CoralArrow>();
			}
		}
	}
}