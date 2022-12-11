using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Desert
{
	public class VultureFeather : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
		}

		public override void SetDefaults()
		{
			Item.width = 28; // hitbox width of the item
			Item.height = 70; // hitbox height of the item
			Item.value = 10000; // how much the item sells for (measured in copper)
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = 999;
		}
	}

	class Vulture : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.type == NPCID.Vulture)
			{
				npcLoot.Add(ItemDropRule.Common(ItemType<VultureFeather>()));
			}
		}
	}
}
