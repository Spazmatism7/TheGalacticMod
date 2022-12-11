using System.Collections;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Assets.Systems
{
    public class Downeds : ModSystem
    {
        //Bosses
        public static bool DownedDesertSpirit = false;
        public static bool DownedSkyGod = false;
        public static bool DownedSeaSerpent = false;
        public static bool DownedAsteroidBoss = false;
        public static bool DownedHellDragonBoss = false;
        public static bool DownedGalacticPeril = false;

        //Minibosses
        public static bool downedMythicalWyvern = false;

        public override void OnWorldLoad()
        {
            DownedDesertSpirit = false;
            DownedSkyGod = false;
            DownedSeaSerpent = false;
            DownedAsteroidBoss = false;
            DownedHellDragonBoss = false;
            DownedGalacticPeril = false;

            //Minibosses
            downedMythicalWyvern = false;
        }

        public override void OnWorldUnload()
        {
            DownedDesertSpirit = false;
            DownedSkyGod = false;
            DownedSeaSerpent = false;
            DownedAsteroidBoss = false;
            DownedHellDragonBoss = false;
            DownedGalacticPeril = false;

            //Minibosses
            downedMythicalWyvern = false;
        }

		public override void SaveWorldData(TagCompound tag)
		{
			if (DownedDesertSpirit)
			{
				tag["DownedDesertSpirit"] = true;
			}
            if (DownedSkyGod)
            {
                tag["DownedSkyGod"] = true;
            }
            if (DownedSeaSerpent)
            {
                tag["DownedSeaSerpent"] = true;
            }
            if (DownedAsteroidBoss)
            {
                tag["DownedAsteroidBoss"] = true;
            }
            if (DownedHellDragonBoss)
            {
                tag["DownedHellDragonBoss"] = true;
            }
            if (DownedGalacticPeril)
            {
                tag["DownedGalacticPeril"] = true;
            }

            //Minibosses
            if (downedMythicalWyvern)
            {
                tag["downedMythicalWyvern"] = true;
            }
        }

		public override void LoadWorldData(TagCompound tag)
		{
            DownedDesertSpirit = tag.ContainsKey("DownedDesertSpirit");
            DownedSkyGod = tag.ContainsKey("DownedSkyGod");
            DownedSeaSerpent = tag.ContainsKey("DownedSeaSerpent");
            DownedAsteroidBoss = tag.ContainsKey("DownedAsteroidBoss");
            DownedHellDragonBoss = tag.ContainsKey("DownedHellDragonBoss");
            DownedGalacticPeril = tag.ContainsKey("DownedGalacticPeril");

            //Minibosses
            downedMythicalWyvern = tag.ContainsKey("downedMythicalWyvern");
        }

		public override void NetSend(BinaryWriter writer)
		{
			var flags = new BitsByte();
			flags[0] = DownedDesertSpirit;
            flags[1] = DownedSkyGod;
            flags[2] = DownedSeaSerpent;
            flags[3] = DownedAsteroidBoss;
            flags[4] = DownedHellDragonBoss;
            flags[5] = DownedGalacticPeril;

            //Minibosses
            flags[-1] = downedMythicalWyvern;
            writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
            DownedDesertSpirit = flags[0];
            DownedSkyGod = flags[1];
            DownedSeaSerpent = flags[2];
            DownedAsteroidBoss = flags[3];
            DownedHellDragonBoss = flags[4];
            DownedGalacticPeril = flags[5];

            //Minibosses
            downedMythicalWyvern = flags[-1];
        }
	}
}