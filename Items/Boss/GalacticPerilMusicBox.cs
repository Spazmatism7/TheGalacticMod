using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ObjectData;
using GalacticMod.Assets.Rarities;

namespace GalacticMod.Items.Boss
{
    public class GalacticPerilMusicBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Music Box (The Universe is In Galactic Peril)");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            // The following code links the music box's item and tile with a music track:
            //   When music with the given ID is playing, equipped music boxes have a chance to change their id to the given item type.
            //   When an item with the given item type is equipped, it will play the music that has musicSlot as its ID.
            //   When a tile with the given type and Y-frame is nearby, if its X-frame is >= 36, it will play the music that has musicSlot as its ID.
            // When getting the music slot, you should not add the file extensions!
            MusicLoader.AddMusicBox(Mod, MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/TheUniverseIsInGalacticPeril"), ModContent.ItemType<GalacticPerilMusicBox>(), ModContent.TileType<GalacticPerilMusicBoxTile>());
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<GalacticPerilMusicBoxTile>();
            Item.width = 24;
            Item.height = 24;
            Item.rare = ModContent.RarityType<GalacticRarity>();
            Item.value = 100000;
            Item.accessory = true;
        }
    }

    public class GalacticPerilMusicBoxTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Origin = new Point16(0, 1);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.addTile(Type);

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Music Box");
            AddMapEntry(new Color(200, 200, 200), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 48, ModContent.ItemType<GalacticPerilMusicBox>());
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<GalacticPerilMusicBox>();
        }
    }
}