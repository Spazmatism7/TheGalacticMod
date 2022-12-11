using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Systems;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace GalacticMod.Items.GamemodeItems
{
	public class NoHitMode : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Chalice of Death");
			Tooltip.SetDefault("Activates No Hit Mode" +
                "\nAll damage is multiplied by 9000" +
                "\nDoes not bypass Journey Godmode" +
                "\nCannot be reversed");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 54;
			Item.rare = ItemRarityID.Purple;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
		}

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

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<NoHitMode>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }

		public override bool? UseItem(Player player)
		{
            if (GetInstance<Gamemodes>().noHitMode)
            {
                GetInstance<Gamemodes>().noHitMode = false; 
                string key = "No Hit Mode is off";
                Color messageColor = Color.Purple;
                if (Main.netMode == NetmodeID.Server) // Server
                {
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                }
                else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
                {
                    Main.NewText(Language.GetTextValue(key), messageColor);
                }
                GetInstance<Gamemodes>().noHitMessage = false;
            }
            else
            {
                return GetInstance<Gamemodes>().noHitMode = true;
            }

            return true;
		}
	}
}