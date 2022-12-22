using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using GalacticMod.Assets.Systems;
using GalacticMod.Items.PreHM.Blood;
using Terraria.GameContent.ItemDropRules;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Items.Accessories
{
    public class Shadowflame : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Melee attacks inflict shadowflame");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = 10000;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<GalacticPlayer>().shadowflame = true;
        }
    }

    class ShadowflameNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.GoblinSummoner)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<Shadowflame>(), 3));
            }
        }
    }
}