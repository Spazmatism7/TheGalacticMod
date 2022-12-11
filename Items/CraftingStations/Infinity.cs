using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using GalacticMod.Items.Accessories.Runes;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Items.CraftingStations
{
	public class Infinity : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("The Infinity");
			AddMapEntry(new Color(200, 200, 200), name);
			AdjTiles = new int[] { 114, 96, ModContent.TileType<AncientAltar>(), ModContent.TileType<CelestialGlobe>(), ModContent.TileType<Catalogue>() }; //Tinkerer's Workshop, Cooking Pot, Ancient Altar, Lava Crucible, Celestial Globe, Catalogue
			//Ancient Altar: Workbenches, Demon Altar, Ancient Manipulator, HM Anvil, HM Forge, Autohammer, Blend-O-Matic (Asphalt)
			//Celestial Globe: Crystal Ball, Bookcase, Alchemy Table, Water, Imbuning Station, Dye Vat, Keg
			//Catalogue: Sawmill, Heavy Work Bench, Solidifier, Lihzhard Furnace, Steampunk Boiler, Table, Table, Table, Chair, Workbench, Loom, Glass Kiln, Sky Mill, Bone Welder, Honey Dispenser, Living Loom, Ice Machine, Water, Honey, Lava
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			if (frameX == 0)
			{
				Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 48, ItemType<InfinityItem>());
			}
		}
	}

	public class InfinityItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Infinity");
			Tooltip.SetDefault("Crafts every item");
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
			Item.createTile = TileType<Infinity>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<InfinityItem>());
			recipe.AddIngredient(ItemType<AncientAltarItem>());
			recipe.AddIngredient(ItemType<CelestialGlobeItem>());
			recipe.AddIngredient(ItemType<CatalogueItem>());
			recipe.AddIngredient(ItemType<SoulFragment>());
			recipe.AddIngredient(ItemID.TinkerersWorkshop);
			recipe.AddIngredient(ItemID.CookingPot);
			recipe.Register();
		}
	}
}