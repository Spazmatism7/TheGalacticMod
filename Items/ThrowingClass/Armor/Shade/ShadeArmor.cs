using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Systems;
using GalacitcMod.Items;

namespace GalacticMod.Items.ThrowingClass.Armor.Shade
{
    [AutoloadEquip(EquipType.Head)]
    public class ShadeHat : ModItem
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
            return body.type == ModContent.ItemType<ShadeShirt>() && legs.type == ModContent.ItemType<ShadePants>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Throwing attacks have a chance to summon shades";
            player.GetModPlayer<GalacticPlayer>().shadeBonus = true;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Throwing) += 0.2f;
            player.GetDamage(DamageClass.Throwing) += 0.1f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class ShadeShirt : ModItem
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

    [AutoloadEquip(EquipType.Legs)]
    public class ShadePants : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased movement speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.LightPurple;
            Item.defense = 15;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.moveSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<ShadePants>());
            recipe.AddIngredient(ItemID.ChlorophyteBar, 23);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}