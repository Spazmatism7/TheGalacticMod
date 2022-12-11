using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.Audio;
using GalacticMod.Assets.Rarities;
using Terraria.DataStructures;

namespace GalacticMod.Items.PostML.Galactic
{
	internal class GalacticFireRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Tooltip.SetDefault("Right click fires dual beams of lightning");
			DisplayName.SetDefault("Galactic Flare Staff");
		}

		public override void SetDefaults()
		{
			Item.damage = 290;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 12;
			Item.width = 58;
			Item.height = 58;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = ModContent.RarityType<GalacticRarity>();
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<GalacticFlame>();
			Item.shootSpeed = 20f;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2) //Right Click
			{
				float numberProjectiles = 2;
				float rotation = MathHelper.ToRadians(3);
				SoundEngine.PlaySound(SoundID.NPCHit1, player.Center);
				Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 35f;

				if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
				{
					position += muzzleOffset;
				}
				for (int j = 0; j < numberProjectiles; j++)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, j / (numberProjectiles)));
					float ai = Main.rand.Next(100);
					int projID = Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y),
						ModContent.ProjectileType<GalacticLightning>(), damage, .5f, Main.myPlayer, perturbedSpeed.ToRotation(), ai);
					Main.projectile[projID].DamageType = DamageClass.Magic;
				}

				for (int i = 0; i < 10; i++)
				{
					float speedY = -2f;
					Vector2 dustspeed = new Vector2(0, speedY).RotatedByRandom(MathHelper.ToRadians(360));
					int dust2 = Dust.NewDust(player.Center + muzzleOffset * 1.6f, 0, 0, DustID.Electric, dustspeed.X, dustspeed.Y, 229, default, 1.5f);
					Main.dust[dust2].noGravity = true;
					Main.dust[dust2].scale = 1f;
				}
			}
			else //left Click
			{
				Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			}
			return false;
		}
	}

	internal class GalacticFlame : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 600;
			Projectile.light = 3f;
		}

		public override void AI()
		{
			Projectile.velocity.Y += Projectile.ai[0];

			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);

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
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.ai[0] += 0.1f;
			Projectile.velocity *= 0.75f;
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 16; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, 181);
				d.frame.Y = 0;
				d.velocity *= 2;
			}

			Vector2 perturbedSpeed = new Vector2(0, -7).RotatedByRandom(MathHelper.ToRadians(360));
			Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<GalacticBoom>(), Projectile.damage, 0, Projectile.owner);
			//Projectile.GetProjectileSource_FromThis()
		}
	}

	public class GalacticBoom : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.aiStyle = 1;
			Projectile.light = 0.1f;
			Projectile.friendly = true;
			Projectile.timeLeft = 3;
			Projectile.penetrate = -1;
			Projectile.tileCollide = true;
			Projectile.scale = 1f;
			Projectile.knockBack = 0;
			Projectile.aiStyle = -1;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		public override void AI()
		{
			Projectile.velocity.X = 0;
			Projectile.velocity.Y = 0;
			Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GiantCursedSkullBolt, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);

			if (Main.rand.NextBool(1))     //this defines how many dust to spawn
			{
				int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GiantCursedSkullBolt, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);

				Main.dust[dust].noGravity = true; //this make so the dust has no gravity
				Main.dust[dust].velocity *= 0.5f;
				int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GiantCursedSkullBolt, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);
			}
			if (Projectile.owner == Main.myPlayer && Projectile.timeLeft == 3)
			{
				Projectile.velocity.X = 0f;
				Projectile.velocity.Y = 0f;
				Projectile.tileCollide = false;

				// change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
				Projectile.position = Projectile.Center;

				Projectile.width = 200;
				Projectile.height = 200;
				Projectile.Center = Projectile.position;
				Projectile.knockBack = 6f;
			}
		}

		public override bool? CanDamage()
		{
			if (Projectile.timeLeft > 3)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item, Projectile.position);

			for (int i = 0; i < 50; i++)
			{
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.GiantCursedSkullBolt);
				dust.noGravity = true;
				dust.scale = 2f;
				dust.velocity *= 5f;
				dust.fadeIn = 1f;
			}

			for (int i = 0; i < 50; i++)
			{
				float speedY = -2f;

				Vector2 perturbedSpeed = new Vector2(0, speedY).RotatedByRandom(MathHelper.ToRadians(360));
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.GiantCursedSkullBolt, perturbedSpeed.X, perturbedSpeed.Y);

				dust.noGravity = true;
				dust.scale = 1.5f;
				dust.velocity *= 4f;
				dust.fadeIn = 1f;
			}

			for (int i = 0; i < 50; i++)
			{
				Dust dust;
				Vector2 position = Projectile.position;
				dust = Main.dust[Terraria.Dust.NewDust(position, Projectile.width, Projectile.height, DustID.GiantCursedSkullBolt, 0f, 0f, 0, default, 1f)];
				dust.noGravity = true;
				dust.scale = 1f;
			}
		}
	}
}