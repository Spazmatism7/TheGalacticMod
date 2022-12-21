using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using GalacticMod.Items.PostML.Celestial;
using GalacticMod.Items.CraftingStations;
using GalacticMod.Assets.Config;

namespace GalacticMod.Items.Accessories.Runes
{
	[AutoloadEquip(EquipType.Wings)]
	public class UniversalInsignia : ModItem
	{
		public override string Texture => GetInstance<PersonalConfig>().NoEpilepsy ? base.Texture + "_NoEpilepsy" : base.Texture;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Insane Speed!!" +
				"\nAllows flight and slow fall" +
				"\nAllows the ability to climb walls and dash" +
				"\nGives a chance to dodge attacks" +
				"\nGrants extra mobility on ice" +
				"\nProvides the ability to walk on water, honey & lava" +
				"\nGrants immunity to fire blocks and 7 seconds of immunity to lava" +
				"\nIncreases jump speed, jump height, and Fall Resistance" +
				"\nGrants the ability to swim and greatly extends underwater breathing" +
				"\nGenerates a very subtle glow which becomes more vibrant underwater" +
                "\nAllows user to sextuple jump");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 2));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(600, 8.5f, 2.2f);
		}

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Purple;
			Item.width = 28;
			Item.height = 44;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
        }

		public override void UpdateEquip(Player player)
		{
			//Frog Legs
			player.jumpSpeedBoost += 2.4f;
			player.extraFall += 15;

			//Hellspark Boots
			player.waterWalk = true;
			player.fireWalk = true;
			player.lavaMax += 420;
			player.accRunSpeed = 12f;
			player.moveSpeed += 2f;
			player.iceSkate = true;
			player.rocketBoots = 3;

			//Master Ninja Gear
			player.blackBelt = true;
			player.dashType = 3;
			player.spikedBoots = 2;

			//Arctic Diving Gear
			player.arcticDivingGear = true;
			player.accFlipper = true;
			player.accDivingHelm = true;
			if (player.wet)
			{
				Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.2f, 0.8f, 0.9f);
			}

			//Bundle of Balloons
			player.hasJumpOption_Cloud = true;
			player.hasJumpOption_Sandstorm = true;
			player.hasJumpOption_Blizzard = true;
			player.jumpBoost = true;

			//Fart in a Balloon & Sharkron Balloon
			player.hasJumpOption_Fart = true;
			player.hasJumpOption_Sail = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.controlUp && player.controlJump && player.wingTime > 0)
			{
				if (Main.rand.NextBool(10))
				{
					var dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.ShadowbeamStaff);
					dust.scale = 2f;
				}
			}
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 2f;
			ascentWhenRising = 0.3f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 2.2f;
			constantAscend = 0.15f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<UniversalInsignia>());
			recipe.AddIngredient(Mod, "ConstellationWeavers");
			recipe.AddIngredient(Mod, "SoulFragment");
			recipe.AddIngredient(ItemID.ArcticDivingGear);
			recipe.AddIngredient(ItemID.BundleofBalloons);
			recipe.AddIngredient(ItemID.FartInABalloon);
			recipe.AddIngredient(ItemID.SharkronBalloon);
			recipe.AddIngredient(ItemID.MasterNinjaGear);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
		}
	}
}