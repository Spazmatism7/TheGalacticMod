using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.ItemDropRules;

namespace GalacticMod.NPCs
{
    public class Toxotai : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 20;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 20;
            AnimationType = NPCID.SkeletonArcher;
            NPC.width = 18;
            NPC.height = 40;

            NPC.aiStyle = 3;
            AIType = 110;

            NPC.damage = 36;
            NPC.defense = 10;
            NPC.lifeMax = 70;
            NPC.knockBackResist = 0.7f;

            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = SoundID.NPCDeath2;
            NPC.value = 400f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)].ZoneMarble)
                return SpawnCondition.Meteor.Chance * 1f;
            else
                return SpawnCondition.Meteor.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Gladius, 20));
            npcLoot.Add(ItemDropRule.Common(ItemID.Pizza, 20));
        }
    }
}
