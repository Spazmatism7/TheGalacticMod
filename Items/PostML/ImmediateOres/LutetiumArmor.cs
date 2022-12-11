using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GalacticMod.Assets.Systems;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Rarities;
using GalacticMod.Buffs;

namespace GalacticMod.Items.PostML.ImmediateOres
{
    [AutoloadEquip(EquipType.Head)]
    public class LutetiumHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Really hard" +
                "\nYou're that bad at dodging?");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Purple;
            Item.width = 28;
            Item.height = 30;

            Item.defense = 28;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<LutetiumHelm>());
            recipe.AddIngredient(Mod, "LutetiumBar", 17);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<LutetiumCuirass>() && legs.type == ModContent.ItemType<LutetiumLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            /*player.setBonus = "Hitting enemies grants increased defense";
            player.GetModPlayer<GalacticPlayer>().LutetiumGuard = true;*/

            var list = GalacticMod.ArmourSpecialHotkey.GetAssignedKeys();
            string keyName = "(Not bound, set in controls)";

            if (list.Count > 0)
            {
                keyName = list[0];
            }

            player.setBonus = "Pressing '" + keyName + "' hardens the armor for a short time";

            if (GalacticMod.ArmourSpecialHotkey.Current && !player.HasBuff(ModContent.BuffType<LutetiumArmor>()) && !player.HasBuff(ModContent.BuffType<LutetiumCooldown>()))
            {
                player.AddBuff(ModContent.BuffType<LutetiumArmor>(), 15 * 60);
                player.AddBuff(ModContent.BuffType<LutetiumCooldown>(), 18 * 60);
            }
        }

        public override void ArmorSetShadows(Player player)
        {
            if (player.HasBuff(ModContent.BuffType<LutetiumArmor>()))
                player.armorEffectDrawOutlines = true;
            else
                player.armorEffectDrawOutlines = false;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class LutetiumCuirass : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("You're that bad at dodging?");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Purple;

            Item.defense = 33;
        }

        public override void UpdateEquip(Player player)
        {
            player.dashType = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<LutetiumCuirass>());
            recipe.AddIngredient(Mod, "LutetiumBar", 23);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();

        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Legs)]
    public class LutetiumLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Increased movement speed" +
                "\nYou're that bad at dodging?");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Purple;

            Item.defense = 29;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<LutetiumLeggings>());
            recipe.AddIngredient(Mod, "LutetiumBar", 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}