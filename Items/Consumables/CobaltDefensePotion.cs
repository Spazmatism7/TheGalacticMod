using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Consumables
{
    public class CobaltDefensePotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Grants Defense");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 10000; //sell value
            Item.buffType = BuffType<Buffs.CobaltDefense>(); //Specify an existing buff to be applied when used.
            Item.buffTime = 5400; //The amount of time the buff declared in item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<CobaltDefensePotion>());
            recipe.AddIngredient(ItemID.CobaltOre, 4);
            recipe.AddIngredient(ItemID.Bottle);
            recipe.AddTile(TileID.Bottles);
            recipe.Register();
        }
    }
}