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
    public class GoblinArcherHat : ModItem
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
            return body.type == ItemType<GoblinArcherVest>() && legs.type == ItemType<GoblinArcherPants>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased ranged damage" +
                "\n10% increased movement speed";
            player.GetDamage(DamageClass.Ranged) += 0.1f;
            player.moveSpeed += 0.1f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class GoblinArcherVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased arrow damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 6;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.arrowDamage += 0.1f;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class GoblinArcherPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased ranged damage");
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

        public override void UpdateEquip(Player Player)
        {
            Player.GetDamage(DamageClass.Ranged) += 0.1f;
        }
    }

    public class GoblinArcherSet : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.GoblinArcher)
            {
                npcLoot.Add(ItemDropRule.OneFromOptions(20, ItemType<GoblinArcherHat>(), ItemType<GoblinArcherVest>(), ItemType<GoblinArcherPants>()));
                npcLoot.Add(ItemDropRule.Common(ItemType<GoblinBow>(), 20));
            }
        }
    }

    public class GoblinBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 38;
            Item.useTime = 26;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 7f;
            Item.useAmmo = AmmoID.Arrow;
        }
    }
}