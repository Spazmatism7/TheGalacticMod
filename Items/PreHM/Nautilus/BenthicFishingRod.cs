using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using System;
using System.IO;

namespace GalacticMod.Items.PreHM.Nautilus
{
    public class BenthicFishingRod : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The fishing line never snaps.");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            // These are copied through the CloneDefaults method:
            // Item.width = 24;
            // Item.height = 28;
            // Item.useStyle = ItemUseStyleID.Swing;
            // Item.useAnimation = 8;
            // Item.useTime = 8;
            // Item.UseSound = SoundID.Item1;
            Item.CloneDefaults(ItemID.WoodFishingPole);
            Item.rare = ItemRarityID.Orange;

            Item.fishingPole = 35; // Sets the poles fishing power
            Item.shootSpeed = 14f; // Sets the speed in which the bobbers are launched. Wooden Fishing Pole is 9f and Golden Fishing Rod is 17f.
            Item.shoot = ModContent.ProjectileType<BenthicBobber>();
        }

        // Grants the High Test Fishing Line bool if holding the item.
        // NOTE: Only triggers through the hotbar, not if you hold the item by hand outside of the inventory.
        public override void HoldItem(Player player)
        {
            player.accFishingLine = true;
        }
    }

    public class BenthicBobber : ModProjectile
    {
        public static readonly Color[] PossibleLineColors = new Color[] {
            new Color(15, 91, 93), //A teal color
			new Color(148, 147, 101) //A yellow-brown color
		};

        // This holds the index of the fishing line color in the PossibleLineColors array.
        private int fishingLineColorIndex;

        private Color FishingLineColor => PossibleLineColors[fishingLineColorIndex];

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Benthic Bobber");
        }

        public override void SetDefaults()
        {
            // These are copied through the CloneDefaults method
            // Projectile.width = 14;
            // Projectile.height = 14;
            // Projectile.aiStyle = 61;
            // Projectile.bobber = true;
            // Projectile.penetrate = -1;
            // Projectile.netImportant = true;
            Projectile.CloneDefaults(ProjectileID.BobberWooden);

            DrawOriginOffsetY = -8; // Adjusts the draw position
        }

        public override void OnSpawn(IEntitySource source)
        {
            // Decide color of the pole by getting the index of a random entry from the PossibleLineColors array.
            fishingLineColorIndex = (byte)Main.rand.Next(PossibleLineColors.Length);
        }

        // What if we want to randomize the line color
        public override void AI()
        {
            // Always ensure that graphics-related code doesn't run on dedicated servers via this check.
            if (!Main.dedServ)
            {
                // Create some light based on the color of the line.
                Lighting.AddLight(Projectile.Center, FishingLineColor.ToVector3());
            }
        }

        public override void ModifyFishingLine(ref Vector2 lineOriginOffset, ref Color lineColor)
        {
            // Change these two values in order to change the origin of where the line is being drawn.
            // This will make it draw 47 pixels right and 31 pixels up from the player's center, while they are looking right and in normal gravity.
            lineOriginOffset = new Vector2(47, -31);
            // Sets the fishing line's color. Note that this will be overridden by the colored string accessories.
            lineColor = FishingLineColor;
        }

        // These last two methods are required so the line color is properly synced in multiplayer.
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)fishingLineColorIndex);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            fishingLineColorIndex = reader.ReadByte();
        }
    }
}