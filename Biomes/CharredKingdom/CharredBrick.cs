using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using System;
using GalacticMod.Assets.Rarities;
using GalacticMod.Items.Hardmode.Amaza;

namespace GalacticMod.Biomes.CharredKingdom
{
    public class CharredBrick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Still hot from long gone fires");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.createTile = TileType<CharredBrickTile>();
            Item.width = 16;
            Item.height = 16;
            Item.value = 3000;
            Item.rare = RarityType<HellfireRarity>();
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
        }
    }

    public class CharredBrickTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileShine[Type] = 1000; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Charred Brick");

            DustType = 84;
            ItemDrop = ItemType<CharredBrick>();
            HitSound = SoundID.Tink;
            MineResist = 4f;
            MinPick = 225;
        }
    }

    public class UncharredBrick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Brick");
            Tooltip.SetDefault("Still hot from long gone fires");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.createTile = TileType<UncharredBrickTile>();
            Item.width = 16;
            Item.height = 16;
            Item.value = 3000;
            Item.rare = RarityType<HellfireRarity>();
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
        }
    }

    public class UncharredBrickTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileShine[Type] = 1000; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Hellfire Brick");

            DustType = 84;
            ItemDrop = ItemType<UncharredBrick>();
            HitSound = SoundID.Tink;
            MineResist = 4f;
            MinPick = 225;
        }
    }

    public class FullChar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Burnt Brick");
            Tooltip.SetDefault("Still hot from long gone fires");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.createTile = TileType<FullCharTile>();
            Item.width = 16;
            Item.height = 16;
            Item.value = 3000;
            Item.rare = RarityType<HellfireRarity>();
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
        }
    }

    public class FullCharTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine2[Type] = true; // Modifies the draw color slightly.
            Main.tileShine[Type] = 1000; // How often tiny dust appear off this tile. Larger is less frequently
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Burnt Brick");

            DustType = 84;
            ItemDrop = ItemType<FullChar>();
            HitSound = SoundID.Tink;
            MineResist = 4f;
            MinPick = 225;
        }
    }
}