using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Input;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Desert
{
	public class LightningCaller : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons powerful blasts of Lightning");
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 42;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 20;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<DesertLightning>();
			Item.shootSpeed = 16f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y);

			float ai = Main.rand.Next(100);
			int projID = Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y),
			ModContent.ProjectileType<DesertLightning>(), damage, .5f, Main.myPlayer, perturbedSpeed.ToRotation(), ai);

			return false;
		}
	}
}