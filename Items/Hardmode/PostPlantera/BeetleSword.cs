using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.PostPlantera
{
	public class BeetleSword : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Scarab Sword");
            Tooltip.SetDefault("The Power of the Sun, in the palm of my pincers");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 82;
			Item.DamageType = DamageClass.Melee; //melee weapon
			Item.width = 46;
			Item.height = 48;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 1000; //sell value
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening

			Item.shoot = ProjectileType<FlareBolt>();
			Item.shootSpeed = 3f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<BeetleSword>());
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
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.RuneWizard);
			}
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			Vector2 bladeOffset = Vector2.Normalize(velocity) * 25f;
			if (Collision.CanHit(position, 0, 0, position + bladeOffset, 0, 0))
			{
				position += bladeOffset;
			}
		}
	}

	public class FlareBolt : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = 8;
			Projectile.friendly = true;
			Projectile.alpha = 255;
			Projectile.timeLeft /= 2;
			Projectile.penetrate = 10;
			Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
        }

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(BuffID.OnFire, 120 * 60);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];

			player.AddBuff(ModContent.BuffType<Buffs.BeetleBuff>(), 120);
		}
	}
}