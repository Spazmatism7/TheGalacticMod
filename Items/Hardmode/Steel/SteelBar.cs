using System.IO.Pipes;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;
using GalacticMod.Items.PreHM.Carbon;
using Terraria.ID;
using GalacticMod.Tiles.Bars;

namespace GalacticMod.Items.Hardmode.Steel
{
	public class SteelBar : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Hard as Steel");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.maxStack = 99;
			Item.rare = ItemRarityID.Pink;
			Item.width = 20;
			Item.height = 20;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.value = 4000 * 5;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.createTile = TileType<SteelBarPlaced>();
            Item.consumable = true;
            Item.autoReuse = true;
        }
	}

    class SteelNPC : GlobalNPC
    {
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Steampunker && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                shop.item[nextSlot].SetDefaults(ItemType<SteelBar>());
                nextSlot++;
            }
        }
    }

}