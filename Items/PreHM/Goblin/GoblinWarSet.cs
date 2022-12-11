using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;
using GalacitcMod.Items;
using GalacticMod.Items.Swords.CosmicEdgePath;
using Terraria.DataStructures;
using Terraria.Enums;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace GalacticMod.Items.PreHM.Goblin
{
    [AutoloadEquip(EquipType.Head)]
    public class GoblinWarHelm : ModItem
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
            Item.defense = 7;

            GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<GoblinWarBreastplate>() && legs.type == ItemType<GoblinWarGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased melee damage" +
                "\n+2 defense";
            player.GetDamage(DamageClass.Melee) += 0.1f;
            player.statDefense += 2;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class GoblinWarBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased melee damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 8;

            GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.GetDamage(DamageClass.Melee) += 0.1f;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class GoblinWarGreaves : ModItem
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

            GetInstance<RarityToCostArmor>().modArmor = true;
        }
    }

    public class GoblinWarSet : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.GoblinWarrior)
            {
                npcLoot.Add(ItemDropRule.OneFromOptions(20, ItemType<GoblinWarHelm>(), ItemType<GoblinWarBreastplate>(), ItemType<GoblinWarGreaves>()));
                npcLoot.Add(ItemDropRule.Common(ItemType<GoblinBlade>(), 20));
            }
        }
    }

    public class GoblinBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 23;
            Item.DamageType = DamageClass.Melee;
            Item.width = 38;
            Item.height = 38;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 1);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
        }
    }
}