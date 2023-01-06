using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;

namespace GalacticMod.Items.Hardmode.Steel
{
    public class SteelShotgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 46;
            Item.height = 16;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 4 + Main.rand.Next(2); ; //This defines how many projectiles to shot.
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10)); // This defines the projectiles random spread . 10 degree spread.
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
            }
            return false; // return false to stop vanilla from calling Projectile.NewProjectile.
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<SteelShotgun>());
            recipe.AddIngredient(Mod, "SteelBar", 12);
            recipe.AddIngredient(ItemID.Boomstick);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}