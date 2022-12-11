using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace GalacticMod.Items.PreHM.Star
{
	public class FeatherBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Converts wooden arrows into feather arrows" +
                "\nFeather arrows are unaffected by gravity");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 26;
			Item.height = 58;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 8f;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 10f;
			Item.useAmmo = AmmoID.Arrow;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<FeatherBow>());
			recipe.AddIngredient(ItemID.Feather, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ProjectileType<FeatherArrow>();
            }
        }
    }

    public class FeatherArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feather Arrow");
        }

        public override void SetDefaults()
        {
            Projectile.aiStyle = 0;
            Projectile.width = 36;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.velocity.Y += Projectile.ai[0];

            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 225, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d.noGravity = true;
            }

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

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, 225);
                d.velocity *= 2;
                d.noGravity = true;
            }
        }
    }
}