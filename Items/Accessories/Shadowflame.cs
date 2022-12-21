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
    public class Shadowflame : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Melee attacks inflict shadowflame");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = 10000;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<GalacticPlayer>().shadowflame = true;
        }
    }
}