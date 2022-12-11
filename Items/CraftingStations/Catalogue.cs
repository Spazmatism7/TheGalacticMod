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
	public class Catalogue : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolidTop[Type] = false;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 3;
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Catalogue");
			AddMapEntry(new Color(200, 200, 200), name);
			AdjTiles = new int[] { 106, 283, 220, 303, 307, 14, 469, 487, 15, TileID.WorkBenches, 86, 302, 305, 300, 308, 304, 306 };
			//Sawmill, Heavy Work Bench, Solidifier, Lihzhard Furnace, Steampunk Boiler, Table, Table, Table, Chair, Workbench, Loom, Glass Kiln, Sky Mill, Bone Welder, Honey Dispenser, Living Loom, Ice Machine, Water, Honey, Lava
			//218, Meat Grinder, 499, Decay Chamber, 301, Flesh Cloning Vat
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			if (frameX == 0)
			{
				Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 48, ItemType<CatalogueItem>());
			}
		}
	}

	public class CatalogueItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Catalogue");
			Tooltip.SetDefault("Allows Creation of almost any furniture item");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 9));
		}

		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 48;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.rare = ItemRarityID.Red;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 150;
			Item.createTile = TileType<Catalogue>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<CatalogueItem>());
			recipe.AddIngredient(ItemID.Sawmill); //Sawmill
			recipe.AddIngredient(ItemID.HeavyWorkBench); //Heavy Work Bench
			recipe.AddIngredient(ItemID.Solidifier); //Solidifier
			recipe.AddIngredient(ItemID.LihzahrdFurnace); //Lihzhard Furnace
			recipe.AddIngredient(ItemID.SteampunkBoiler); //Steampunk Boiler
			recipe.AddIngredient(ItemID.WorkBench);
			recipe.AddIngredient(ItemID.Loom); //Loom
			recipe.AddIngredient(ItemID.GlassKiln); //Glass Kiln
			recipe.AddIngredient(ItemID.SkyMill); //Sky Mill
			recipe.AddIngredient(ItemID.BoneWelder); //Bone Welder
			recipe.AddIngredient(ItemID.HoneyDispenser); //Honey Dispenser
			recipe.AddIngredient(ItemID.LivingLoom); //Living Loom
			recipe.AddIngredient(ItemID.IceMachine); //Ice Machine
			recipe.AddIngredient(ItemID.MeatGrinder);
			//recipe.AddRecipeGroup("GalacticMod:Evil Furniture Station"); 2193, 4142, Flesh Cloning Vat, Decay Chamber
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}