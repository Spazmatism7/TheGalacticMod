using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Tiles.Bars;

namespace GalacticMod.Items.Hardmode.Asteroid
{
    public class IridiumBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
            Tooltip.SetDefault("You can see the stars reflecting of it's surface");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.LightPurple;
            Item.useAnimation = 15;
            Item.useTime = 10;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = TileType<IridiumBarPlaced>();
            Item.consumable = true;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<IridiumBar>());
            recipe.AddIngredient(Mod, "IridiumShard", 2);
            recipe.AddIngredient(Mod, "AsteroidBar");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public class IridiumShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
            Tooltip.SetDefault("You can feel the stars");
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 24;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.LightPurple;
        }
    }
}
