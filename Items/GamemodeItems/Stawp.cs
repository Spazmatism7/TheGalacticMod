using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Systems;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.Chat;
using Terraria.Audio;

namespace GalacticMod.Items.GamemodeItems
{
	public class Reverser : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("World Artifact");
			Tooltip.SetDefault("Switches the difficulty of the world");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.LightRed;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = false;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < Main.maxNPCs; i++) //cant use while boss alive
            {
                if (Main.npc[i].active && Main.npc[i].boss)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool? UseItem(Player player)
        {
            string text = "";

            if (player.altFunctionUse == 2)
            {
                switch (Main.GameMode)
                {
                    case 0:
                        Main.GameMode = 3;
                        player.difficulty = 3;
                        text = "Journey mode is now enabled!";
                        break;

                    case 1:
                        Main.GameMode = 0;
                        player.difficulty = 0;
                        text = "Normal mode is now enabled!";
                        break;

                    case 2:
                        Main.GameMode = 1;
                        player.difficulty = 0;
                        text = "Expert mode is now enabled!";
                        break;

                    default:
                        Main.GameMode = 2;
                        player.difficulty = 0;
                        text = "Master mode is now enabled!";
                        break;
                }
            }
            else
            {
                switch (Main.GameMode)
                {
                    case 0:
                        Main.GameMode = 1;
                        player.difficulty = 0;
                        text = "Expert mode is now enabled!";
                        break;

                    case 1:
                        Main.GameMode = 2;
                        player.difficulty = 0;
                        text = "Master mode is now enabled!";
                        break;

                    case 2:
                        Main.GameMode = 3;
                        player.difficulty = 3;
                        text = "Journey mode is now enabled!";
                        break;

                    default:
                        Main.GameMode = 0;
                        player.difficulty = 0;
                        text = "Normal mode is now enabled!";
                        break;
                }
            }

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(text, new Color(175, 75, 255));
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), new Color(175, 75, 255));
                NetMessage.SendData(MessageID.WorldData); //sync world
            }

            SoundEngine.PlaySound(SoundID.Roar, player.Center);

            return true;
        }
    }
}