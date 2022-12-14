using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Items.PreHM.Blood;
using Microsoft.Xna.Framework;
using GalacticMod.NPCs.Banners;

namespace GalacticMod.NPCs.BloodMoon
{
    public class BloodyAglamation : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 9;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 9;

            NPC.width = 40;
            NPC.height = 46;

            NPC.aiStyle = 3;
            AIType = NPCID.BloodZombie;
            AnimationType = NPCID.FlyingSnake;

            NPC.lifeMax = 450;
            NPC.damage = 50;
            NPC.defense = 30;
            NPC.knockBackResist = 0.45f;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath43;
            Banner = NPC.type;
            BannerItem = ItemType<BloodAglamationBanner>();
            NPC.value = Item.buyPrice(0, 0, 25, 0);
        }

        public override void AI()
        {
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.OnFire3] = true;
            NPC.buffImmune[BuffID.Confused] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.Frostburn2] = true;
            NPC.buffImmune[BuffID.ShadowFlame] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
        }

        public override bool? CanFallThroughPlatforms() => true;

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("A disgusting hybrid of a Blood Zombie and a Drippler")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.bloodMoon && NPC.downedBoss1)
                return .05f;
            else
                return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<BloodyDrop>(), 1, 3, 4));
            npcLoot.Add(ItemDropRule.Common(ItemID.SharkToothNecklace, 100));
            npcLoot.Add(ItemDropRule.Common(ItemID.MoneyTrough, 12));
            npcLoot.Add(ItemDropRule.Common(ItemID.BloodMoonStarter, 15));
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_4").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_5").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                }
            }
        }
    }
}
