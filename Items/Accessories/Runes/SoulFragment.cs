using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace GalacticMod.Items.Accessories.Runes
{
	public class SoulFragment : ModItem
	{
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The Essence of Gods");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4)); 
			ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation

            ItemID.Sets.ItemIconPulse[Item.type] = true; // The item pulses while in the player's inventory
            ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity
        }

        public override void SetDefaults()
		{
			Item.maxStack = 7;
			Item.rare = ItemRarityID.Purple;
			Item.width = 40;
			Item.height = 40;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.autoReuse = true;
		}

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Blue.ToVector3() * 0.55f * Main.essScale); // Makes this item glow when thrown out of inventory.
        }
    }
}