using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;

namespace GalacticMod.Items.PreHM.Star
{
	public class Stardust : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("The essence of the Stars");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 8));
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 25;
			Item.maxStack = 999;
			Item.rare = ItemRarityID.Green;
		}
	}
}
