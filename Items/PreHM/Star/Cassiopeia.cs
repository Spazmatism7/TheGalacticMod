using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace GalacticMod.Items.PreHM.Star
{
    public class Cassiopeia : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Converts Wood Arrows into Jester Arrows");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 44;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 26;
            Item.height = 58;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 8f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 20f;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<Cassiopeia>());
            recipe.AddIngredient(Mod, "Sagitta");
            recipe.AddIngredient(Mod, "SkyEssence", 14);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ProjectileID.JestersArrow;
            }
        }
    }
}