using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using GalacticMod.Items.PostML.Celestial;
using System;

namespace GalacticMod.Items.Boss
{
    [AutoloadEquip(EquipType.Wings)]
    public class SkyGodCrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crown of the Sky God");
            Tooltip.SetDefault("Allows flight and slow fall");
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(60, 8.5f, 1.5f);
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Expert;
            Item.width = 46;
            Item.height = 36;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }
    }
}