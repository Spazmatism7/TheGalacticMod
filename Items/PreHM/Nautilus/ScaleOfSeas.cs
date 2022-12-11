﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using GalacticMod.NPCs.Bosses.PreHM;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Nautilus
{
    public class ScaleOfSeas : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shade Scale");
            Tooltip.SetDefault("The fury of the ocean vibrates through it");
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 12; // This helps sort inventory know this is a boss summoning item.
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 18;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item44;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(NPCType<JormungandrHead>());
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
                    NPC.SpawnOnPlayer(player.whoAmI, NPCType<JormungandrHead>());
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: NPCType<JormungandrHead>());
                }
            }

            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<ScaleOfSeas>());
            recipe.AddIngredient(ItemID.VilePowder, 15);
            recipe.AddIngredient(ItemID.Bass, 10);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();

            recipe = Recipe.Create(ItemType<ScaleOfSeas>());
            recipe.AddIngredient(ItemID.ViciousPowder, 15);
            recipe.AddIngredient(ItemID.Bass, 10);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
