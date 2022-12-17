using GalacticMod.NPCs.Bosses.PreHM;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.NPCs
{
    public class Stuff : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.type == NPCID.Harpy)
            {
                if (NPC.AnyNPCs(NPCType<SkyGod>()))
                    GetInstance<HarpyCounter>().harpyCounter++;

                Color messageColor = Color.Cyan;
                string key = "Harpies: " + GetInstance<HarpyCounter>().harpyCounter;
                Color halfColor = Color.Yellow;
                string halfKey = "An ancient force stirs...";
                string halfKey_ = "The sky trembles";

                //Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);

                if (GetInstance<HarpyCounter>().harpyCounter == 50)
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(halfKey), halfColor);
                if (GetInstance<HarpyCounter>().harpyCounter == 75)
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(halfKey_), halfColor);

                if (GetInstance<HarpyCounter>().harpyCounter >= 100)
                {
                    SoundEngine.PlaySound(SoundID.Roar);

                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.SpawnBoss((int)npc.position.X, (int)npc.position.Y, NPCType<SkyGod>(), Main.myPlayer);
                    }
                    else
                    {
                        NetMessage.SendData(MessageID.SpawnBoss, number: Main.myPlayer, number2: NPCType<SkyGod>());
                    }
                }
            }
        }
    }

    public class HarpyCounter : ModSystem
    {
        public int harpyCounter;

        public override void PreUpdateWorld()
        {
            if (harpyCounter >= 100)
            {
                harpyCounter = 0;
            }
        }
    }
}