using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.GameContent.Creative;
using GalacticMod.Items.PreHM.Mage;
using GalacticMod.Items.GamemodeItems;
using Terraria.GameContent.ItemDropRules;

namespace GalacticMod.Items.PreHM
{
	internal class StarterBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starter Bag");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 30;
			Item.height = 42;
		}

		public override bool CanRightClick()
		{
			return true;
		}

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            itemLoot.Add(ItemDropRule.Common(ItemID.WoodenBow));
            itemLoot.Add(ItemDropRule.Common(ItemID.WoodenArrow, 1, 100, 100));
            itemLoot.Add(ItemDropRule.Common(ItemType<PrototypePrism>()));
            itemLoot.Add(ItemDropRule.Common(ItemID.Shuriken, 1, 50, 60));
            itemLoot.Add(ItemDropRule.Common(ItemID.ManaCrystal));
            itemLoot.Add(ItemDropRule.Common(ItemID.Torch, 1, 25, 75));
            itemLoot.Add(ItemDropRule.Common(ItemID.Rope, 1, 25, 75));
        }
	}
}