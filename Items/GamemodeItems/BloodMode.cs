using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Systems;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Items.PostML.ImmediateOres;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace GalacticMod.Items.GamemodeItems
{
	public class BloodMode : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ritualistic Dagger of Blood");
			Tooltip.SetDefault("Activates Blood Mode" +
				"\nYou don't regen health" +
                "\nOther forms of Healing still work" +
                "\nHaving the bleeding debuff deals damage" +
                "\nDoubles max life" +
				"\nCannot be reversed");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.Master;
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

        public override bool? UseItem(Player player)
        {
            if (GetInstance<Gamemodes>().BloodMode)
            {
                GetInstance<Gamemodes>().BloodMode = false;
                string key = "The Blood Gods have been quenched";
                Color messageColor = Color.DarkRed;
                if (Main.netMode == NetmodeID.Server) // Server
                {
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                }
                else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
                {
                    Main.NewText(Language.GetTextValue(key), messageColor);
                }
                GetInstance<Gamemodes>().bloodMessage = false;
            }
            else
			{
                player.statLife = player.statLifeMax2;
                return GetInstance<Gamemodes>().BloodMode = true;
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<BloodMode>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}