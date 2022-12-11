using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using System;

namespace GalacticMod.Items.Consumables
{
    public class Strawberry : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Minor improvements to all stats" +
                "\nYum Yum Yum");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 20;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Green;
            Item.value = 10000; //sell value
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 5 * 60 * 60;
        }
    }
}