using GalacticMod.Items.PostML.Celestial;
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
	public class CelestialGlobe : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Celestial Globe");
			AddMapEntry(new Color(200, 200, 200), name);
			AdjTiles = new int[] { 125, 101, 355, 243, 228, 94 }; //Crystal Ball, Bookcase, Alchemy Table, Imbuning Station, Dye Vat, Keg
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			if (frameX == 0)
			{
				Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 48, ItemType<CelestialGlobeItem>());
			}
		}
	}

	public class CelestialGlobeItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Globe");
			Tooltip.SetDefault("Counts as Crystal Ball, Bookcase, Alchemy Table, Water, Imbuning Station, Dye Vat and Keg");
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
			Item.createTile = ModContent.TileType<CelestialGlobe>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<CelestialGlobeItem>());
			recipe.AddIngredient(ItemID.AlchemyTable);
			recipe.AddRecipeGroup("GalacticMod:Bookcase");
			recipe.AddIngredient(ItemID.CrystalBall); //Crystal Ball
			recipe.AddIngredient(ItemID.ImbuingStation); //Imbuning Station
			recipe.AddIngredient(ItemID.DyeVat); //Dye Vat
			recipe.AddIngredient(ItemID.Keg); //Keg
			recipe.AddIngredient(ItemType<GalaxyFragment>(), 7);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}