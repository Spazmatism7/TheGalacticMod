using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacitcMod.Items;

namespace GalacticMod.Items.PreHM.Star
{
    [AutoloadEquip(EquipType.Head)]
    public class StarHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("7% Increased Magic/Summon damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 4;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<StarHelmet>());
            recipe.AddIngredient(Mod, "Stardust", 30);
            recipe.AddRecipeGroup("GalacticMod:GoldBar", 17);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<StarChestplate>() && legs.type == ModContent.ItemType<StarLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+40 mana" +
                "\n10% increased Magic/Summon damage";
            player.statManaMax2 += 40;
            player.GetDamage(DamageClass.MagicSummonHybrid) += 0.1f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.MagicSummonHybrid) += 0.07f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class StarChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased Magic/Summoning Critical Strike Chances");
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
            Player.GetCritChance(DamageClass.MagicSummonHybrid) += 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<StarChestplate>());
            recipe.AddIngredient(Mod, "Stardust", 40);
            recipe.AddRecipeGroup("GalacticMod:GoldBar", 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class StarLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased movement speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 4;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.moveSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<StarLeggings>());
            recipe.AddIngredient(Mod, "Stardust", 35);
            recipe.AddRecipeGroup("GalacticMod:GoldBar", 17);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}