using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using GalacticMod.Projectiles;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using GalacticMod;
using System;
using GalacticMod.Buffs;
using GalacticMod.Assets.Rarities;
using GalacticMod.Items.Hardmode.Mage;
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.Swords.CosmicEdgePath
{
	public class CosmicEdge : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Cosmic Destruction" +
                "\nRight click to unleash the fury of the sun");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 102;
			Item.height = 102;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.DamageType = DamageClass.Melee;
			Item.damage = 350;
			Item.knockBack = 6;
			Item.crit = 6;
			Item.value = Item.buyPrice(gold: 5);
			Item.rare = RarityType<GalacticRarity>();
			Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileType<CosmicSun>();
			Item.shootSpeed = 15f;
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
		}

		public override void HoldItem(Player player) => player.magmaStone = true;

		public override bool AltFunctionUse(Player player) => true;

		public override bool CanUseItem(Player player) => true;

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse == 2) //Right Click
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
				perturbedSpeed /= 2;
				Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
			}
			else //left Click
			{
				velocity *= 2;
				Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
				float ceilingLimit = target.Y;
				if (ceilingLimit > player.Center.Y - 200f)
				{
					ceilingLimit = player.Center.Y - 200f;
				}
				position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
				Vector2 heading = target - position;

				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}

				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}

				heading.Normalize();
				heading *= velocity.Length();
				heading.Y += Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(source, position, heading, ProjectileType<CosmicMeteor>(), damage, knockback, player.whoAmI, 1f, ceilingLimit);
			}

			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<CosmicEdge>());
			recipe.AddIngredient(Mod, "ElementalEdge");
			recipe.AddIngredient(Mod, "EnchantedWrath");
			recipe.AddIngredient(Mod, "GalaxyBlade");
			recipe.AddIngredient(Mod, "HellfireBlade");
            recipe.AddIngredient(Mod, "AncientExcalibur");
            recipe.AddIngredient(ItemID.MeteoriteBar, 30);
            recipe.AddIngredient(ItemID.MagmaStone);
            recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}

	public class CosmicMeteor : ModProjectile
	{
        public override void SetStaticDefaults()
        {
			Main.projFrames[Projectile.type] = 4;
		}

        public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Melee;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.height = 62;
			Projectile.width = 96;
			Projectile.tileCollide = true;
			Projectile.penetrate = 99;
			Projectile.timeLeft = 20 * 60;
			Projectile.light = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 2;
        }

		public override void AI()
		{
			if (++Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= Main.projFrames[Projectile.type])
					Projectile.frame = 0;
			}

			Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f) ? 1 : -1;

			Projectile.rotation = Projectile.velocity.ToRotation();
			if (Projectile.spriteDirection == 1)
			{
				Projectile.rotation += MathHelper.Pi;
			}

			for (int i = 0; i < 16; i++)
			{
				Dust lava = Dust.NewDustPerfect(Projectile.Center, DustID.Lava);
				lava.frame.Y = 0;
			}

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

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.velocity - Projectile.velocity, ProjectileType<FieryExplosion>(), Projectile.damage / 2, 0, Projectile.owner);
            //Projectile.GetProjectileSource_FromThis()
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
		{
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.velocity - Projectile.velocity, ProjectileType<FieryExplosion>(), Projectile.damage / 2, 0, Projectile.owner);
            //Projectile.GetProjectileSource_FromThis()
            return true;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(BuffType<HellfireDebuff>(), 30 * 60);
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffType<HellfireDebuff>(), 27 * 60);
		}
	}

	public class CosmicSun : ModProjectile
	{
        public override void SetStaticDefaults()
        {
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
		}

        public override void SetDefaults()
		{
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.friendly = true;
			Projectile.penetrate = 2;
			Projectile.timeLeft = 5 * 60;
			Projectile.light = 5f;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 1;
			DrawOffsetX = 0;
			DrawOriginOffsetY = 0;
		}

		public override void AI()
		{
			Projectile.velocity.Y += Projectile.ai[0];

			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);

            Projectile.ai[1]++;
			if (Projectile.ai[1] >= 30)
			{
				SoundEngine.PlaySound(SoundID.Item20 with { Volume = 1f, Pitch = 0.5f }, Projectile.Center);

				int numberProjectiles = 2 + Main.rand.Next(2);
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(0, -2.5f).RotatedByRandom(MathHelper.ToRadians(180));
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), 
						ProjectileType<CosmicRay>(), (int)(Projectile.damage * 0.7f), Projectile.knockBack, Projectile.owner);
				}
				Projectile.ai[1] = 0;
			}

			if (Projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref Projectile.velocity);
				Projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 400f;
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
				Projectile.velocity = (10f * Projectile.velocity + move) / 10f;
				AdjustMagnitude(ref Projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 10f)
			{
				vector *= 10f / magnitude;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.ai[0] += 0.1f;
			Projectile.velocity *= 0.75f;
			Vector2 perturbedSpeed = new Vector2(0, -7).RotatedByRandom(MathHelper.ToRadians(360));
			Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<CosmicSunReplica>(), Projectile.damage, 0, Projectile.owner);
		}

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 16; i++)
			{
				Dust dST = Dust.NewDustPerfect(Projectile.Center, 15);
				dST.frame.Y = 0;
				dST.velocity *= 2;

				Dust dS = Dust.NewDustPerfect(Projectile.Center, DustID.YellowStarDust);
				dS.frame.Y = 0;
				dS.velocity *= 2;
			}

			Vector2 perturbedSpeed = new Vector2(0, -7).RotatedByRandom(MathHelper.ToRadians(360));
			Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<CosmicBoom>(), Projectile.damage, 0, Projectile.owner);
			//Projectile.GetProjectileSource_FromThis()
		}
	}

	public class CosmicSunReplica : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 50;
			Projectile.height = 50;
			Projectile.friendly = true;
			Projectile.timeLeft = 5 * 60;
			Projectile.light = 5f;
			Projectile.penetrate = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
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
				Dust dST = Dust.NewDustPerfect(Projectile.Center, 15);
				dST.frame.Y = 0;
				dST.velocity *= 2;

				Dust dS = Dust.NewDustPerfect(Projectile.Center, DustID.YellowStarDust);
				dS.frame.Y = 0;
				dS.velocity *= 2;
			}

			Vector2 perturbedSpeed = new Vector2(0, -7).RotatedByRandom(MathHelper.ToRadians(360));
			Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<CosmicBoom>(), Projectile.damage, 0, Projectile.owner);
			//Projectile.GetProjectileSource_FromThis()
		}
	}

	public class CosmicBoom : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 24;
			Projectile.height = 24;
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
			Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.YellowStarDust, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);

			if (Main.rand.NextBool(1))     //this defines how many dust to spawn
			{
				int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.YellowStarDust, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1.5f);

				Main.dust[dust].noGravity = true; //this make so the dust has no gravity
				Main.dust[dust].velocity *= 0.5f;
				int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.MagicMirror, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);
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
			SoundEngine.PlaySound(SoundID.Item1, Projectile.position);

			for (int i = 0; i < 50; i++)
			{
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.YellowStarDust);
				dust.noGravity = true;
				dust.scale = 2f;
				dust.velocity *= 5f;
				dust.fadeIn = 1f;
			}

			for (int i = 0; i < 50; i++)
			{
				float speedY = -2f;

				Vector2 perturbedSpeed = new Vector2(0, speedY).RotatedByRandom(MathHelper.ToRadians(360));
				var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.MagicMirror, perturbedSpeed.X, perturbedSpeed.Y);

				dust.noGravity = true;
				dust.scale = 1.5f;
				dust.velocity *= 4f;
				dust.fadeIn = 1f;
			}

			for (int i = 0; i < 50; i++)
			{
				Dust dust;
				Vector2 position = Projectile.position;
				dust = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, DustID.YellowStarDust, 0f, 0f, 0, default, 1f)];
				dust.noGravity = true;
				dust.scale = 1f;
			}
		}
	}

	public class CosmicRay : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 101;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 3;
			Projectile.timeLeft = 7 * 60;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }

        public override void AI()
        {
			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 10;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
        }

        public override Color? GetAlpha(Color lightColor)
		{
			return Color.LightGoldenrodYellow;
		}
	}
}