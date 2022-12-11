using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace GalacticMod.Items.Hardmode.Steel
{
	public class SteelDrill : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 20;
			Item.height = 12;
			Item.useTime = 7;
			Item.useAnimation = 25;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.pick = 200;
			Item.tileBoost++;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(0, 22, 50, 0);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<SteelDrillProj>();
			Item.shootSpeed = 30f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<SteelDrill>());
			recipe.AddIngredient(Mod, "SteelBar", 16);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class SteelDrillProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Steel Drill");
		}

		public override void SetDefaults()
		{
			Projectile.width = 26;
			Projectile.height = 46;
			Projectile.aiStyle = 20;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true; //so you can't hit enemies through walls
			Projectile.DamageType = DamageClass.Melee;

			DrawOffsetX = -1;
			DrawOriginOffsetY = 0;
		}

		public override void AI()
		{
			int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 100, default(Color), 1.9f);
			Main.dust[dust].noGravity = true;
		}
	}
}