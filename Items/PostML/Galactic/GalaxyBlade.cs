using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Rarities;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Audio;

namespace GalacticMod.Items.PostML.Galactic
{
	internal class GalaxyBlade : ModItem
	{
		public bool rightclick;
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Sharper than the stars");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 273;
			Item.DamageType = DamageClass.Melee;
			Item.width = 52;
			Item.height = 52;
			Item.useTime = 25;
			Item.useAnimation = 25;
			if (rightclick)
            {
				Item.DefaultToSpear(877, 3.5f, 24);
				Item.SetWeaponValues(56, 12f);
				Item.SetShopValues(ItemRarityColor.LightRed4, Item.buyPrice(0, 6));
				Item.channel = true;
			}
            else
            {
				Item.useStyle = ItemUseStyleID.Swing;
			}
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = ModContent.RarityType<GalacticRarity>();
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
			Item.shoot = ProjectileType<GalaxyFlame>();
			Item.shootSpeed = 15f;
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
				int stabsword = ProjectileType<GalaxySpear>();
				rightclick = true;

				Projectile.NewProjectile(source, position, velocity, stabsword, damage, knockback, player.whoAmI);
			}
			else //left Click
			{
				Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			}
			return false;
		}
	}

	internal class GalaxyFlame : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 600;
			Projectile.light = 3f;
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
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(0), ProjectileType<GFBoom>(), Projectile.damage, 0, Projectile.owner);
        }
    }

	internal class GalaxySpear : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 25;
			Projectile.height = 25;
			Projectile.aiStyle = 19;
			Projectile.alpha = 255;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.scale = 2f;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }
	}
}