using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Items.Dev;
using Terraria.GameContent.ItemDropRules;
using GalacticMod.Items.Hardmode.PostPlantera;

namespace GalacticMod.Assets.Systems
{
    public class GrabBags : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public bool Deathbringer;

        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            if (GetInstance<Gamemodes>().Deathbringer)
                Deathbringer = true;
            else
                Deathbringer = false;
        }

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            // In addition to this code, we also do similar code in Common/GlobalNPCs/ExampleNPCLoot.cs to edit the boss loot for non-expert drops. Remember to do both if your edits should affect non-expert drops as well.
            if (item.type == ItemID.PlanteraBossBag)
                itemLoot.Add(ItemDropRule.Common(ItemType<BarOfLife>(), 1, 10, 10));

            if (item.type == ItemID.GolemBossBag)
                itemLoot.Add(ItemDropRule.Common(ItemType<RockEdge>(), 6));

            if (item.type is ItemID.QueenSlimeBossBag or ItemID.DestroyerBossBag or ItemID.TwinsBossBag or ItemID.SkeletronPrimeBossBag or ItemID.PlanteraBossBag or ItemID.GolemBossBag or 
                ItemID.FairyQueenBossBag or ItemID.FishronBossBag or ItemID.CultistBossBag or ItemID.BossBagBetsy or ItemID.MoonLordBossBag)
            {
                itemLoot.Add(ItemDropRule.OneFromOptions(50, ItemType<PaintStrype>(), ItemType<Flyswatter>()));
            }

            if (Deathbringer)
            {

            }
        }
    }
}