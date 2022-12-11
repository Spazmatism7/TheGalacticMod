using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Desert
{
	public class SandyRing : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Sandstorms no longer affect you" +
				"\nMost desert enemies will no longer attack you");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 28;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.accessory = true;
			Item.expert = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			player.buffImmune[194] = true; //Mighty Wind
			player.npcTypeNoAggro[546] = true; //Angry Tumbler
			player.npcTypeNoAggro[61] = true; //Vulture
			player.npcTypeNoAggro[69] = true; //Antlion
			player.npcTypeNoAggro[537] = true; //Sand Slime
			player.npcTypeNoAggro[582] = true; //Antlion Larva
			player.npcTypeNoAggro[508] = true; //Small Antlion
			player.npcTypeNoAggro[580] = true; //Giant Antlion
			player.npcTypeNoAggro[509] = true; //Antlion Swarmer
			player.npcTypeNoAggro[581] = true; //Giant Antlion Swarmer
		}
	}
}