using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Items.Consumables;

namespace GalacticMod.Assets.Systems
{
	public class Recipes : ModSystem
	{
		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemID.EnchantedSword);
			recipe.AddIngredient(ItemID.FallenStar, 7); //Fallen Stars
			recipe.AddIngredient(ItemID.PlatinumShortsword);
			recipe.AddTile(TileID.Anvils); //Anvil
			recipe.Register();

			recipe = Recipe.Create(ItemID.EnchantedSword);
			recipe.AddIngredient(ItemID.FallenStar, 7); //Fallen Stars
			recipe.AddIngredient(ItemID.GoldShortsword);
			recipe.AddTile(TileID.Anvils); //Anvil
			recipe.Register();

            recipe = Recipe.Create(ItemID.LihzahrdPowerCell);
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddIngredient(ItemID.FallenStar, 25);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 10);
            recipe.AddTile(TileID.LihzahrdAltar);
            recipe.Register();

            recipe = Recipe.Create(ItemID.LihzahrdPowerCell);
            recipe.AddIngredient(ItemID.Glass, 10);
            recipe.AddIngredient(ItemID.FallenStar, 25);
            recipe.AddIngredient(ItemID.LihzahrdBrick, 10);
            recipe.AddTile(TileID.LihzahrdFurnace);
            recipe.Register();
        }

		public override void PostAddRecipes()
		{
			for (int i = 0; i < Recipe.numRecipes; i++)
			{
				Recipe recipe = Main.recipe[i];

				// All recipes that require wood will now need 100% more
				if (recipe.TryGetResult(ItemID.Zenith, out Item result))
				{
					recipe.AddIngredient(Mod, "SoulFragment");
				}
			}
		}

        public override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Cobalt Bar", new int[]
            {
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            });
            RecipeGroup.RegisterGroup("GalacticMod:CobaltBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Adamantite Bar", new int[]
            {
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            });
            RecipeGroup.RegisterGroup("GalacticMod:AdamantiteBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gold Bar", new int[]
            {
                ItemID.GoldBar,
                ItemID.PlatinumBar
            });
            RecipeGroup.RegisterGroup("GalacticMod:GoldBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Bar", new int[]
            {
                ItemID.SilverBar,
                ItemID.TungstenBar
            });
            RecipeGroup.RegisterGroup("GalacticMod:SilverBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Copper Bar", new int[]
            {
                ItemID.CopperBar,
                ItemID.TinBar
            });
            RecipeGroup.RegisterGroup("GalacticMod:CopperBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Wings", new int[]
            {
                493, //Angel Wings
				492, //Demon Wings
				1162, //Leaf Wings
				761, //Fairy Wings
				2494, //Fin Wings
				822, //Frozen Wings
				785, //Harpy Wings
				748, //Jetpack
				655, //Red's Wings
				1583, //D-Town's Wings
				1584, //Will's Wings
				1585, //Crowno's Wings
				1586, //Cenx's Wings
				3228, //Lazure's Barrier Platform
				3580, //Yoraiz0r's Spell
				3582, //Jim's Wings
				3588, //Skiphs' Paws
				3592, //Loki's Wings
				3924, //Arkhalis' Lightwings
				3928, //Leinfors' Prehensile Cloak
				1165, //Bat Wings
				1515, //Bee Wings
				749, //Butterfly Wings
				821, //Flame Wings
				1866, //Hoverboard
				786, //Bone Wings
				2770, //Mothron Wings
				823, //Spectre Wings
				2280, //Beetle Wings
				1871, //Festive Wings
				1830, //Spooky Wings
				1797, //Tattered Fairy Wings
				948, //Steampunk Wings
				3883, //Betsy's Wings
				2609, //Fishron Wings
				3470, //Nebula Mantle
				3469, //Vortex Booster
				3468, //Solar Wings
				3471 //Stardust Wings
			});
            RecipeGroup.RegisterGroup("GalacticMod:Wings", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " HM Anvil", new int[]
            {
                525,
                1220
            });
            RecipeGroup.RegisterGroup("GalacticMod:HM Anvil", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " HM Forge", new int[]
            {
                524,
                1221
            });
            RecipeGroup.RegisterGroup("GalacticMod:HM Forge", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Bookcase", new int[]
            {
                354, //Bookcase
				
				4189, //Nebula Bookcase
				4147, //Solar Bookcase
				4210, //Stardust Bookcase
				4168, //Vortex Bookcase 
				3960, //Lesion Bookcase
				4568, //Bamboo Bookcase 
				2025, //Glass Bookcase
				2138, //Bone Bookcase  
				2030, //Lihzhard Bookcase
				2536, //Palm Bookcase
				2028, //Spooky Bookcase
				2029, //Skyware Bookcase
				2024, //Steampunk Bookcase
				4300, //Sandstone
				2233, //Dynasty Bookcase    //1.4 only
				2022, //Flesh Bookcase
				2023, //Honey Bookcase
				2031, //Frozen Bookcase
				2135, //Living Wood Bookcase
				2554, //Boreal Wood Bookcase
				2020, //Cactus Bookshelf
				3917, //Crystal Bookcase
				2021, //Ebonwood Bookcase
				3167, //Granite Bookcase
				3166, //Marble Bookcase
				2817, //Martian Bookcase
				3165, //Meteorite Bookcase
				2540, //Mushroom Bookcase
				2027, //Pearlwood Bookcase
				2670, //Pumpkin Bookcase
				2026, //Rich Mahogany
				2136, //Shadewood Bookcase
				2569, //Slime Bookcase

				1512, //Gothic Bookcase
				1414, //Blue Dungeon
				1415, //Green Dungeon
				1416, //Pink Dungeon
				1463, //Obsidian Bookcase
				2137, //Golden Bookcase //1.4
			});
            RecipeGroup.RegisterGroup("GalacticMod:Bookcase", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Staff", new int[]
            {
                ItemID.Vilethorn,
                ItemID.CrimsonRod
            });
            RecipeGroup.RegisterGroup("GalacticMod:EvilStaff", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Bar", new int[]
            {
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            });
            RecipeGroup.RegisterGroup("GalacticMod:EvilBar", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Bow", new int[]
            {
                ItemID.DemonBow,
                ItemID.TendonBow
            });
            RecipeGroup.RegisterGroup("GalacticMod:EvilBow", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Knife", new int[]
            {
                ItemType<Items.ThrowingClass.Weapons.Knives.DemonicDagger>(),
                ItemType<Items.ThrowingClass.Weapons.Knives.BloodyKnife>()
            });
            RecipeGroup.RegisterGroup("GalacticMod:EvilKnife", group);

            if (RecipeGroup.recipeGroupIDs.ContainsKey("Fruit"))
            {
                int index = RecipeGroup.recipeGroupIDs["Fruit"];
                group = RecipeGroup.recipeGroups[index];
                group.ValidItems.Add(ItemType<Strawberry>());
            }

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Cobalt Sword", new int[]
            {
                ItemID.CobaltSword,
                ItemID.PalladiumSword
            });
            RecipeGroup.RegisterGroup("GalacticMod:CobaltSword", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Mythril Sword", new int[]
            {
                ItemID.MythrilSword,
                ItemID.OrichalcumSword
            });
            RecipeGroup.RegisterGroup("GalacticMod:MythrilSword", group);

            group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Adamantite Sword", new int[]
            {
                ItemID.AdamantiteSword,
                ItemID.TitaniumSword
            });
            RecipeGroup.RegisterGroup("GalacticMod:AdamantiteSword", group);
        }
    }
}


