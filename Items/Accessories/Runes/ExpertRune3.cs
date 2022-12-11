/*using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.ID;

namespace GalacticMod.Items.Accessories.Runes
{
	public class ExpertRune3 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ultimate Rune of the Powerhungry");

			Tooltip.SetDefault("Slimes are Friendly" +
				"\nGrants ability to dash" +
				"\nIncreases strength of friendly bees" +
				"\n Grants abilities of Spore Sac and Shiny Stone");
		}

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Pink;
			Item.width = 80;
			Item.height = 80;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.npcTypeNoAggro[1] = true;
			player.npcTypeNoAggro[16] = true;
			player.npcTypeNoAggro[59] = true;
			player.npcTypeNoAggro[71] = true;
			player.npcTypeNoAggro[81] = true;
			player.npcTypeNoAggro[138] = true;
			player.npcTypeNoAggro[121] = true;
			player.npcTypeNoAggro[122] = true;
			player.npcTypeNoAggro[141] = true;
			player.npcTypeNoAggro[147] = true;
			player.npcTypeNoAggro[183] = true;
			player.npcTypeNoAggro[184] = true;
			player.npcTypeNoAggro[204] = true;
			player.npcTypeNoAggro[225] = true;
			player.npcTypeNoAggro[244] = true;
			player.npcTypeNoAggro[302] = true;
			player.npcTypeNoAggro[333] = true;
			player.npcTypeNoAggro[335] = true;
			player.npcTypeNoAggro[334] = true;
			player.npcTypeNoAggro[336] = true;
			player.npcTypeNoAggro[537] = true;

			player.dash = 2;

			player.strongBees = true;

			player.sporeSac = true;

			player.shinyStone = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<ExpertRune3>());
			recipe.AddIngredient(Mod, "ExpertRune1");
			recipe.AddIngredient(Mod, "ExpertRune2");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}*/