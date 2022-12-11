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
    public class VanadiumHeaume : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Increased Life regen");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Purple;
            Item.width = 28;
            Item.height = 30;

            Item.defense = 23;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<VanadiumHeaume>());
            recipe.AddIngredient(Mod, "VanadiumBar", 17);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<VanadiumCuirass>() && legs.type == ModContent.ItemType<VanadiumJambeau>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "When you hit an enemy, you have increased healing for 3 seconds" +
                "\nIf under 10% health, life regen is increased";
            player.GetModPlayer<GalacticPlayer>().VanadiumHeal = true;
            if (player.statLife <= (player.statLifeMax2 * 0.1f))
            {
                player.lifeRegen += 1;
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.lifeRegen += 4;
        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Body)]
    public class VanadiumCuirass : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Increased Life regen" +
                "\nIncreases maximum mana by 60");
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
            player.lifeRegen += 3;
            player.statManaMax2 += 60;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<VanadiumCuirass>());
            recipe.AddIngredient(Mod, "VanadiumBar", 23);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Legs)]
    public class VanadiumJambeau : ModItem
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

            Item.defense = 23;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.2f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<VanadiumJambeau>());
            recipe.AddIngredient(Mod, "VanadiumBar", 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}