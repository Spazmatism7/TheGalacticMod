using GalacticMod.Tiles;
using GalacticMod.Assets.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Assets.Systems
{
    public class OreGeneration : ModSystem
    {
        public bool hasGeneratedCarbonite;
        public bool hasGennedStorm;
        public bool hasGeneratedAmaza;
        public bool hasGeneratedIOres;

        public override void OnWorldLoad()
        {
            hasGeneratedCarbonite = false;
            hasGeneratedAmaza = false;
            hasGeneratedIOres = false;
            hasGennedStorm = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["gennedCarbon"] = hasGeneratedCarbonite;
            tag["gennedAmaza"] = hasGeneratedAmaza;
            tag["gennedIOres"] = hasGeneratedIOres;
            tag["gennedStorm"] = hasGennedStorm;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            hasGeneratedCarbonite = tag.GetBool("gennedCarbon");
            hasGeneratedAmaza = tag.GetBool("gennedAmaza");
            hasGeneratedIOres = tag.GetBool("gennedIOres");
            hasGennedStorm = tag.GetBool("gennedStorm");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = hasGeneratedCarbonite;
            flags[1] = hasGeneratedAmaza;
            flags[2] = hasGeneratedIOres;
            flags[3] = hasGennedStorm;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            hasGeneratedCarbonite = flags[0];
            hasGeneratedAmaza = flags[1];
            hasGeneratedIOres = flags[2];
            hasGennedStorm = flags[3];
        }

        public override void PreUpdateWorld()
        {
            if (!GetInstance<GalacticModConfig>().PreventOreSpawn)
            {
                if (!hasGeneratedCarbonite && NPC.downedBoss3)
                {
                    for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 6E-06); i++)
                    {
                        WorldGen.OreRunner(
                            WorldGen.genRand.Next(0, Main.maxTilesX), // X Coord of the tile
                            WorldGen.genRand.Next((int)WorldGen.rockLayer, Main.maxTilesY - 200), // Y Coord of the tile
                            WorldGen.genRand.Next(18, 28), // Strength (High = more)
                            WorldGen.genRand.Next(5, 6), // Steps
                            (ushort)TileType<CarboniteT>() // The tile type that will be spawned
                           );
                    }
                    string key = "The world has been blessed with Carbonite!";
                    Color messageColor = Color.Cyan;
                    if (Main.netMode == NetmodeID.Server) // Server
                    {
                        Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
                    {
                        Main.NewText(Language.GetTextValue(key), messageColor);
                    }
                    hasGeneratedCarbonite = true;
                }

                if (!hasGennedStorm && Main.hardMode)
                {
                    for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 6E-06); i++)
                    {
                        WorldGen.OreRunner(
                            WorldGen.genRand.Next(0, Main.maxTilesX), // X Coord of the tile
                            WorldGen.genRand.Next((int)WorldGen.worldSurfaceHigh, Main.maxTilesY), // Y Coord of the tile
                            WorldGen.genRand.Next(18, 28), // Strength (High = more)
                            WorldGen.genRand.Next(5, 6), // Steps
                            (ushort)TileType<StormOreT>() // The tile type that will be spawned
                           );
                    }
                    string key = "The heavens tremble";
                    Color messageColor = Color.Cyan;
                    if (Main.netMode == NetmodeID.Server) // Server
                    {
                        Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
                    {
                        Main.NewText(Language.GetTextValue(key), messageColor);
                    }
                    hasGennedStorm = true;
                }

                if (!hasGeneratedAmaza && NPC.downedMechBossAny)
                {
                    for (int i = 0; i < (Main.maxTilesX * Main.maxTilesY); i++)
                    {
                        WorldGen.OreRunner(
                            WorldGen.genRand.Next(0, Main.maxTilesX), // X Coord of the tile
                            WorldGen.genRand.Next((int)WorldGen.rockLayerLow, (int)WorldGen.worldSurfaceHigh /*Main.maxTilesY - 200*/), // Y Coord of the tile
                            WorldGen.genRand.Next(18, 28), // Strength (High = more)
                            WorldGen.genRand.Next(5, 6), // Steps
                            (ushort)TileType<AmazaOreT>() // The tile type that will be spawned
                           );
                    }
                    string key = "The world has been blessed with Amaza!";
                    Color messageColor = Color.Cyan;
                    if (Main.netMode == NetmodeID.Server) // Server
                    {
                        Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
                    {
                        Main.NewText(Language.GetTextValue(key), messageColor);
                    }
                    hasGeneratedAmaza = true;
                }

                if (!hasGeneratedIOres && NPC.downedMoonlord)
                {
                    for (int i = 0; i < (int)(Main.maxTilesX * Main.maxTilesY * 6E-06); i++)
                    {
                        WorldGen.OreRunner(
                            WorldGen.genRand.Next(0, Main.maxTilesX), // X Coord of the tile
                            WorldGen.genRand.Next((int)WorldGen.rockLayerHigh, (int)WorldGen.worldSurfaceLow /*Main.maxTilesY - 200*/), // Y Coord of the tile
                            WorldGen.genRand.Next(18, 28), // Strength (High = more)
                            WorldGen.genRand.Next(5, 6), // Steps
                            (ushort)TileType<LutetiumOreT>() // The tile type that will be spawned
                           );
                        WorldGen.OreRunner(
                           WorldGen.genRand.Next(0, Main.maxTilesX), // X Coord of the tile
                           WorldGen.genRand.Next((int)WorldGen.rockLayerLow, (int)WorldGen.worldSurfaceLow /*Main.maxTilesY - 200*/), // Y Coord of the tile
                           WorldGen.genRand.Next(18, 28), // Strength (High = more)
                           WorldGen.genRand.Next(5, 6), // Steps
                           (ushort)TileType<VanadiumOreT>() // The tile type that will be spawned
                          );
                        WorldGen.OreRunner(
                           WorldGen.genRand.Next(0, Main.maxTilesX), // X Coord of the tile
                           WorldGen.genRand.Next((int)WorldGen.rockLayerHigh, (int)WorldGen.worldSurfaceLow /*Main.maxTilesY - 200*/), // Y Coord of the tile
                           WorldGen.genRand.Next(18, 28), // Strength (High = more)
                           WorldGen.genRand.Next(5, 6), // Steps
                           (ushort)TileType<OsmiumOreT>() // The tile type that will be spawned
                          );
                        WorldGen.OreRunner(
                           WorldGen.genRand.Next(0, Main.maxTilesX), // X Coord of the tile
                           WorldGen.genRand.Next((int)WorldGen.rockLayerLow, (int)WorldGen.worldSurfaceLow /*Main.maxTilesY - 200*/), // Y Coord of the tile
                           WorldGen.genRand.Next(18, 28), // Strength (High = more)
                           WorldGen.genRand.Next(5, 6), // Steps
                           (ushort)TileType<ZirconiumOreT>() // The tile type that will be spawned
                          );
                    }
                    string key = "The world has been blessed with the Sacred Ores!";
                    Color messageColor = Color.Cyan;
                    if (Main.netMode == NetmodeID.Server) // Server
                    {
                        Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
                    {
                        Main.NewText(Language.GetTextValue(key), messageColor);
                    }
                    hasGeneratedIOres = true;
                }
            }
        }
    }
}