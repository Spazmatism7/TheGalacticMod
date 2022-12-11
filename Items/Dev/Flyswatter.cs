using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using System;

namespace GalacticMod.Items.Dev
{
    public class Flyswatter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Flyswatter");
            Tooltip.SetDefault("Smoosh");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Melee;
            Item.width = 50;
            Item.height = 50;
            Item.knockBack = 99;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Expert;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 20;
            Item.useTime = 20;
        }
    }
}