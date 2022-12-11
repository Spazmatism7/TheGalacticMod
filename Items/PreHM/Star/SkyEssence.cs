using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;

namespace GalacticMod.Items.PreHM.Star
{
    public class SkyEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The essence of the Sky God");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 3));
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Orange;
        }
    }
}
