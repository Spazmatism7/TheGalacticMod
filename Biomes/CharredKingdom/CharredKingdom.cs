using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using SubworldLibrary;
using Terraria.DataStructures;
using Terraria.WorldBuilding;
using Terraria.IO;

namespace GalacticMod.Biomes.CharredKingdom
{
    public class CharredKingdom : Subworld
    {
        public override int Width => 1000;
        public override int Height => 750;

        public override bool ShouldSave => false;
        public override bool NoPlayerSaving => false;

        public override List<GenPass> Tasks => new List<GenPass>()
        {
            new CharredKingdomGenPass()
        };

        // Sets the time to the middle of the day whenever the subworld loads
        public override void OnLoad()
        {
            Main.dayTime = true;
            Main.time = 27000;
        }
    }

    public class CharredKingdomGenPass : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public CharredKingdomGenPass() : base("Terrain", 1) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Generating terrain"; // Sets the text displayed for this pass
            Main.worldSurface = Main.maxTilesY - 42; // Hides the underground layer just out of bounds
            Main.rockLayer = Main.maxTilesY; // Hides the cavern layer way out of bounds
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    progress.Set((j + i * Main.maxTilesY) / (float)(Main.maxTilesX * Main.maxTilesY)); // Controls the progress bar, should only be set between 0f and 1f
                    Tile tile = Main.tile[i, j];
                    tile.HasTile = true;
                    tile.TileType = (ushort)TileType<UncharredBrickTile>();
                }
            }
        }
    }

    public class UpdateSubworldSystem : ModSystem
    {
        public override void PreUpdateWorld()
        {
            if (SubworldSystem.IsActive<CharredKingdom>())
            {
                // Update mechanisms
                Wiring.UpdateMech();

                // Update tile entities
                TileEntity.UpdateStart();
                foreach (TileEntity te in TileEntity.ByID.Values)
                {
                    te.Update();
                }
                TileEntity.UpdateEnd();

                // Update liquid
                if (++Liquid.skipCount > 1)
                {
                    Liquid.UpdateLiquid();
                    Liquid.skipCount = 0;
                }
            }
        }
    }

    public class StandardWorldGenPass : GenPass
    {
        //TODO: remove this once tML changes generation passes
        public StandardWorldGenPass() : base("Standard World", 100) { }

        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            GenerationProgress cache = WorldGenerator.CurrentGenerationProgress; // Cache generation progress...
            WorldGen.GenerateWorld(Main.ActiveWorldFileData.Seed);
            WorldGenerator.CurrentGenerationProgress = cache; // ...because GenerateWorld sets it to null when it ends, and it must be set back
        }
    }
}
