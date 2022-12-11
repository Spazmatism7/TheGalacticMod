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
	/*public class BeetleYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Weevil");
			ItemID.Sets.Yoyo[Item.type] = true;
			ItemID.Sets.GamepadExtraRange[Item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
			Tooltip.SetDefault("Fires beetles");
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
			Item.damage = 60;
			Item.rare = ItemRarityID.Yellow;

			Item.DamageType = DamageClass.Melee;
			Item.channel = true;
			Item.noMelee = true;
			Item.noUseGraphic = true;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(silver: 1);
			Item.shoot = ProjectileType<BeetleYoyoP>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<BeetleYoyo>());
			recipe.AddIngredient(ItemID.BeetleHusk, 13);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.MartianHit);
			}
		}
	}*/

	public class BeetleYoyoP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 7.5f;
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 400f;
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 13f;
			DisplayName.SetDefault("The Weevil");
		}

		public override void SetDefaults()
		{
			Projectile.extraUpdates = 0;
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = 99;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.scale = 1f;
		}

		int shoottime = 0;
		public override void AI()
		{
			Dust dust;
			// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
			Vector2 position = Projectile.position;
			dust = Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.RedsWingsRun, 0f, 0f, 0, new Color(255, 255, 255), 1f);
			dust.noGravity = true;

			shoottime++;
			if (shoottime >= 30)
			{
				SoundEngine.PlaySound(SoundID.Zombie1, Projectile.Center);
				Vector2 perturbedSpeed = new Vector2(0, -4).RotatedByRandom(MathHelper.ToRadians(360));
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<BeetleProj>(), (int)(Projectile.damage * 0.5f), 0, Projectile.owner);
                shoottime = 0;
			}
		}
	}

    public class BeetleProj : ModProjectile //For beetle Weapons
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beetle");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;

            Projectile.friendly = true;
            Projectile.penetrate = 3;

            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.scale = 1f;
            Projectile.extraUpdates = 1;


            DrawOffsetX = 0;
            DrawOriginOffsetY = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;

        }

        int damagetime = 0;
        public override bool? CanDamage()
        {
            if (damagetime <= 30)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override void AI()
        {
            damagetime++;
            if (Projectile.velocity.X < 0)
            {
                Projectile.spriteDirection = -1;
            }
            else
            {
                Projectile.spriteDirection = 1;

            }
            Projectile.rotation = Projectile.velocity.X / 20;

            if (damagetime > 30)
            {
                if (Projectile.localAI[0] == 0f)
                {
                    AdjustMagnitude(ref Projectile.velocity);
                    Projectile.localAI[0] = 1f;
                }
                Vector2 move = Vector2.Zero;
                float distance = 700f;
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
                    Projectile.velocity = (10 * Projectile.velocity + move) / 11f;
                    AdjustMagnitude(ref Projectile.velocity);
                }
            }

            AnimateProjectile();
        }

        private void AdjustMagnitude(ref Vector2 vector)
        {
            if (damagetime > 30)
            {
                float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                if (magnitude > 6f)
                {
                    vector *= 6f / magnitude;
                }
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X * 0.8f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y * 0.8f;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.damage = (Projectile.damage * 9) / 10;
        }

        public override void Kill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                for (int i = 0; i < 7; i++)
                {
                    var dust = Dust.NewDustDirect(Projectile.Center, Projectile.width = 10, Projectile.height = 10, DustID.RedsWingsRun);

                    dust.noGravity = true;
                }
            }
        }

        public void AnimateProjectile() // Call this every frame, for example in the AI method.
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5) // This will change the sprite every 8 frames (0.13 seconds). Feel free to experiment.
            {
                Projectile.frame++;
                Projectile.frame %= 3; // Will reset to the first frame if you've gone through them all.
                Projectile.frameCounter = 0;
            }
        }
    }
}