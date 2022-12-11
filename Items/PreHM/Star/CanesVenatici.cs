using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;

namespace GalacticMod.Items.PreHM.Star
{
	internal class CanesVenatici : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 23;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 10;
			Item.width = 36;
			Item.height = 36;
			Item.rare = ItemRarityID.Green;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5;
			Item.value = 10000;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<CanesVenaticiProj>();
			Item.shootSpeed = 9.5f * 3;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(11);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(source, position, perturbedSpeed, type, damage / 3, knockback, player.whoAmI);
			}
			SoundEngine.PlaySound(SoundID.Item, player.Center);
			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<CanesVenatici>());
			recipe.AddIngredient(Mod, "Stardust", 20);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}

	public class CanesVenaticiProj : ModProjectile
	{
		public override string Texture => $"GalacticMod/Items/PreHM/Star/StarProjectile";

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Canes Venatici");
		}

		public override void SetDefaults()
		{
			Projectile.width = 90;
			Projectile.height = 34;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.light = 1f;
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