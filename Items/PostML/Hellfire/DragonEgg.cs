using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using GalacticMod.NPCs.Bosses.PostML;
using GalacticMod.Assets.Rarities;

namespace GalacticMod.Items.PostML.Hellfire
{
	public class DragonEgg : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("The Egg of the Dragon of the Underworld");
			ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 20;
			Item.rare = ModContent.RarityType<HellfireRarity>();
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return player.ZoneUnderworldHeight && !NPC.AnyNPCs(NPCType<HellfireDragon>());
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
					NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<HellfireDragon>());
				}
				else
				{
					NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: ModContent.NPCType<HellfireDragon>());
				}
			}

			return true;
		}
	}
}