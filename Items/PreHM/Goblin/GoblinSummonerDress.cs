using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Input;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using GalacitcMod.Items;

namespace GalacticMod.Items.PreHM.Goblin
{
    [AutoloadEquip(EquipType.Body)]
    public class GoblinSummonerDress : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("%10 increased summon damage" +
                "\n+1 minion slots");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 13;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.GetDamage(DamageClass.Summon) += 0.1f;
            Player.slotsMinions++;
        }
    }

    public class DarkGoblinSet : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.GoblinSummoner)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<GoblinSummonerDress>(), 20));
            }
        }
    }
}