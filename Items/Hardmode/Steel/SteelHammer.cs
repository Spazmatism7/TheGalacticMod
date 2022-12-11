using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace GalacticMod.Items.Hardmode.Steel
{
    public class SteelHammer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Steel Warhammer");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.hammer = 90;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 10;
            Item.rare = ItemRarityID.Pink;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<SteelHammer>());
            recipe.AddIngredient(Mod, "SteelBar", 13);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}