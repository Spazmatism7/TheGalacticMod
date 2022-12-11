using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Frost
{
	public class FrostOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Brrrr");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
		}

		public override void SetDefaults()
		{
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 12;
			Item.height = 12;
			Item.value = 3000;
			Item.rare = ItemRarityID.Pink;
		}
	}

	public class FrostOreGlobalNPC : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (Main.hardMode)
			{
				if (Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].ZoneSnow)
				{
					if (Main.rand.NextBool(7))
					{
						npcLoot.Add(ItemDropRule.Common(ItemType<FrostOre>(), 1, 2));
					}
				}
			}
		}
	}
}