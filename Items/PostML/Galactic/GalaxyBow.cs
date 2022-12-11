using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using GalacticMod.Assets.Rarities;

namespace GalacticMod.Items.PostML.Galactic
{
	internal class GalaxyBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Fires an extra 'special' arrow" +
                "\nRight click to fire a spread of special arrows");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 280;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 30;
			Item.height = 50;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = ModContent.RarityType<GalacticRarity>();
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
			Item.shootSpeed = 14f;
			Item.useAmmo = AmmoID.Arrow;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 12f;
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
				type = ModContent.ProjectileType<GalaxyBowArrow>();

				int numberProjectiles = 1 + Main.rand.Next(2); ; //This defines how many projectiles to shot.
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10)); // This defines the projectiles random spread . 10 degree spread.
					Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
				}
				SoundEngine.PlaySound(SoundID.Item, player.Center);
			}

			else
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10)); // This defines the projectiles random spread . 10 degree spread.
				Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
				Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<GalaxyBowArrow>(), damage, knockback, player.whoAmI);
				SoundEngine.PlaySound(SoundID.Item, player.Center);
			}

			return false;
		}
	}
}