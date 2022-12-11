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
    public class OsmiumMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("7% increased damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Purple;
            Item.width = 28;
            Item.height = 30;

            Item.defense = 21;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<OsmiumMask>());
            recipe.AddIngredient(Mod, "OsmiumBar", 17);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<OsmiumMail>() && legs.type == ModContent.ItemType<OsmiumGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            /*player.setBonus = "Striking a enemy consumes their soul and uses it to fuel your rampage";
            player.GetModPlayer<GalacticPlayer>().OsmiumDamage = true;*/

            var list = GalacticMod.ArmourSpecialHotkey.GetAssignedKeys();
            string keyName = "(Not bound, set in controls)";

            if (list.Count > 0)
            {
                keyName = list[0];
            }

            player.setBonus = "Pressing '" + keyName + "' grants you immense power for a short time" +
                "\nYour damage, critical strike chance and speed will be buffed, but your defenses will take a toll";

            if (GalacticMod.ArmourSpecialHotkey.Current && !player.HasBuff(ModContent.BuffType<Bloodthirsty>()) && !player.HasBuff(ModContent.BuffType<OsmiumCooldown>()))
            {
                player.AddBuff(ModContent.BuffType<Bloodthirsty>(), 20 * 60);
                player.AddBuff(ModContent.BuffType<OsmiumCooldown>(), 30 * 60);
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.07f;
        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Body)]
    public class OsmiumMail : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("10% increased damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Purple;

            Item.defense = 24;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<OsmiumMail>());
            recipe.AddIngredient(Mod, "OsmiumBar", 23);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Legs)]
    public class OsmiumGreaves : ModItem
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
            Recipe recipe = Recipe.Create(ModContent.ItemType<OsmiumGreaves>());
            recipe.AddIngredient(Mod, "OsmiumBar", 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}