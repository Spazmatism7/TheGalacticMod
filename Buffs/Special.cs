using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Assets.Systems;
using Microsoft.Xna.Framework;

namespace GalacticMod.Buffs
{
    public class Stunned : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stunned");
            Description.SetDefault("You cannot move!");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GalacticNPC>().stunned = true;
        }
    }

    public class Instability : ModBuff
    {
        public bool canBeCleared => false; //overide

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Instability");
            Description.SetDefault("You are unable to warp");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (Main.rand.Next(4) < 2)
            {
                if (player.gravDir == 1)
                {
                    var dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.AncientLight, 0, -3);
                    dust.scale = 1.25f;
                    dust.noGravity = true;
                    dust.velocity *= 0.75f;
                }
                else
                {
                    var dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.Sunflower, 0, +3);
                    dust.scale = 1.25f;
                    dust.noGravity = true;
                    dust.velocity *= 0.75f;
                }
            }
        }
    }
}