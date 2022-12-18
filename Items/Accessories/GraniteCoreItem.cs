using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using System.Collections.Generic;
using GalacticMod.Buffs;

namespace GalacticMod.Items.Accessories
{
    public class GraniteCoreItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Granite Core");
            
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var key = GalacticMod.GraniteCoreHotkey.GetAssignedKeys();
            string keyName = "(Not bound, set in controls)";

            if (key.Count > 0)
            {
                keyName = key[0];
            }

            foreach (TooltipLine line in tooltips)
            {
                if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                {
                    line.Text = "Press and hold '" + keyName + "' to place yourself in a form of stasis";
                }
            }
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
            if (GalacticMod.GraniteCoreHotkey.Current)
            {
                player.AddBuff(ModContent.BuffType<GraniteCoreBuff>(), 12);
            }
        }
    }
}