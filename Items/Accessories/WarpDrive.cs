using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using GalacticMod.Assets.Rarities;
using GalacticMod.Items.PostML.Galactic;

namespace GalacticMod.Items.Accessories
{
	public class WarpDrive : ModItem
	{
		float n = 1f;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Allows you to travel at warp speed" +
                "\nAllows you to walk on water" +
                "\nGrants immunity to fire blocks and 7 seconds in lava");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.rare = RarityType<GalacticRarity>();
			Item.width = 20;
			Item.height = 20;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
			n += 0.05f;
			player.waterWalk = true;
			player.fireWalk = true;
			player.lavaMax += 420;
			player.moveSpeed += n;
			player.iceSkate = true;
		}
	}

    public class WarpAttackDrive : ModItem
    {
        float n = 1f;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows you to attack at warp speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = RarityType<GalacticRarity>();
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }

        public override void UpdateEquip(Player player)
        {
            n += 0.1f;
            player.GetAttackSpeed(DamageClass.Generic) += n;
        }
    }
}