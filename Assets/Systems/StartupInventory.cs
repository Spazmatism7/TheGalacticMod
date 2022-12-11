using GalacticMod.Items.PreHM;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GalacticMod.Assets.Config;
using GalacticMod.Items.GamemodeItems;

namespace GalacticMod.Assets.Systems
{
	public class StartupInventory : ModPlayer
	{
		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
            if (ModContent.GetInstance<GalacticModConfig>().StartBag)
            {
                return new[] {
                    new Item(ModContent.ItemType<StarterBag>())
                };
            }

            return new[] {
                new Item(ModContent.ItemType<NoHitMode>()),
                new Item(ModContent.ItemType<BloodMode>())
            };
        }
	}
}