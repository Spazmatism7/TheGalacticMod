using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.PostPlantera
{
	public class SpectreTome : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Tome of the Spectre Curse");
            Tooltip.SetDefault("Fires the reflecting souls of the dead");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.width = 24;
			Item.height = 24;
			Item.useAnimation = 25;
			Item.useTime = 25;
			Item.shootSpeed = 16f;
			Item.knockBack = 2.5f;
			Item.damage = 100;
			Item.rare = ItemRarityID.Yellow;

			Item.DamageType = DamageClass.Magic;
			Item.noMelee = true;
			Item.autoReuse = true;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(silver: 1);
			Item.shoot = ProjectileType<SpectreBolt>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<SpectreTome>());
			recipe.AddIngredient(ItemID.SpectreBar, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class SpectreBolt : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.timeLeft /= 2;
			Projectile.penetrate = 10;
			Projectile.DamageType = DamageClass.Magic;
		}

		public override void AI()
		{
			Projectile.velocity.Y += Projectile.ai[0];
			int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.SpectreStaff, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust].velocity *= -0.3f;

			int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.DungeonSpirit, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 2f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust2].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust2].velocity *= -0.3f;
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

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(BuffType<Buffs.SpiritCurse>(), 7 * 60);
		}

		public override void ModifyHitPvp(Player target, ref int damage, ref bool crit) => target.AddBuff(BuffType<Buffs.SpiritCurse>(), 7 * 60);
	}
}

