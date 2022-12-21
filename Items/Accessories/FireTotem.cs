using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Systems;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Items.Accessories
{
    public class FireTotem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Using weapons rains fire towards the cursor");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        }
        public override void SetDefaults()
        {
            Item.canBePlacedInVanityRegardlessOfConditions = true;

            Item.width = 16;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;

            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<GalacticPlayer>().fireTotem = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<FireTotem>());
            recipe.AddIngredient(ItemID.HellstoneBar, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}