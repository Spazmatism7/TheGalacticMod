using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PostML.Hellfire
{
	public class HellfireShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants ability to dash" +
				"\nImmunity to Burning, On Fire and Knockback" +
				"\nCreates peace with the demons of hell" +
				"\nMelee attacks inflict fire damage" +
                "\nEnables auto swing for melee weapons");
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
			player.noKnockback = true;
			player.fireWalk = true;
			player.buffImmune[24] = true; //On Fire!
			player.npcTypeNoAggro[24] = true; //Fire Imp
			player.npcTypeNoAggro[62] = true; //Demon
			player.npcTypeNoAggro[66] = true; //Voodoo Demon
			player.npcTypeNoAggro[156] = true; //Red Devil
			player.dashType = 3;

			//Fire Gauntlet
			player.autoReuseGlove = true;
			player.magmaStone = true;
		}
	}
}