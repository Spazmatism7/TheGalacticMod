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
	public class FeatherSword : ModItem
	{
		bool shootNow = false;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires a feather every other swing");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 28;
			Item.DamageType = DamageClass.Melee; //melee weapon
			Item.width = 68;
			Item.height = 68;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4.25f;
			Item.value = 1000; //sell value
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening

			Item.shoot = ProjectileType<FeatherSwordProj>();
			Item.shootSpeed = 10f;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (shootNow)
            {
                shootNow = false;
				return true;
            }
            else
                shootNow = true;

            return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<FeatherSword>());
			recipe.AddIngredient(ItemID.Feather, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}

	public class FeatherSwordProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Feather");
		}

		public override void SetDefaults()
		{
			Projectile.width = 28;
			Projectile.height = 14;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }

		public override void AI()
		{
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
		}
	}
}