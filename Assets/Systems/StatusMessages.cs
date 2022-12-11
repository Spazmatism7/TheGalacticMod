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
    public class StatusMessages : ModSystem
    {
        public static bool bloodMoonMessage;

        public override void OnWorldLoad()
        {
            bloodMoonMessage = false;
        }

        public override void OnWorldUnload()
        {
            bloodMoonMessage = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            if (bloodMoonMessage)
            {
                tag["bloodMoonMessage"] = true;
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            bloodMoonMessage = tag.ContainsKey("bloodMoonMessage");
        }

        public override void NetSend(BinaryWriter writer)
        {
            var flags = new BitsByte();
            flags[0] = bloodMoonMessage;

            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            bloodMoonMessage = flags[0];
        }

        public override void PreUpdateWorld()
        {
            if (NPC.downedBoss1 && !bloodMoonMessage)
            {
                string key = "The Blood Moon cries";
                Color messageColor = Color.DarkRed;
                if (Main.netMode == NetmodeID.Server) // Server
                {
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                }
                else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
                {
                    Main.NewText(Language.GetTextValue(key), messageColor);
                }
                bloodMoonMessage = true;
            }
        }
    }
}