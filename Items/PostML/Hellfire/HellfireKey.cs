using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using GalacticMod.NPCs.Bosses.PostML;
using GalacticMod.Assets.Rarities;
using System;
using SubworldLibrary;
using GalacticMod.Biomes.CharredKingdom;

namespace GalacticMod.Items.PostML.Hellfire
{
    public class HellfireKey : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A Mysterious Artifact that vibrates with power");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = RarityType<HellfireRarity>();
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item123;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < 50; i++)
                {
                    int dust2 = Dust.NewDust(new Vector2(player.Center.X - 5, player.Top.Y), 10, 10, DustID.Torch, 0f, 0f, 200, default, 0.8f);
                    Main.dust[dust2].velocity *= 2f;
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].scale = 1.5f;
                }

                SubworldSystem.Enter<CharredKingdom>();

                SoundEngine.PlaySound(SoundID.Item123, player.position);
            }

            return true;
        }
    }
}