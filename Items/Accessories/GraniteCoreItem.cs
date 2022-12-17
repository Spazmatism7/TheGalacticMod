using GalacticMod.Tiles;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using System;
using GalacticMod.Buffs;

namespace GalacticMod.Items.Accessories
{
    public class GraniteCoreItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            var list = GalacticMod.ArmourSpecialHotkey.GetAssignedKeys();
            string keyName = "(Not bound, set in controls)";

            if (list.Count > 0)
            {
                keyName = list[0];
            }

            DisplayName.SetDefault("Granite Core");
            Tooltip.SetDefault("Press and hold '" + keyName + "' places you in a form of stasis");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.value = 10000;
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }

        public override void UpdateEquip(Player player)
        {
            if (GalacticMod.ArmourSpecialHotkey.Current && !player.HasBuff(ModContent.BuffType<GraniteCoreBuff>()))
            {
                player.AddBuff(ModContent.BuffType<GraniteCoreBuff>(), 12);
            }
        }
    }
}