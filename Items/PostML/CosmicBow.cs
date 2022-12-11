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
    public class CosmicBow : ModItem
    {
        int angle = 5;
        string typeToShoot = "inputted arrows";

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Cosmic Slinger");
            Tooltip.SetDefault("Eternity" +
                "\nFires an insane spread of arrows" +
                "\nRight click to change the angle of the spread of arrows");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tooltip = new TooltipLine(Mod, "GalacticMod: Cosmic Slinger", $"The bow will fire at an angle of {angle} degrees") { OverrideColor = Color.BlueViolet };
            tooltips.Add(tooltip);
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                switch (angle)
                {
                    case 1:
                        angle = 5;
                        break;
                    case 5:
                        angle = 10;
                        break;
                    case 10:
                        angle = 15;
                        break;
                    case 15:
                        angle = 20;
                        break;
                    case 20:
                        angle = 25;
                        break;
                    case 25:
                        angle = 1;
                        break;
                }
                SoundEngine.PlaySound(SoundID.Research, player.Center);
            }
            else
            {
                int numberProjectiles = 30 + Main.rand.Next(2); ; //This defines how many projectiles to shot.

                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(angle)); // This defines the projectiles random spread; 5 degree spread.
                    Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
                }
                SoundEngine.PlaySound(SoundID.Item36 with { Volume = 1f, Pitch = 0.5f }, player.Center);
            }

            return false;
        }
    }
}