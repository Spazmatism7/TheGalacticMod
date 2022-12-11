using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace GalacticMod.Items.Hardmode
{
	public class CursedFlamethrower : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots a jet of cursed flames");
		}

		public override void SetDefaults()
		{
			Item.damage = 41;
			Item.crit = 2;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 50;
			Item.height = 18;
			Item.useTime = 4;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1f;
			Item.value = 10000;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item34;
			Item.shoot = ModContent.ProjectileType<CursedFlames>();
			Item.shootSpeed = 4f;
			Item.useAmmo = AmmoID.Gel;
		}

		public override void HoldItem(Player player) { }

        public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			// To make this item only consume ammo during the first jet, we check to make sure the animation just started. ConsumeAmmo is called 5 times because of item.useTime and item.useAnimation values in SetDefaults above.
			return player.itemAnimation >= player.itemAnimationMax - 4;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<CursedFlamethrower>());
			recipe.AddIngredient(ItemID.Flamethrower);
			recipe.AddIngredient(ItemID.CursedFlames, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), new Vector2(velocity.X, velocity.Y), type, damage / 2, knockback, player.whoAmI, 0, 0);
            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 54f;
			if (Collision.CanHit(position, 6, 6, position + muzzleOffset, 6, 6))
			{
				position += muzzleOffset;
			}
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, -2); // If your own flamethrower is being held wrong, edit these values. You can test out holdout offsets using Modder's Toolkit.
		}
	}

	public class CursedFlames : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;

        }
        public override void SetDefaults()
        {
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
            Projectile.extraUpdates = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.scale = 0.1f;
            DrawOffsetX = -35;
            DrawOriginOffsetY = -35;
            Projectile.light = 0.8f;
            Projectile.ArmorPenetration = 15;
        }

        public override bool? CanDamage()
        {
            if (Projectile.ai[1] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        int dustoffset;
        public override void AI()
        {
            dustoffset++;
            Projectile.rotation += 0.1f;

            if (Main.rand.NextBool(10))     //this defines how many dust to spawn
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X - (dustoffset / 2), Projectile.position.Y - (dustoffset / 2)), Projectile.width + dustoffset, Projectile.height + dustoffset, DustID.CursedTorch, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1f);
                Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                Main.dust[dust].velocity *= 0.5f;
                //int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);
            }

            if (Projectile.scale <= 1f)
                Projectile.scale += 0.012f;

            else
            {
                Projectile.alpha += 3;

                Projectile.velocity.X *= 0.98f;
                Projectile.velocity.Y *= 0.98f;

                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 10) // This will change the sprite every 8 frames (0.13 seconds). Feel free to experiment.
                {
                    Projectile.frame++;
                    Projectile.frameCounter = 0;
                }
            }

            if (Projectile.alpha > 150 || Projectile.wet)
            {
                Projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.damage = (Projectile.damage * 9) / 10;
            target.AddBuff(BuffID.CursedInferno, 240); //Gives cursed flames to target for 4 seconds. (60 = 1 second, 240 = 4 seconds)
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 240); //Gives cursed flames to target for 4 seconds. (60 = 1 second, 240 = 4 seconds)
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity *= 0;
            //Projectile.Kill();
            return false;
        }
	}

	public class Ichorthrower : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots a jet of ichor");
		}

		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 50;
			Item.height = 18;
			Item.useTime = 6;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 0f;
			Item.value = 10000;
			Item.autoReuse = true;
			Item.crit = -4;
			Item.UseSound = SoundID.Item34;
			Item.shoot = ModContent.ProjectileType<Ichor>();
			Item.shootSpeed = 3f;
			Item.useAmmo = AmmoID.Gel;
		}

		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			// To make this item only consume ammo during the first jet, we check to make sure the animation just started. ConsumeAmmo is called 5 times because of item.useTime and item.useAnimation values in SetDefaults above.
			return player.itemAnimation >= player.itemAnimationMax - 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<Ichorthrower>());
			recipe.AddIngredient(ItemID.Flamethrower);
			recipe.AddIngredient(ItemID.Ichor, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), new Vector2(velocity.X, velocity.Y), type, damage, knockback, player.whoAmI, 0, 0);
            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 54f;
			if (Collision.CanHit(position, 6, 6, position + muzzleOffset, 6, 6))
			{
				position += muzzleOffset;
			}
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(0, -2); // If your own flamethrower is being held wrong, edit these values. You can test out holdout offsets using Modder's Toolkit.
		}
	}

	public class Ichor : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;

        }
        public override void SetDefaults()
        {
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
            Projectile.extraUpdates = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.scale = 0.1f;
            DrawOffsetX = -35;
            DrawOriginOffsetY = -35;
            Projectile.light = 0.8f;
            Projectile.ArmorPenetration = 15;
        }

        public override bool? CanDamage()
        {
            if (Projectile.ai[1] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        int dustoffset;
        public override void AI()
        {
            dustoffset++;
            Projectile.rotation += 0.1f;

            if (Main.rand.NextBool(10))     //this defines how many dust to spawn
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X - (dustoffset / 2), Projectile.position.Y - (dustoffset / 2)), Projectile.width + dustoffset, Projectile.height + dustoffset, DustID.Ichor, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1f);
                Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                Main.dust[dust].velocity *= 0.5f;
                //int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);
            }

            if (Projectile.scale <= 1f)
                Projectile.scale += 0.012f;

            else
            {
                Projectile.alpha += 3;

                Projectile.velocity.X *= 0.98f;
                Projectile.velocity.Y *= 0.98f;

                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 10) // This will change the sprite every 8 frames (0.13 seconds). Feel free to experiment.
                {
                    Projectile.frame++;
                    Projectile.frameCounter = 0;
                }
            }

            if (Projectile.alpha > 300 || Projectile.wet)
            {
                Projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.damage = (Projectile.damage * 9) / 10;
            target.AddBuff(BuffID.Ichor, 240); //Gives cursed flames to target for 4 seconds. (60 = 1 second, 240 = 4 seconds)
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 240); //Gives cursed flames to target for 4 seconds. (60 = 1 second, 240 = 4 seconds)
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity *= 0;
            //Projectile.Kill();
            return false;
        }
	}
}