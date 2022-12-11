using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using GalacticMod.NPCs.Bosses.PostML;
using GalacticMod.Assets.Rarities;

namespace GalacticMod.Items.PostML.Galactic
{
	public class GalacticSigil : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Pulsates with the power of the stars" +
                "\nNot Consumable");
			ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 12; // This helps sort inventory know this is a boss summoning item.
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ModContent.RarityType<GalacticRarity>();
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(NPCType<GalacticPeril>()) && (player.ZoneSkyHeight || player.ZoneOverworldHeight);
		}

		public override bool? UseItem(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				for (int i = 0; i < 50; i++)
				{
					int dust2 = Dust.NewDust(new Vector2(player.Center.X - 5, player.Top.Y), 10, 10, DustID.Vortex, 0f, 0f, 200, default, 0.8f);
					Main.dust[dust2].velocity *= 2f;
					Main.dust[dust2].noGravity = true;
					Main.dust[dust2].scale = 1.5f;
				}
				SoundEngine.PlaySound(SoundID.Roar, player.position);

				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.SpawnOnPlayer(player.whoAmI, NPCType<TheStorm>());
					NPC.SpawnOnPlayer(player.whoAmI, NPCType<GalacticPeril>());
				}
				else
				{
					NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: NPCType<GalacticPeril>());
				}
			}

			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<GalacticSigil>());
			recipe.AddIngredient(Mod, "SigilCore");
			recipe.AddIngredient(ItemID.LunarBar, 36);
			recipe.AddIngredient(Mod, "GalaxyFragment", 40);
			recipe.AddIngredient(Mod, "VanadiumBar", 30);
			recipe.AddIngredient(Mod, "LutetiumBar", 30);
			recipe.AddIngredient(Mod, "Zirconium", 30);
			recipe.AddIngredient(Mod, "OsmiumBar", 30);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
}