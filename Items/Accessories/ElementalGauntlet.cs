using GalacticMod.Tiles;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using System;
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.Accessories
{
    public class ElementalGauntlet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases melee knockback and size" +
                "\n12% increased melee damage and speed" +
                "\nEnables auto swing for melee weapons" +
                "\nMelee attacks inflict fire, frostburn, shadowflame, and poison");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.kbGlove = true;
            player.autoReuseGlove = true;
            player.meleeScaleGlove = true;
            player.GetAttackSpeed(DamageClass.Melee) += 0.12f;
            player.GetDamage(DamageClass.Melee) += 0.12f;
            player.magmaStone = true;

            player.GetModPlayer<GalacticPlayer>().shadowflame = true;

            player.GetModPlayer<GalacticPlayer>().elementalGauntlet = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<ElementalGauntlet>());
            recipe.AddIngredient(ItemID.FireGauntlet);
            recipe.AddIngredient(ItemID.IceTorch, 99);
            recipe.AddIngredient(Mod, "Shadowflame");
            recipe.AddIngredient(ItemID.Stinger, 25);
            recipe.AddIngredient(Mod, "BarOfLife", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}