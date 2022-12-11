using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GalacticMod.Assets.Systems;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Rarities;
using GalacitcMod.Items;

namespace GalacticMod.Items.Hardmode.Ancient
{
    [AutoloadEquip(EquipType.Head)]
    public class AncientMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("20% increased throwing attack speed" +
                "\n15% increased throwing damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Cyan;
            Item.width = 28;
            Item.height = 30;

            Item.defense = 23;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<AncientCuirass>() && legs.type == ModContent.ItemType<AncientGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increased throwing stats";
            player.GetAttackSpeed(DamageClass.Throwing) += 0.2f;
            player.GetDamage(DamageClass.Throwing) += 0.2f;
            player.GetCritChance(DamageClass.Throwing) += 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Throwing) += 0.2f;
            player.GetDamage(DamageClass.Throwing) += 0.15f;
        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Body)]
    public class AncientCuirass : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Increased throwing crit by 2" +
                "\n10% increased throwing damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Cyan;

            Item.defense = 29;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Throwing) += 0.1f;
            player.GetCritChance(DamageClass.Throwing) += 2;
        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Legs)]
    public class AncientGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("10% increased movement speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Cyan;

            Item.defense = 22;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.1f;
        }
    }
}