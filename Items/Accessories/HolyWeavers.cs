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

namespace GalacticMod.Items.Accessories
{
	[AutoloadEquip(EquipType.Shoes)]
	public class HolyWeavers : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Extreme Speed!!" +
                "\nGrants extra mobility on ice" +
				"\nProvides the ability to walk on water, honey & lava" +
				"\nGrants immunity to fire blocks and 7 seconds of immunity to lava" +
                "\nIncreases jump speed and allows auto-jump" +
				"\nIncreases fall resistance");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Pink;
			Item.width = 34;
			Item.height = 32;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void UpdateEquip(Player player)
		{
            //Amphibian Boots
            player.accRunSpeed = 6f;
            player.autoJump = true;
            player.jumpSpeedBoost += 1.6f;
            player.extraFall += 10;

			//Terraspark
            player.waterWalk = true;
			player.fireWalk = true;
			player.lavaMax += 420;
			player.accRunSpeed = 10f;
			player.moveSpeed += 0.3f;
			player.iceSkate = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<HolyWeavers>());
			recipe.AddIngredient(ItemID.TerrasparkBoots);
			recipe.AddIngredient(ItemID.AmphibianBoots);
			recipe.AddIngredient(ItemID.CrystalShard, 16);
			recipe.AddIngredient(ItemID.UnicornHorn, 7); //Unicorn Horns
			recipe.AddIngredient(ItemID.PixieDust, 16); //Pixie Dust
			recipe.AddIngredient(ItemID.SoulofLight, 9); //Souls of Light
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}