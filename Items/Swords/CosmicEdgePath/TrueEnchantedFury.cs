using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using GalacticMod.Items.PreHM;
using GalacticMod.Projectiles;

namespace GalacticMod.Items.Swords.CosmicEdgePath
{
	public class TrueEnchantedFury : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A Jewel of Heaven and Earth");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 34;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.autoReuse = true;

			Item.DamageType = DamageClass.Melee;
			Item.damage = 66;
			Item.knockBack = 6;
			Item.crit = 2;
			Item.value = Item.buyPrice(gold: 5);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<TrueEnchantedBeam>(); // ID of the projectiles the sword will shoot
			Item.shootSpeed = 8f; // Speed of the projectiles the sword will shoot
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position, velocity, ProjectileID.DeathSickle, damage, knockback, player.whoAmI);

			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
			Vector2 heading = target - position;

			if (heading.Y < 0f)
			{
				heading.Y *= -1f;
			}

			if (heading.Y < 20f)
			{
				heading.Y = 20f;
			}

			heading.Normalize();
			heading *= velocity.Length();
			heading.Y += Main.rand.Next(-40, 41) * 0.02f;
			Projectile.NewProjectile(source, position, heading, ProjectileID.Starfury, damage, knockback, player.whoAmI, 1f, ceilingLimit);

			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<TrueEnchantedFury>());
			recipe.AddIngredient(Mod, "EnchantedFury");
			recipe.AddIngredient(ItemID.BeamSword);
			recipe.AddIngredient(ItemID.DeathSickle);
			recipe.AddIngredient(ItemID.BrokenHeroSword);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class TrueEnchantedBeam : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.height = 38;
			Projectile.width = 38;
			Projectile.aiStyle = 27;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 3;
			Projectile.light = 0.5f;
			Projectile.alpha = 230;
			Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }
	}
}