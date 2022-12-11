using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.DataStructures;

namespace GalacticMod.Items.PreHM.Truffle
{
    public class Shroomstick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a spread of bullets");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 28;
            Item.height = 70;
            Item.useTime = 36; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 36; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 6;
            Item.value = 10000; // how much the item sells for (measured in copper)
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11;
            Item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 5.5f; // the speed of the projectile (measured in pixels per frame)
            Item.useAmmo = AmmoID.Bullet; // The "ammo Id" of the ammo item that this weapon uses. Note that this is not an item Id, but just a magic value.
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<Shroomstick>());
            recipe.AddIngredient(ItemID.Boomstick);
            recipe.AddIngredient(ItemID.GlowingMushroom, 30);
            recipe.AddTile(TileID.WorkBenches);
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

        public override Vector2? HoldoutOffset() => new Vector2(2f, -2f);

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int numberProjectiles = 3 + Main.rand.Next(2); //This defines how many projectiles to shot.

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(20)); // This defines the projectiles random spread; 5 degree spread.
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
            }
            return false; // return false to stop vanilla from calling Projectile.NewProjectile.
        }
    }
}