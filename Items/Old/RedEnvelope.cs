using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using System;

namespace GalacticMod.Items.Old
{
    public class RedEnvelope : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}" + // References a language key that says "Right Click To Open" in the language of the game
                "\nRelic of a Lost Age"); 

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 2);
        }

        public override bool CanRightClick() => true;

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            // Drop a special weapon/accessory etc. specific to this crate's theme (i.e. Sky Crate dropping Fledgling Wings or Starfury)
            int[] themedDrops = new int[] {
                ItemID.WhitePearl,
                ItemID.GoldCoin,
                ItemID.LadyBug,
                ItemID.Oyster
            };
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, themedDrops));

            //Firecracker
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Firecracker>(), 1, 20, 40));

            //Drop coins
            itemLoot.Add(ItemDropRule.Common(ItemID.CopperCoin, 1, 50, 500));
        }
    }
}