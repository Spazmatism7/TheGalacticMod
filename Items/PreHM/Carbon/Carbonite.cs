using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;

namespace GalacticMod.Items.PreHM.Carbon
{
	public class Carbonite : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 22;
			Item.height = 22;
			Item.value = 3000;
			Item.rare = ItemRarityID.Orange;
		}
	}

    class CarbonNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type is NPCID.AngryBones or NPCID.AngryBonesBig or NPCID.AngryBonesBigHelmet or NPCID.AngryBonesBigMuscle || (NPC.downedBoss3 && npc.type is NPCID.Skeleton or 
                NPCID.SkeletonAlien or NPCID.SkeletonArcher or NPCID.SkeletonAstonaut or NPCID.SkeletonCommando or NPCID.SkeletonSniper or NPCID.SkeletonTopHat or NPCID.BigPantlessSkeleton or 
                NPCID.BigSkeleton or NPCID.ArmoredSkeleton or NPCID.BigHeadacheSkeleton or NPCID.BigMisassembledSkeleton or NPCID.BoneThrowingSkeleton or NPCID.BoneThrowingSkeleton2 or 
                NPCID.BoneThrowingSkeleton3 or NPCID.BoneThrowingSkeleton4 or NPCID.DD2SkeletonT1 or NPCID.DD2SkeletonT3 or NPCID.CursedSkull or NPCID.GiantCursedSkull or 
                NPCID.HeadacheSkeleton or NPCID.HeavySkeleton or NPCID.MisassembledSkeleton or NPCID.PantlessSkeleton or NPCID.GreekSkeleton or NPCID.SmallHeadacheSkeleton or 
                NPCID.SmallMisassembledSkeleton or NPCID.SmallPantlessSkeleton or NPCID.SmallSkeleton or NPCID.SporeSkeleton or NPCID.TacticalSkeleton))
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<Carbonite>(), 10));
            }
        }
    }
}