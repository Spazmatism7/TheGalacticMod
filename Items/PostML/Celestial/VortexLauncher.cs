using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using GalacticMod.Items.Hardmode.PostPlantera;

namespace GalacticMod.Items.PostML.Celestial
{
    public class VortexLauncher : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Launches a random variety of rocket");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.shoot = ProjectileID.RocketFireworkYellow;
            Item.useAmmo = AmmoID.Rocket;
            Item.UseSound = SoundID.Item61;
            Item.damage = 50;
            Item.knockBack = 3f;
            Item.shootSpeed = 10f;
            Item.noMelee = true; //Does the weapon itself inflict damage?
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 1; i++)
            {
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(velocity.X, velocity.Y), type, damage, knockback, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item61, position);
            }
            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Here we randomly set type to either the original (as defined by the ammo), a vanilla projectile, or a mod projectile.
            
            if (type is ProjectileID.RocketII or ProjectileID.RocketIV or ProjectileID.ClusterRocketII or ProjectileID.MiniNukeRocketII or ProjectileID.Celeb2RocketExplosive or 
                ProjectileID.Celeb2RocketExplosiveLarge or ProjectileID.RocketSnowmanII or ProjectileID.RocketSnowmanIV)
                type = Main.rand.Next(new int[] { type, ProjectileID.RocketII, ProjectileID.RocketIV, ProjectileID.ClusterRocketII, ProjectileID.MiniNukeRocketII, 
                    ProjectileID.Celeb2RocketExplosive, ProjectileID.Celeb2RocketExplosiveLarge , ProjectileID.RocketSnowmanII, ProjectileID.RocketSnowmanIV, ProjectileID.ClusterSnowmanRocketII });
            else
                type = Main.rand.Next(new int[] { type, ProjectileID.RocketI, ProjectileID.RocketIII, ProjectileID.ClusterRocketI, ProjectileID.MiniNukeRocketI,
                    ProjectileID.Celeb2Rocket, ProjectileID.Celeb2RocketLarge, ProjectileID.RocketSnowmanI, ProjectileID.RocketSnowmanIII, ProjectileID.ClusterSnowmanRocketI, 
                    ProjectileID.VortexBeaterRocket, ProjectileID.RocketFireworkBlue, ProjectileID.RocketFireworkRed, ProjectileID.RocketFireworkYellow, ProjectileID.RocketFireworkGreen, type, 
                    ProjectileID.RocketI, ProjectileID.RocketIII, ProjectileID.ClusterRocketI, ProjectileID.MiniNukeRocketI, ProjectileID.Celeb2Rocket, ProjectileID.Celeb2RocketLarge, 
                    ProjectileID.RocketSnowmanI, ProjectileID.RocketSnowmanIII, ProjectileID.ClusterSnowmanRocketI, type });

            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10));
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<VortexLauncher>());
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentVortex, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}