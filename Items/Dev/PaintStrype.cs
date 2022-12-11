using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using GalacticMod.Projectiles.Dev;
using Terraria.Audio;
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.Dev
{
	public class PaintStrype : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Tooltip.SetDefault("Fires colorful arrows");
		}

		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Ranged;
			Item.width = 28;
			Item.height = 70;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = -12;
			Item.expert = true;
			Item.crit = 6;
			Item.UseSound = SoundID.Item;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.useAmmo = AmmoID.Arrow;
            Item.damage = 145;
            Item.useTime = 15;
            Item.shootSpeed = 20f;
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10)); // This defines the projectiles random spread . 10 degree spread.
			Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<ColorArrow>(), damage, knockback, player.whoAmI);
			SoundEngine.PlaySound(SoundID.Item, player.Center);

			return false;
		}
	}
}