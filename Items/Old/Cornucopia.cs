using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;
using System;
using GalacticMod.Items.Hardmode.Ancient;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Items.Old
{
    public class Cornucopia : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The Horn of Plenty, a relic of a long gone age");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(gold: 1);

            Item.healLife = 120; // While we change the actual healing value in GetHealLife, Item.healLife still needs to be higher than 0 for the item to be considered a healing item
            Item.potion = true; // Makes it so this item applies potion sickness on use and allows it to be used with quick heal
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<Cornucopia>());
            recipe.AddIngredient(ItemType<AncientBar>(), 10);
            recipe.AddIngredient(ItemID.GreaterHealingPotion, 30);
            recipe.AddIngredient(ItemID.Apple, 2);
            recipe.AddIngredient(ItemID.Banana, 2);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}