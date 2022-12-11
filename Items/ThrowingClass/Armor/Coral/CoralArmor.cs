using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacitcMod.Items;

namespace GalacticMod.Items.ThrowingClass.Armor.Coral
{
    [AutoloadEquip(EquipType.Head)]
    public class CoralHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("5% Increased Throwing damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 2;
            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<CoralHelmet>());
            recipe.AddIngredient(ItemID.Coral, 40);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<CoralBreastplate>() && legs.type == ModContent.ItemType<CoralGreaves>();
        }

        public override void UpdateArmorSet(Player Player)
        {
            Player.setBonus = "Allows waterbreathing";
            Player.gills = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.GetDamage(DamageClass.Throwing) += 0.05f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class CoralBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased Throwing Critical Strike Chance");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 3;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.GetCritChance(DamageClass.Throwing) += 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<CoralBreastplate>());
            recipe.AddIngredient(ItemID.Coral, 50);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class CoralGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("5% Increased Throwing Damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 2;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.GetDamage(DamageClass.Throwing) += 0.05f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<CoralGreaves>());
            recipe.AddIngredient(ItemID.Coral, 40);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}