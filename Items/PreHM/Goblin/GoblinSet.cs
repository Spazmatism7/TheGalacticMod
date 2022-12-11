using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;
using GalacitcMod.Items;

namespace GalacticMod.Items.PreHM.Goblin
{
    [AutoloadEquip(EquipType.Head)]
    public class GoblinCap : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 24;
            Item.height = 20;
            Item.defense = 5;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<GoblinShirt>() && legs.type == ItemType<GoblinPants>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "2% increased damage and attack speed" +
                "7% increased movement speed";
            player.GetDamage(DamageClass.Generic) += 0.02f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.02f;
            player.moveSpeed += 0.07f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class GoblinShirt : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 7;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class GoblinPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 5;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }
    }

    public class GoblinSet : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.GoblinPeon)
            {
                npcLoot.Add(ItemDropRule.OneFromOptions(20, ItemType<GoblinCap>(), ItemType<GoblinShirt>(), ItemType<GoblinPants>()));
            }
        }
    }
}