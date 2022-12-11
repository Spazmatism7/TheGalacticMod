using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.DataStructures;
using System;

namespace GalacticMod.Items.PreHM.Nautilus
{
    // This is an example bug net designed to demonstrate the use cases for various hooks related to catching NPCs such as critters with items.
    public class BenthicBugNet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Used for catching critters");

            // This set is needed to define an item as a tool for catching NPCs at all.
            // An additional set exists called LavaproofCatchingTool which will allow your item to freely catch the Underworld's lava critters. Use it accordingly.
            ItemID.Sets.CatchingTool[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // These are, with a few modifications, the properties applied to the base Bug Net; they're provided here so that you can mess with them as you please.
            // Explanations on them will be glossed over here, as they're not the primary point of the lesson.
            // Common Properties
            Item.width = 44;
            Item.height = 48;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(0, 0, 40);

            // Use Properties
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
        }
    }
}