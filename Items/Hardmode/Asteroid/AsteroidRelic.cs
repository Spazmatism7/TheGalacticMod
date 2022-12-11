using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.NPCs.Bosses.Hardmode;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Asteroid
{
    public class AsteroidRelic : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asteroid Relic");
            Tooltip.SetDefault("Emits and eerie sound");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 12; // This helps sort inventory know this is a boss summoning item.
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 18;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.LightRed;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item44;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(NPCType<AsteroidBehemoth>());
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
                    NPC.SpawnOnPlayer(player.whoAmI, NPCType<AsteroidBehemoth>());
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: NPCType<AsteroidBehemoth>());
                }
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<AsteroidRelic>());
            recipe.AddIngredient(Mod, "AsteroidBar", 10);
            recipe.AddIngredient(Mod, "Stardust", 12);
            recipe.AddIngredient(Mod, "BarOfLife");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
