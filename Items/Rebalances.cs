using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GalacticMod.Assets.Systems;
using GalacticMod.Assets.Config;
using static Terraria.ModLoader.ModContent;
using System.Collections.Generic;
using System.Linq;

namespace GalacitcMod.Items
{
    public class Rebalances : GlobalItem
    {
        public override void SetDefaults(Item Item)
        {
            switch (Item.type)
            {
                case ItemID.BloodHamaxe:
                    Item.axe = 140;
                    break;
            }
        }
    }
}

