using GalacticMod.Items.Hardmode.Ancient;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.CraftingStations
{
	public class AncientAltar : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolidTop[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Ancient Altar");
			AddMapEntry(new Color(200, 200, 200), name);
			AdjTiles = new int[] { TileID.WorkBenches, 26, 412, 134, 133, 247, 217 }; //Workbenches, Demon Altar, Ancient Manipulator, HM Anvil, HM Forge, Autohammer, Blend-O-Matic (Asphalt)
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			if (frameX == 0)
			{
				Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 48, ItemType<AncientAltarItem>());
			}
		}
	}

	public class AncientAltarItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Altar");
			Tooltip.SetDefault("Counts as Workbenches, Demon Altar, Ancient Manipulator, HM Anvil, HM Forge, Autohammer and Blend-O-Matic");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 14;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 150;
			Item.rare = ItemRarityID.Red;
			Item.createTile = ModContent.TileType<AncientAltar>();
		}

		public override void AddRecipes()
		{
			
			Recipe recipe = Recipe.Create(ItemType<AncientAltarItem>());
			recipe.AddIngredient(ItemID.WorkBench);
			recipe.AddIngredient(ItemID.LunarCraftingStation);
			recipe.AddRecipeGroup("GalacticMod:HM Anvil");
			recipe.AddRecipeGroup("GalacticMod:HM Forge");
			recipe.AddIngredient(ItemID.Autohammer); //Autohammer
			recipe.AddIngredient(ItemID.BlendOMatic); //Blend-O-Matic (Asphalt)
			recipe.AddIngredient(ModContent.ItemType<AncientBar>(), 10);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}