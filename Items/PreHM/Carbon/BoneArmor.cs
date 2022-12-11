using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacitcMod.Items;

namespace GalacticMod.Items.PreHM.Carbon
{
    [AutoloadEquip(EquipType.Head)]
    public class BoneHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("7% Increased Throwing damage");
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
            Recipe recipe = Recipe.Create(ModContent.ItemType<BoneHelmet>());
            recipe.AddIngredient(Mod, "Carbonite", 16);
            recipe.AddIngredient(ItemID.Bone, 40);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BoneChestplate>() && legs.type == ModContent.ItemType<BoneGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increased throwing stats";
            player.GetAttackSpeed(DamageClass.Throwing) += 0.1f;
            player.GetDamage(DamageClass.Throwing) += 0.1f;
            player.GetCritChance(DamageClass.Throwing) += 1;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Throwing) += 0.07f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class BoneChestplate : ModItem
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
            Item.defense = 6;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.GetCritChance(DamageClass.Throwing) += 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<BoneChestplate>());
            recipe.AddIngredient(Mod, "Carbonite", 24);
            recipe.AddIngredient(ItemID.Bone, 60);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class BoneGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("7.5% increased movement speed");
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
            Player.moveSpeed += 0.075f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<BoneGreaves>());
            recipe.AddIngredient(Mod, "Carbonite", 18);
            recipe.AddIngredient(ItemID.Bone, 50);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}