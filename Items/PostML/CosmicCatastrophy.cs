using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Projectiles;
using Terraria.DataStructures;
using Terraria.Audio;
using System.Collections.Generic;
using GalacticMod.Assets.Rarities;
using GalacticMod.Items.Hardmode.Elemental;

namespace GalacticMod.Items.PostML
{
    public class CosmicCatastrophy : ModItem
    {
        string typeToShoot = "inputted arrows";

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Eternity" +
                "\nRight click to change the arrow fired");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip2 = new TooltipLine(Mod, "GalacticMod: CosmicCatastrophy", $"The bow will fire {typeToShoot}") { OverrideColor = Color.PaleVioletRed };
            tooltips.Add(tooltip2);
        }

        public override void SetDefaults()
        {
            Item.damage = 90;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 38;
            Item.height = 86;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = RarityType<GalacticRarity>();
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.VilePowder;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player) => true;

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<CosmicCatastrophy>());
            recipe.AddIngredient(Mod, "TerraBow");
            recipe.AddIngredient(Mod, "ElementalCatastrophy");
            recipe.AddIngredient(Mod, "GalaxyBow");
            recipe.AddIngredient(Mod, "HellBow");
            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                switch (typeToShoot)
                {
                    case "inputted arrows": //Cosmic Slinger
                        typeToShoot = "arrows from the sky";
                        break;
                    case "arrows from the sky": //Cosmic Stormbow
                        typeToShoot = "shadowfire arrows from the sky";
                        break;
                    case "shadowfire arrows from the sky": //Cosmic Shadebow
                        typeToShoot = "elemental arrows";
                        break;
                    case "elemental arrows": //Cosmic Catastrophy
                        typeToShoot = "inputted arrows";
                        break;

                }
                SoundEngine.PlaySound(SoundID.Research, player.Center);
            }
            else
            {
                int numberProjectiles = 30 + Main.rand.Next(2); ; //This defines how many projectiles to shot.

                if (typeToShoot == "shadowfire arrows from the sky")
                {
                    Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                    float ceilingLimit = target.Y;
                    if (ceilingLimit > player.Center.Y - 200f)
                    {
                        ceilingLimit = player.Center.Y - 200f;
                    }
                    for (int i = 0; i < 7; i++)
                    {
                        position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                        position.Y -= 100 * i;
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
                        Projectile.NewProjectile(source, position, heading, ProjectileType<ShadowfireArrow>(), damage, knockback, player.whoAmI, 0f, ceilingLimit);
                    }
                }

                else if (typeToShoot == "elemental arrows")
                {
                    type = ProjectileType<ElementalArrow>();
                    numberProjectiles = 10 + Main.rand.Next(2);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(15)); // This defines the projectiles random spread; 5 degree spread.
                        Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
                    }
                    SoundEngine.PlaySound(SoundID.Item36 with { Volume = 1f, Pitch = 0.5f }, player.Center);
                }

                else if (typeToShoot == "arrows from the sky")
                {
                    Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                    float ceilingLimit = target.Y;
                    if (ceilingLimit > player.Center.Y - 200f)
                    {
                        ceilingLimit = player.Center.Y - 200f;
                    }
                    for (int i = 0; i < 12; i++)
                    {
                        position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                        position.Y -= 100 * i;
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
                        Projectile.NewProjectile(source, position, heading, type, damage, knockback, player.whoAmI, 0f, ceilingLimit);
                    }
                }
                else
                {
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(15)); // This defines the projectiles random spread; 5 degree spread.
                        Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
                    }
                    SoundEngine.PlaySound(SoundID.Item36 with { Volume = 1f, Pitch = 0.5f }, player.Center);
                }
            }

            return false;
        }
    }
}