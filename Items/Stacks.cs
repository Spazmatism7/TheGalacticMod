using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GalacticMod.Assets.Systems;
using GalacticMod.Assets.Config;
using static Terraria.ModLoader.ModContent;

namespace GalacitcMod.Items
{
	public class Stacks : GlobalItem
	{
		public override void SetDefaults(Item Item)
		{
			if (Item.type is 267 or 1307) //Voodoo Dolls //remove 267 after 1.4.4
			{
				Item.maxStack = GetInstance<GalacticModConfig>().VoodooDollStackCount;
			}
			if (Item.type == ItemID.TempleKey) //Temple Key
            {
				Item.maxStack = 999;
            }
			if (Item.type is 43 or 560 or 70 or 544 or 556 or 557 or 1293) //Sus Eye, Slime Crown, Worm Food, Mech Eye, Mech Worm, Mech Skull, Lihzhard Power Cell
			{
				Item.maxStack = GetInstance<GalacticModConfig>().BossSummonStackCount;
			}
		}
	}
}
