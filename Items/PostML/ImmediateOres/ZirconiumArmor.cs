using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GalacticMod.Assets.Systems;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Rarities;

namespace GalacticMod.Items.PostML.ImmediateOres
{
    [AutoloadEquip(EquipType.Head)]
    public class ZirconiumHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("10% Increased attack speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Purple;
            Item.width = 28;
            Item.height = 30;

            Item.defense = 22;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<ZirconiumHat>());
            recipe.AddIngredient(Mod, "Zirconium", 17);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ZirconiumRobe>() && legs.type == ModContent.ItemType<ZirconiumPants>();
        }

        public override void UpdateArmorSet(Player player)
        {
            var list = GalacticMod.ArmourSpecialHotkey.GetAssignedKeys();
            string keyName = "(Not bound, set in controls)";

            if (list.Count > 0)
            {
                keyName = list[0];
            }

            player.setBonus = "Press '" + keyName + "' to the cursor's location within a limited range" +
                "\nWarping has a 7 second cooldown";

            player.GetModPlayer<GalacticPlayer>().ZirconiumWarp = true;

            /*player.setBonus = "Hitting enemies grants increased movement speed";
            player.GetModPlayer<GalacticPlayer>().ZirconiumSpeed = true;*/
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Body)]
    public class ZirconiumRobe : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Grants the ability to dash into enemies");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Purple;

            Item.defense = 26;
        }

        public override void UpdateEquip(Player player)
        {
            player.dashType = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<ZirconiumRobe>());
            recipe.AddIngredient(Mod, "Zirconium", 23);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Legs)]
    public class ZirconiumPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Increased movement speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Purple;

            Item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.2f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<ZirconiumPants>());
            recipe.AddIngredient(Mod, "Zirconium", 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}