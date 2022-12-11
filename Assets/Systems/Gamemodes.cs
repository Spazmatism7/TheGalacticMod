using GalacticMod.Assets.Config;
using GalacticMod.Items.GamemodeItems;
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
    public class Gamemodes : ModSystem
    {
        public bool noHitMode = false;
        public bool noHitMessage;

        public bool BloodMode = false;
        public bool bloodMessage;

        public bool Deathbringer = false;

        public override void OnWorldLoad()
        {
            noHitMode = false;
            BloodMode = false;
            Deathbringer = false;
        }

        public override void OnWorldUnload()
        {
            noHitMode = false;
            BloodMode = false;
            Deathbringer = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (noHitMode)
            {
                tag["noHitMode"] = true;
            }
            if (BloodMode)
            {
                tag["BloodMode"] = true;
            }
            if (Deathbringer)
            {
                tag["Deathbringer"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            noHitMode = tag.ContainsKey("noHitMode");
            BloodMode = tag.ContainsKey("BloodMode");
            Deathbringer = tag.ContainsKey("Deathbringer");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = noHitMode;
            flags[1] = BloodMode;
            flags[2] = Deathbringer;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            noHitMode = flags[0];
            BloodMode = flags[1];
            Deathbringer = flags[2];
        }

        public override void PreUpdateWorld()
        {
            if (noHitMode && !noHitMessage)
            {
                string key = "No Hit Mode is active";
                Color messageColor = Color.Purple;
                if (Main.netMode == NetmodeID.Server) // Server
                {
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                }
                else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
                {
                    Main.NewText(Language.GetTextValue(key), messageColor);
                }
                noHitMessage = true;
            }

            if (BloodMode && !bloodMessage)
            {
                string key = "The Blood Gods thirst";
                Color messageColor = Color.DarkRed;
                if (Main.netMode == NetmodeID.Server) // Server
                {
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                }
                else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
                {
                    Main.NewText(Language.GetTextValue(key), messageColor);
                }
                bloodMessage = true;
            }
        }
    }
}