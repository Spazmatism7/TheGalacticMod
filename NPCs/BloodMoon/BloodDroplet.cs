using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Items.PreHM.Blood;
using GalacticMod.Items.Consumables;

namespace GalacticMod.NPCs.BloodMoon
{
	public class BloodDroplet : ModNPC
	{
		public override void SetStaticDefaults()
        {
			Main.npcFrameCount[NPC.type] = 2;
		}

		public override void SetDefaults()
		{
			NPC.width = 32;
			NPC.height = 22;
			NPC.damage = 25;
			NPC.defense = 7;
			NPC.lifeMax = 80;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath2;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.aiStyle = 1;
			AIType = 1;
			AnimationType = 1;
			Banner = Item.NPCtoBanner(1);
			BannerItem = Item.BannerToItem(Banner);
			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{ // Influences how the NPC looks in the Bestiary
				Velocity = 0f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
			};
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			// We can use AddRange instead of calling Add multiple times in order to add multiple items at once
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A slime that absorded the power of the blood moon")
			});
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.bloodMoon && NPC.downedBoss1) // if blood moon and post-EoC
			{
				return .1f;
			}
			else
			{
				return 0f;
			}
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemType<BloodyDrop>(), 1, 2, 3));
            npcLoot.Add(ItemDropRule.Common(ItemType<Strawberry>(), 50, 1));
        }
	}

    class BloodyDropNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type is NPCID.Drippler or NPCID.BloodZombie or NPCID.GoblinShark && NPC.downedBoss1)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<BloodyDrop>(), 7));
            }
        }
    }
}
