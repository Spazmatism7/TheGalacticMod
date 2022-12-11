using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Projectiles;
using Terraria.Audio;

namespace GalacticMod.Items.Hardmode.Mage
{
	public class FrostflareBolt : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Casts a orb of frostfire");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 50;
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
			Item.shoot = ModContent.ProjectileType<Frostflare>();
			Item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<FrostflareBolt>());
			recipe.AddIngredient(Mod, "FireBolt");
			recipe.AddIngredient(Mod, "FrostBolt");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class Frostflare : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 4;
			Projectile.timeLeft = 600;
			Projectile.alpha = 190; // How transparent to draw this projectile. 0 to 255. 255 is completely transparent.
		}

		public override void AI()
		{
			Projectile.velocity.Y += Projectile.ai[0];
			int fdust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Ice, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fdust].noGravity = true; //this make so the dust has no gravity
			Main.dust[fdust].velocity *= -0.3f;

			int fdust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.IcyMerman, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 2f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fdust2].noGravity = true; //this make so the dust has no gravity
			Main.dust[fdust2].velocity *= -0.3f;

			int fdust3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Frost, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fdust3].noGravity = true; //this make so the dust has no gravity
			Main.dust[fdust3].velocity *= -0.3f;

			int fdust4 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.FrostStaff, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fdust4].noGravity = true; //this make so the dust has no gravity
			Main.dust[fdust4].velocity *= -0.3f;

			int fdust5 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.FrostDaggerfish, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fdust5].noGravity = true; //this make so the dust has no gravity
			Main.dust[fdust5].velocity *= -0.3f;

			int fdust6 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.FrostHydra, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fdust6].noGravity = true; //this make so the dust has no gravity
			Main.dust[fdust6].velocity *= -0.3f;

			int fidust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Torch, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fidust].noGravity = true; //this make so the dust has no gravity
			Main.dust[fidust].velocity *= -0.3f;

			int fidust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.FlameBurst, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 2f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fidust2].noGravity = true; //this make so the dust has no gravity
			Main.dust[fidust2].velocity *= -0.3f;

			int fidust3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Flare, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fidust3].noGravity = true; //this make so the dust has no gravity
			Main.dust[fidust3].velocity *= -0.3f;

			int fidust4 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Lava, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fidust4].noGravity = true; //this make so the dust has no gravity
			Main.dust[fidust4].velocity *= -0.3f;

			int fidust5 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.LavaMoss, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fidust5].noGravity = true; //this make so the dust has no gravity
			Main.dust[fidust5].velocity *= -0.3f;

			int fidust6 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.SolarFlare, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[fidust6].noGravity = true; //this make so the dust has no gravity
			Main.dust[fidust6].velocity *= -0.3f;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
			{
				Projectile.Kill();
			}
			else
			{
				Projectile.ai[0] += 0.1f;
				if (Projectile.velocity.X != oldVelocity.X)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}
				Projectile.velocity *= 0.75f;
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.ai[0] += 0.1f;
			Projectile.velocity *= 0.75f;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(BuffID.Frostburn, (1 + 1/2) * 60);
			target.AddBuff(BuffID.OnFire, 3 * 60);
		}
	}
}