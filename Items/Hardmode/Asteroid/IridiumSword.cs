using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using GalacticMod.Buffs;

namespace GalacticMod.Items.Hardmode.Asteroid
{
    public class IridiumSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inflicts Iridium Poison upon enemies");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 72;
            Item.DamageType = DamageClass.Melee; //melee weapon
            Item.width = 44;
            Item.height = 44;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 1000; //sell value
            Item.rare = ItemRarityID.LightPurple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true; //autoswing
            Item.useTurn = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<IridiumSword>());
            recipe.AddIngredient(Mod, "IridiumBar", 14);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffType<IridiumPoison>(), 240);
        }
    }
}