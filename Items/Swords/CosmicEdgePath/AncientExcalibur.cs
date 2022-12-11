using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Creative;
using System;

namespace GalacticMod.Items.Swords.CosmicEdgePath
{
    public class AncientExcalibur : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The Blade of a King");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 72;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = 1000; //sell value
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true; //autoswing
            Item.useTurn = true; //player can turn while animation is happening
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<AncientExcalibur>());
            recipe.AddRecipeGroup("GalacticMod:CobaltSword");
            recipe.AddRecipeGroup("GalacticMod:MythrilSword");
            recipe.AddRecipeGroup("GalacticMod:AdamantiteSword");
            recipe.AddIngredient(ItemID.SoulofMight, 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}