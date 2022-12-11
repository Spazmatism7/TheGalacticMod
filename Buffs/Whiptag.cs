using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Assets.Systems;
using Microsoft.Xna.Framework;
using System;

namespace GalacticMod.Buffs
{
    public class WhiptagSand : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tagged by Sirrocco");
            Description.SetDefault("You have been tagged by Sirrocco");
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense -= (int)(npc.defense * 0.4f);
        }
    }
}