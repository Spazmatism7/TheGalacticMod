using GalacticMod.Assets.Rarities;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace GalacticMod.Biomes.CharredKingdom
{
    public class CharredWall : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Still hot from long gone fires");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 400;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.rare = ModContent.RarityType<HellfireRarity>();
            Item.createWall = ModContent.WallType<CharredWallPlaced>();
        }
    }

    public class CharredWallPlaced : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;

            ItemDrop = ModContent.ItemType<CharredWall>();

            AddMapEntry(new Color(150, 150, 150));
        }
    }

    public class CharWall : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Still hot from long gone fires");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 400;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.rare = ModContent.RarityType<HellfireRarity>();
            Item.createWall = ModContent.WallType<CharWallPlaced>();
        }
    }

    public class CharWallPlaced : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;

            ItemDrop = ModContent.ItemType<CharWall>();

            AddMapEntry(new Color(150, 150, 150));
        }
    }

    public class UncharredWall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Wall");
            Tooltip.SetDefault("Still hot from long gone fires");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 400;
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = 999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.rare = ModContent.RarityType<HellfireRarity>();
            Item.createWall = ModContent.WallType<UncharredWallPlaced>();
        }
    }

    public class UncharredWallPlaced : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;

            ItemDrop = ModContent.ItemType<UncharredWall>();

            AddMapEntry(new Color(150, 150, 150));
        }
    }
}