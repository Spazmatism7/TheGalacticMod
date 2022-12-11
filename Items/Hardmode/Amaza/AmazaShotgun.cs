using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using GalacticMod.Tiles;
using Microsoft.Xna.Framework.Input;
using Terraria.GameContent.Creative;
using System.Reflection.Metadata;

namespace GalacticMod.Items.Hardmode.Amaza
{
	public class AmazaShotgun : ModItem
	{
		public override string Texture => GetInstance<Assets.Config.PersonalConfig>().ClassicAmaza ? base.Texture + "_Old" : base.Texture;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("BOOM!" +
				"\nFires a spread of bullets" +
                "\n33% chance not to consume ammo");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 51;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 76;
			Item.height = 22;
			Item.useAnimation = 28;
			Item.useTime = 28;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.LightPurple;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = false;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 9f;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<AmazaShotgun>());
			recipe.AddIngredient(Mod, "AmazaBar", 17);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(2f, -2f);
		}

		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= .33f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            int numberProjectiles = 4 + Main.rand.Next(3); //This defines how many projectiles to shot.

            for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(25)); // This defines the projectiles random spread; 5 degree spread.
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
            }
			return false; // return false to stop vanilla from calling Projectile.NewProjectile.
		}
	}
}
