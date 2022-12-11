using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Systems;
using GalacitcMod.Items;

namespace GalacticMod.Items.ThrowingClass.Armor.Sensi
{
    [AutoloadEquip(EquipType.Head)]
    public class SensiHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% increased throwing attack speed" +
                "\n10% increased throwing damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.LightPurple;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 11;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<OldRobe>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increased throwing damage and crit";
            player.GetDamage(DamageClass.Throwing) += 0.15f;
            player.GetCritChance(DamageClass.Throwing) += 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Throwing) += 0.2f;
            player.GetDamage(DamageClass.Throwing) += 0.1f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class OldRobe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased throwing crit by 2" +
                "\n10% increased throwing damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.LightPurple;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 24;
            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;

        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Throwing) += 0.1f;
            player.GetCritChance(DamageClass.Throwing) += 2;
        }
    }
}