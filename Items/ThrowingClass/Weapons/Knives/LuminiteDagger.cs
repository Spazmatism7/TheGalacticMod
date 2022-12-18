using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Items.Swords.CosmicEdgePath;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace GalacticMod.Items.ThrowingClass.Weapons.Knives
{
	public class LuminiteDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 300;
            Item.width = 10;
            Item.height = 24;
            Item.DamageType = DamageClass.Throwing;
			Item.useTime = 30;
            Item.useAnimation = 20;
            Item.crit = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noUseGraphic = true;
			Item.noMelee = true;
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ProjectileType<LuminiteDaggerP>();
            Item.shootSpeed = 16f;

            Item.UseSound = SoundID.Item13;
            Item.value = Item.sellPrice(silver: 5);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
            Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI); ;

            return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<LuminiteDagger>());
			recipe.AddIngredient(ItemID.ThrowingKnife, 100);
			recipe.AddIngredient(ItemID.LunarBar, 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

    public class LuminiteDaggerP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Luminite Dagger");
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.DamageType = DamageClass.Throwing;
        }

        public override void AI()
        {
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

            Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();
        }
    }
}