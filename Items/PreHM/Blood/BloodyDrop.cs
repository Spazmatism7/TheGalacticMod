using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Blood
{
	public class BloodyDrop : ModItem
	{
		public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("A droplet of the blood of the Moon");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}
		public override void SetDefaults()
        {
			Item.width = 7;
			Item.height = 10;
			Item.maxStack = 999;
			Item.rare = ItemRarityID.Blue;
		}
	}
}
