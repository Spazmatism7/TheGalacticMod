using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;

namespace GalacticMod.Items.Hardmode.Amaza
{
    public class AmazaHamaxe : ModItem
    {
        public override string Texture => GetInstance<Assets.Config.PersonalConfig>().ClassicAmaza ? base.Texture + "_Old" : base.Texture;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Smash and Chop!");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 72;
            Item.DamageType = DamageClass.Melee; //melee weapon
            Item.width = 54;
            Item.height = 54;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.axe = 175;          //How much axe power the weapon has, note that the axe power displayed in-game is this value multiplied by 5
            Item.hammer = 100;      //How much hammer power the weapon has
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.rare = ItemRarityID.LightPurple;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<AmazaHamaxe>());
            recipe.AddIngredient(Mod, "AmazaBar", 13);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}