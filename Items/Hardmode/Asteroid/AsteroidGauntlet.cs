using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.Enums;
using System;
using System.Collections.Generic;

namespace GalacticMod.Items.Hardmode.Asteroid
{
	public class AsteroidGauntlet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Punch 'em into space");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 110;
			Item.width = 26;
			Item.height = 26;
			Item.useAnimation = 16;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightRed;

			Item.useTime = 16;
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.useStyle = ItemUseStyleID.Rapier;
			Item.autoReuse = true;
			Item.noUseGraphic = true; // The sword is actually a "projectile", so the item should not be visible when used
			Item.noMelee = true; // The projectile will do the damage and not the item
			Item.shoot = ModContent.ProjectileType<AsteroidGauntletProj>(); // The projectile is what makes a shortsword work
			Item.shootSpeed = 1f; // This value bleeds into the behavior of the projectile as velocity, keep that in mind when tweaking values

		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<AsteroidGauntlet>());
			recipe.AddIngredient(Mod, "AsteroidBar", 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

    public class AsteroidGauntletProj : ModProjectile
	{
		public const int FadeInDuration = 7;
		public const int FadeOutDuration = 4;

		public const int TotalDuration = 16 * 2;

		// The "width" of the blade
		public float CollisionWidth => 10f * Projectile.scale;

		public int Timer
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Asteroid Gauntlet");
		}

		public override void SetDefaults()
		{
			Projectile.height = 30;
			Projectile.width = 20;
			Projectile.aiStyle = -1; // Use our own AI to customize how it behaves, if you don't want that, keep this at ProjAIStyleID.ShortSword. You would still need to use the code in SetVisualOffsets() though
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = true;
			Projectile.scale = 1f;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.ownerHitCheck = true; // Prevents hits through tiles. Most melee weapons that use projectiles have this
			Projectile.extraUpdates = 1; // Update 1+extraUpdates times per tick
			Projectile.timeLeft = 64; // This value does not matter since we manually kill it earlier, it just has to be higher than the duration we use in AI
			Projectile.hide = true; // Important when used alongside player.heldProj. "Hidden" projectiles have special draw conditions
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			Timer += 1;
			if (Timer >= TotalDuration)
			{
				// Kill the projectile if it reaches it's intented lifetime
				//Projectile.Kill();
				Projectile.velocity -= Projectile.velocity;
				return;
			}
			else
			{
				// Important so that the sprite draws "in" the player's hand and not fully infront or behind the player
				player.heldProj = Projectile.whoAmI;
			}

			// Fade in and out
			// GetLerpValue returns a value between 0f and 1f - if clamped is true - representing how far Timer got along the "distance" defined by the first two parameters
			// The first call handles the fade in, the second one the fade out.
			// Notice the second call's parameters are swapped, this means the result will be reverted
			Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);

			// Keep locked onto the player, but extend further based on the given velocity (Requires ShouldUpdatePosition returning false to work)
			Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
			Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;

            // The code in this method is important to align the sprite with the hitbox how we want it to
            SetVisualOffsets();
		}

		private void SetVisualOffsets()
		{
			// 32 is the sprite size (here both width and height equal)
			const int HalfSpriteWidth = 30 / 2;
			const int HalfSpriteHeight = 20 / 2;

			int HalfProjWidth = Projectile.width / 2;
			int HalfProjHeight = Projectile.height / 2;

			// Vanilla configuration for "hitbox in middle of sprite"
			DrawOriginOffsetX = 0;
			DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
			DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);

			// Vanilla configuration for "hitbox towards the end"
			//if (Projectile.spriteDirection == 1) {
			//	DrawOriginOffsetX = -(HalfProjWidth - HalfSpriteWidth);
			//	DrawOffsetX = (int)-DrawOriginOffsetX * 2;
			//	DrawOriginOffsetY = 0;
			//}
			//else {
			//	DrawOriginOffsetX = (HalfProjWidth - HalfSpriteWidth);
			//	DrawOffsetX = 0;
			//	DrawOriginOffsetY = 0;
			//}
		}

		public override bool ShouldUpdatePosition()
		{
			// Update Projectile.Center manually
			return false;
		}

		public override void CutTiles()
		{
			// "cutting tiles" refers to breaking pots, grass, queen bee larva, etc.
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 start = Projectile.Center;
			Vector2 end = start + Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10f;
			Utils.PlotTileLine(start, end, CollisionWidth, DelegateMethods.CutTiles);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			// "Hit anything between the player and the tip of the sword"
			// shootSpeed is 2.1f for reference, so this is basically plotting 12 pixels ahead from the center
			Vector2 start = Projectile.Center;
			Vector2 end = start + Projectile.velocity * 6f;
			float collisionPoint = 0f; // Don't need that variable, but required as parameter
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, CollisionWidth, ref collisionPoint);
		}
	}

	public class AsteroidGauntletShockwave : ModProjectile
	{
		int deathTimer;
		bool death = false;

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 44;
			Projectile.aiStyle = 28;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.tileCollide = true;
			Projectile.alpha = 150;
			Projectile.light = 1f;
			Projectile.timeLeft = 12;
		}

		public override void AI()
		{
			Projectile.velocity.Y += Projectile.ai[0];

			Lighting.AddLight(Projectile.Center, Color.Orange.ToVector3() * 0.78f);

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

			deathTimer--;

			if (deathTimer <= 0 && death)
				Projectile.Kill();
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			deathTimer = 12;
			death = true;
        }

		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 16; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch);
				d.frame.Y = 0;
				d.velocity *= 2;
			}

			for (int i = 0; i < 16; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Lava);
				d.frame.Y = 0;
				d.velocity *= 2;
			}
		}
	}
}