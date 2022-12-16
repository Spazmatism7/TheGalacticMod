using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using System.Threading.Tasks;
using Terraria.GameContent.Creative;
using GalacticMod.Items.PreHM.Nautilus;

namespace GalacticMod.Items.Hardmode.PostPlantera
{
	public class BeetleSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Weevil Poker");
            Tooltip.SetDefault("Fires a beams randomly");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 84;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 18;
			Item.useTime = 24;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.width = 32;
			Item.height = 32;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(silver: 10);

			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileType<BeetleSpearP>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<BeetleSpear>());
			recipe.AddIngredient(ItemID.BeetleHusk, 13);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
				//Emit dusts when the sword is swung
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.MartianHit);
			}
		}

		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
	}

	public class BeetleSpearP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beetle Spear");
		}

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 19;
            Projectile.penetrate = -1;
            Projectile.scale = 1f;
            Projectile.alpha = 0;

            Projectile.hide = true;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.friendly = true;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }

        protected virtual float HoldoutRangeMin => 25f;

        protected virtual float HoldoutRangeMax => 100f;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner]; // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            int duration = player.itemAnimationMax; // Define the duration the projectile will exist in frames

            player.heldProj = Projectile.whoAmI; // Update the player's held projectile id

            // Reset projectile time left if necessary
            if (Projectile.timeLeft > duration)
            {
                Projectile.timeLeft = duration;
            }

            Projectile.velocity = Vector2.Normalize(Projectile.velocity); // Velocity isn't used in this spear implementation, but we use the field to store the spear's attack direction.

            float halfDuration = duration * 0.5f;
            float progress;

            // Here 'progress' is set to a value that goes from 0.0 to 1.0 and back during the item use animation.
            if (Projectile.timeLeft < halfDuration)
            {
                progress = Projectile.timeLeft / halfDuration;
            }
            else
            {
                progress = (duration - Projectile.timeLeft) / halfDuration;
            }

            // Move the projectile from the HoldoutRangeMin to the HoldoutRangeMax and back, using SmoothStep for easing the movement
            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);

            // Apply proper rotation to the sprite.
            if (Projectile.spriteDirection == -1)
            {
                // If sprite is facing left, rotate 45 degrees
                Projectile.rotation += MathHelper.ToRadians(45f);
            }
            else
            {
                // If sprite is facing right, rotate 135 degrees
                Projectile.rotation += MathHelper.ToRadians(135f);
            }

			SummonBeetleBolt();
        }

        private int damageBeam = 60;

		private void SummonBeetleBolt()
        {
			if (Projectile.localAI[0] > 0f)
			{
				return;
			}
			Projectile.localAI[0] = 1000f;
			int num = Main.rand.Next(2) * 2 - 1;
			Vector2 vector = new Vector2(num * (4f + Main.rand.Next(3)), 0f);
            Vector2 vector2 = Projectile.Center + new Vector2(-num * 120, 0f);
			vector += (Projectile.Center + Vector2.Zero * 15f - vector2).SafeNormalize(Vector2.Zero) * 2f;
            Vector2 velocity = Main.rand.Next(new Vector2[] { Projectile.velocity, Projectile.velocity * 2, Projectile.velocity / 2, vector, vector, vector / 2, vector * 2 });
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector2, velocity, ProjectileType<BeetleBeam>(), damageBeam, 0f, Projectile.owner);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];

			player.AddBuff(ModContent.BuffType<Buffs.BeetleBuff>(), 120);
		}
	}

	public class BeetleBeam : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true;
			Projectile.light = 3f;
			Projectile.scale = 1.1f;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 180;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.spriteDirection = Projectile.direction;
		}

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.Yellow.ToVector3() * 0.78f);

            Projectile.direction = Projectile.spriteDirection = (Projectile.velocity.X > 0f) ? 1 : -1;

            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.Pi;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];

			player.AddBuff(BuffType<Buffs.BeetleBuff>(), 120);
		}
	}
}