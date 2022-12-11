using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System;
using GalacitcMod.Items;

namespace GalacticMod.Items.Hardmode.Asteroid
{
    [AutoloadEquip(EquipType.Head)]
    public class IridiumHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased Critical Strike Chance by 4");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.LightPurple;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 17;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<IridiumHelm>());
            recipe.AddIngredient(Mod, "IridiumBar", 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<IridiumBreastplate>() && legs.type == ModContent.ItemType<IridiumPants>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased damage" +
                "\nIncreased armor penetration";
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.GetArmorPenetration(DamageClass.Generic) += 0.1f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 4;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class IridiumBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.LightPurple;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 19;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.10f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<IridiumBreastplate>());
            recipe.AddIngredient(Mod, "IridiumBar", 25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class IridiumPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("12% increased movement speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.LightPurple;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 18;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.moveSpeed += 0.12f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<IridiumPants>());
            recipe.AddIngredient(Mod, "IridiumBar", 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}