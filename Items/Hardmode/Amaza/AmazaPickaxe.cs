using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.Hardmode.Amaza
{
    public class AmazaPickaxe : ModItem
    {
        public override string Texture => ModContent.GetInstance<Assets.Config.PersonalConfig>().ClassicAmaza ? base.Texture + "_Old" : base.Texture;

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Mines blocks in a 3x3 radius");
        }

        public override void SetDefaults()
        {
            Item.damage = 72;
            Item.DamageType = DamageClass.Melee; //melee weapon
            Item.width = 44;
            Item.height = 44;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.pick = 190;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.rare = ItemRarityID.LightPurple;
            Item.autoReuse = true;
            Item.tileBoost = 2;
            Item.GetGlobalItem<AoEPickaxe>().miningRadius = 1;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<AmazaPickaxe>());
            recipe.AddIngredient(Mod, "AmazaBar", 18);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}