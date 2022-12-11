using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.Audio;

namespace GalacticMod.Items.Hardmode.PostPlantera
{
	public class SpiritsWrath : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spirit's Wrath");
			Tooltip.SetDefault("Fires the heat-seaking souls of the dead");
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.reuseDelay = 10;
			Item.knockBack = 7f;
			Item.width = 16;
			Item.height = 16;
			Item.damage = 80;
			Item.UseSound = SoundID.DD2_BetsysWrathShot;
			Item.mana = 14;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(0, 5);
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.autoReuse = true;

			Item.shoot = ModContent.ProjectileType<SpiritsWrathP>();
			Item.shootSpeed = 14f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<SpiritsWrath>());
			recipe.AddIngredient(ItemID.SpectreBar, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class SpiritsWrathP : ModProjectile
	{
		public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Spirit's Wrath");
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = 28;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 10;
			Projectile.timeLeft = 10 * 60;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.light = 1f;
		}

		public override void Kill(int timeLeft)
		{
			Vector2 perturbedSpeed = new Vector2(0, -7).RotatedByRandom(MathHelper.ToRadians(360));
			Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<SpiritsWrathP2>(), Projectile.damage - 10, 0, Projectile.owner);
			//Projectile.GetProjectileSource_FromThis()
		}

		public override void AI()
		{
			Projectile.velocity.Y += Projectile.ai[0];
			int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.SpectreStaff, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust].velocity *= -0.3f;

			int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Cloud, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 2f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust2].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust2].velocity *= -0.3f;

			if (Projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref Projectile.velocity);
				Projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 250f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && Main.npc[k].type != NPCID.TargetDummy)
				{
					if (Collision.CanHit(Projectile.Center, 0, 0, Main.npc[k].Center, 0, 0))
					{
						Vector2 newMove = Main.npc[k].Center - Projectile.Center;
						float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
						if (distanceTo < distance)
						{
							move = newMove;
							distance = distanceTo;
							target = true;
						}
					}
				}
			}
			if (target)
			{
				AdjustMagnitude(ref move);
				Projectile.velocity = (10 * Projectile.velocity + move) / 6f;
				AdjustMagnitude(ref Projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 4f)
			{
				vector *= 4f / magnitude;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(BuffType<Buffs.SpiritCurse>(), 7 * 60);
		}

        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit) => target.AddBuff(BuffType<Buffs.SpiritCurse>(), 7 * 60);
    }

	public class SpiritsWrathP2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spirit's Wrath");
		}

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
			Projectile.light = 1f;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
		}

		public override void AI()
		{
			Projectile.velocity.X = 0;
			Projectile.velocity.Y = 0;
			Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Cloud, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);

			if (Main.rand.NextBool(1))     //this defines how many dust to spawn
			{
				int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.SpectreStaff, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);

				Main.dust[dust].noGravity = true; //this make so the dust has no gravity
				Main.dust[dust].velocity *= 0.5f;
				int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.SpectreStaff, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);
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
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Cloud);
				dust.noGravity = true;
				dust.scale = 2f;
				dust.velocity *= 5f;
				dust.fadeIn = 1f;
			}

			for (int i = 0; i < 50; i++)
			{
				float speedY = -2f;

				Vector2 perturbedSpeed = new Vector2(0, speedY).RotatedByRandom(MathHelper.ToRadians(360));
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.SpectreStaff, perturbedSpeed.X, perturbedSpeed.Y);

				dust.noGravity = true;
				dust.scale = 1.5f;
				dust.velocity *= 4f;
				dust.fadeIn = 1f;
			}

			for (int i = 0; i < 50; i++)
			{
				Dust dust;
				Vector2 position = Projectile.position;
				dust = Main.dust[Terraria.Dust.NewDust(position, Projectile.width, Projectile.height, DustID.SpectreStaff, 0f, 0f, 0, default, 1f)];
				dust.noGravity = true;
				dust.scale = 1f;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(ModContent.BuffType<Buffs.SpiritCurse>(), 7 * 60);
		}
	}
}

