using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using System;

namespace GalacticMod.Items.PreHM.Nautilus
{
    public class BenthicBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}"); // References a language key that says "Right Click To Open" in the language of the game

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
        }

        public override void SetDefaults()
        {
            Item.width = 29;
            Item.height = 18;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 2);
        }

        public override bool CanRightClick() => true;

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            int[] oceanDrops = new int[] {
                ItemID.Coral,
                ItemID.Seashell,
                ItemID.Starfish,
            };
            #region Ocean Drop Amounts
            //Always drop
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, oceanDrops));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, oceanDrops));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, oceanDrops));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, oceanDrops));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, oceanDrops));
            
            //Drops 50% of previous
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(2, oceanDrops));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(4, oceanDrops));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(8, oceanDrops));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(16, oceanDrops));
            #endregion

            int[] cosmeticOceanDrops = new int[] {
                ItemID.JunoniaShell,
                ItemID.LightningWhelkShell,
                ItemID.TulipShell,
            };
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(1, cosmeticOceanDrops));
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(2, cosmeticOceanDrops));

            int[] fishDrops = new int[] {
                ItemID.Trout,
                ItemID.Tuna,
                ItemID.RedSnapper,
                ItemID.Shrimp,
                ItemID.PinkJellyfish
            };
            itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(3, fishDrops));

            //Drop coins
            itemLoot.Add(ItemDropRule.Common(ItemID.CopperCoin, 1, 50, 112));
        }
    }
}