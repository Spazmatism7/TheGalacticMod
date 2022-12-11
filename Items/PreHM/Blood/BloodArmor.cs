using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GalacticMod.Assets.Systems;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Rarities;
using GalacitcMod.Items;

namespace GalacticMod.Items.PreHM.Blood
{
    [AutoloadEquip(EquipType.Head)]
    public class BloodMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Green;
            Item.width = 28;
            Item.height = 30;

            Item.defense = 4;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<BloodMask>());
            recipe.AddIngredient(Mod, "BloodyDrop", 30);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BloodBreastplate>() && legs.type == ModContent.ItemType<BloodGreaves>();
        }

         public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;

            int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.Blood, player.velocity.X * 1f, player.velocity.Y * 1f, 130, default, 1.5f);

        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Grants Immunity to Slow and Bleeding" +
                "\nGrants an extra minion slot";
            player.buffImmune[30] = true;
            player.buffImmune[32] = true;
            player.maxMinions++;
        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Body)]
    public class BloodBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Green;

            Item.defense = 5;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<BloodBreastplate>());
            recipe.AddIngredient(Mod, "BloodyDrop", 40);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Legs)]
    public class BloodGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ItemRarityID.Green;

            Item.defense = 4;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<BloodGreaves>());
            recipe.AddIngredient(Mod, "BloodyDrop", 30);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}