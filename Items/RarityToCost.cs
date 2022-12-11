using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GalacticMod.Assets.Systems;
using GalacticMod.Assets.Config;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Assets.Rarities;
using GalacticMod.Items.ThrowingClass.Weapons.Explosives;

namespace GalacitcMod.Items
{
    public class RarityToCostWeapons : GlobalItem
    {
        public override bool InstancePerEntity => true;

        int gray = Item.sellPrice(copper: 50);
        int white = Item.sellPrice(silver: 10);
        int blue = Item.sellPrice(silver: 20);
        int green = Item.sellPrice(silver: 40);
        int orange = Item.sellPrice(silver: 80);
        int lightRed = Item.sellPrice(gold: 2 ,silver: 40);
        int pink = Item.sellPrice(gold: 4, silver: 80);
        int lightPurple = Item.sellPrice(gold: 7, silver: 20);
        int lime = Item.sellPrice(gold: 9, silver: 60);
        int yellow = Item.sellPrice(gold: 12);
        int cyan = Item.sellPrice(gold: 16);
        int red = Item.sellPrice(gold: 20);
        int purple = Item.sellPrice(gold: 22);

        bool weapon;
        bool modItem;

        public override void SetDefaults(Item Item)
        {
            if (Item.DamageType == DamageClass.Melee || Item.DamageType == DamageClass.Ranged || Item.DamageType == DamageClass.Magic || Item.DamageType == DamageClass.Summon || 
                Item.DamageType == DamageClass.Throwing || Item.DamageType == DamageClass.MeleeNoSpeed || Item.DamageType == DamageClass.SummonMeleeSpeed || 
                Item.DamageType == DamageClass.SummonMeleeSpeed || Item.DamageType == DamageClass.MagicSummonHybrid || Item.DamageType == DamageClass.Generic)
                weapon = true;
            if (!(Item.type > ItemID.None && Item.type < 5125) && Item.type != ItemType<StunBomb>())
                modItem = true;

            if (weapon && modItem)
            {
                switch (Item.rare)
                {
                    case ItemRarityID.Gray:
                        Item.value = gray;
                        break;
                    case ItemRarityID.White:
                        Item.value = white;
                        break;
                    case ItemRarityID.Blue:
                        Item.value = blue;
                        break;
                    case ItemRarityID.Green:
                        Item.value = green;
                        break;
                    case ItemRarityID.Orange:
                        Item.value = orange;
                        break;
                    case ItemRarityID.LightRed:
                        Item.value = lightRed;
                        break;
                    case ItemRarityID.Pink:
                        Item.value = pink;
                        break;
                    case ItemRarityID.LightPurple:
                        Item.value = lightPurple;
                        break;
                    case ItemRarityID.Lime:
                        Item.value = lime;
                        break;
                    case ItemRarityID.Yellow:
                        Item.value = yellow;
                        break;
                    case ItemRarityID.Cyan:
                        Item.value = cyan;
                        break;
                    case ItemRarityID.Red:
                        Item.value = red;
                        break;
                    case ItemRarityID.Purple:
                        Item.value = purple;
                        break;
                }
            }
        }
    }

    public class RarityToCostArmor : GlobalItem
    {
        public override bool InstancePerEntity => true;

        int gray = Item.sellPrice(copper: 75);
        int white = Item.sellPrice(silver: 15);
        int blue = Item.sellPrice(silver: 30);
        int green = Item.sellPrice(silver: 60);
        int orange = Item.sellPrice(gold: 1, silver: 20);
        int lightRed = Item.sellPrice(gold: 2, silver: 40);
        int pink = Item.sellPrice(gold: 4, silver: 80);
        int lightPurple = Item.sellPrice(gold: 7, silver: 20);
        int lime = Item.sellPrice(gold: 9, silver: 60);
        int yellow = Item.sellPrice(gold: 12);
        int cyan = Item.sellPrice(gold: 16);
        int red = Item.sellPrice(gold: 20);
        int purple = Item.sellPrice(gold: 22);

        public bool modArmor;
        bool modItem;

        public override void SetDefaults(Item Item)
        {
            if (!(Item.type > ItemID.None && Item.type < 5125))
                modItem = true;

            if (modArmor && modItem)
            {
                switch (Item.rare)
                {
                    case ItemRarityID.Gray:
                        Item.value = gray;
                        break;
                    case ItemRarityID.White:
                        Item.value = white;
                        break;
                    case ItemRarityID.Blue:
                        Item.value = blue;
                        break;
                    case ItemRarityID.Green:
                        Item.value = green;
                        break;
                    case ItemRarityID.Orange:
                        Item.value = orange;
                        break;
                    case ItemRarityID.LightRed:
                        Item.value = lightRed;
                        break;
                    case ItemRarityID.Pink:
                        Item.value = pink;
                        break;
                    case ItemRarityID.LightPurple:
                        Item.value = lightPurple;
                        break;
                    case ItemRarityID.Lime:
                        Item.value = lime;
                        break;
                    case ItemRarityID.Yellow:
                        Item.value = yellow;
                        break;
                    case ItemRarityID.Cyan:
                        Item.value = cyan;
                        break;
                    case ItemRarityID.Red:
                        Item.value = red;
                        break;
                    case ItemRarityID.Purple:
                        Item.value = purple;
                        break;
                }
            }
        }
    }
}
