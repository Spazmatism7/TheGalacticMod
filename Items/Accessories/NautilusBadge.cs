using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GalacticMod.Assets.Systems;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Items.Accessories
{
    public class NautilusBadge : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("2 Nautilus Spheres will orbit you, damaging and knocking back enemies");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.value = Item.sellPrice(0, 0, 75, 0);
            Item.rare = ItemRarityID.Blue;

            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<GalacticPlayer>().nautilusBadge = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<NautilusBadge>());
            recipe.AddIngredient(ItemID.Seashell, 10);
            recipe.AddIngredient(ItemID.Coral, 6);
            recipe.AddIngredient(ItemID.Starfish, 4);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}