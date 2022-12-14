using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.PostPlantera
{
    public class BeetleHamaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            DisplayName.SetDefault("Darkling Hamaxe");
        }

        public override void SetDefaults()
        {
            Item.damage = 62;
            Item.DamageType = DamageClass.Melee;
            Item.width = 50;
            Item.height = 54;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.axe = 175;          //How much axe power the weapon has, note that the axe power displayed in-game is this value multiplied by 5
            Item.hammer = 110;      //How much hammer power the weapon has
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.rare = ItemRarityID.Yellow;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<BeetleHamaxe>());
            recipe.AddIngredient(ItemID.BeetleHusk, 13);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}