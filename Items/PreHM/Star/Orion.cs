using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Star
{
	public class Orion : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 34;
			Item.DamageType = DamageClass.Melee; //melee weapon
			Item.width = 58;
			Item.height = 58;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4.25f;
			Item.value = 1000; //sell value
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening

			Item.shoot = ProjectileType<OrionProj>();
			Item.shootSpeed = 9.5f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<Orion>());
			recipe.AddIngredient(Mod, "Stardust", 20);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}

	public class OrionProj : ModProjectile
	{
		public override string Texture => $"GalacticMod/Items/PreHM/Star/StarProjectile";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orion");
		}

		public override void SetDefaults()
		{
			Projectile.width = 90;
			Projectile.height = 34;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.light = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }

		public override void AI()
		{
			Lighting.AddLight(Projectile.Center, Color.Yellow.ToVector3() * 0.78f);
			Lighting.AddLight(Projectile.Center, Color.Purple.ToVector3() * 0.78f);

			Projectile.velocity.Y += Projectile.ai[0];

			Projectile.direction = Projectile.spriteDirection = Projectile.velocity.X > 0f ? 1 : -1;
			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.velocity.Y > 16f)
			{
				Projectile.velocity.Y = 16f;
			}
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation += MathHelper.Pi;
			}

			int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust].velocity *= -0.3f;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 16; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame);
				d.frame.Y = 0;
				d.velocity *= 2;
			}

			for (int i = 0; i < 16; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.YellowStarDust);
				d.frame.Y = 0;
				d.velocity *= 2;
			}
		}
	}
}