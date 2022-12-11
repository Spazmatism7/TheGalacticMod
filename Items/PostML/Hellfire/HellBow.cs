using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Assets.Rarities;

namespace GalacticMod.Items.PostML.Hellfire
{
	public class HellBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Transforms arrows into Hellflame arrows" +
                "\n50% chance not to consume ammo");
			DisplayName.SetDefault("Hellflame Bow");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 190;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 52;
			Item.height = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = RarityType<HellfireRarity>();
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Arrow;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<HellBow>());
			recipe.AddIngredient(Mod, "HellfireBar", 16);
			recipe.AddTile(TileID.LunarCraftingStation); //Ancient Manipulator
			recipe.Register();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.WoodenArrowFriendly || type == ProjectileID.FireArrow || type == ProjectileID.UnholyArrow || type == ProjectileID.BoneArrow || type == ProjectileID.HellfireArrow || type == ProjectileID.CursedArrow || type == ProjectileID.FrostburnArrow)
			{
				type = ProjectileType<HellflameArrow>();
			}
		}

		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .50f;
		}
	}
}