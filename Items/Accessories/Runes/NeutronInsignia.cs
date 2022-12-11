using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using GalacticMod.Items.Accessories;
using GalacticMod.Items.CraftingStations;
using Microsoft.Xna.Framework.Input;
using GalacticMod.Assets.Config;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticMod.Items.Accessories.Runes
{
	[AutoloadEquip(EquipType.Shield)]
	public class NeutronInsignia : ModItem
	{
		public override string Texture => GetInstance<PersonalConfig>().NoEpilepsy ? base.Texture + "_NoEpilepsy" : base.Texture;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants immunity to knockback and fire blocks" +
                "\nGrants immunity to most debuffs" +
                "\nGrants 14 defense" +
				"\nCauses stars to fall and increases length of invincibility after taking damage" +
				"\nPuts a shell around the owner when below 50% life that reduces damage by 25%" +
				"\nReduces the cooldown of healing potions by 25%" +
				"\nReleases bees and increases movement speed when damaged" +
				"\nEnemies are more likely to target you");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 2));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation
        }

		public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Purple;
			Item.width = 42;
			Item.height = 44;
			Item.accessory = true;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
        }

		public override void UpdateEquip(Player player)
		{
			//Ankh Shield
			player.buffImmune[46] = true;
			player.noKnockback = true;
			player.fireWalk = true;
			player.buffImmune[33] = true;
			player.buffImmune[36] = true;
			player.buffImmune[30] = true;
			player.buffImmune[20] = true;
			player.buffImmune[32] = true;
			player.buffImmune[31] = true;
			player.buffImmune[35] = true;
			player.buffImmune[23] = true;
			player.buffImmune[22] = true;

			//Frozen Turtle Shell
			player.AddBuff(62, 5);

			//Pocket Mirror & Hand Warmer & Philosopher's Stone
			player.buffImmune[156] = true;
			player.buffImmune[46] = true;
			player.buffImmune[47] = true;
			player.pStone = true;

			//Flesh Knuckles
			player.aggro += 400;

			//Wooden Shield & Shackle & Ankh Shield/Flesh Knuckles Defense
			player.statDefense += 14;
		}

		private void ApplyEquipFunctional(int itemSlot, Item currentItem, Player player)
        {
			//Star Veil
			player.starCloakItem = currentItem;
			player.longInvince = true;
			player.starCloakItem_starVeilOverrideItem = currentItem;

			//Sweetheart Necklace
			player.honeyCombItem = currentItem;
			player.panic = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<NeutronInsignia>());
			recipe.AddIngredient(ItemID.AnkhShield); //Ankh Shield
			recipe.AddIngredient(ItemID.FrozenTurtleShell); //Frozen Turtle Shell
			recipe.AddIngredient(ItemID.PocketMirror); //Pocket Mirror
			recipe.AddIngredient(ItemID.HandWarmer); //Hand Warmer
			recipe.AddIngredient(ItemID.PhilosophersStone); //Philosopher's Stone
			recipe.AddIngredient(ItemID.StarVeil); //Star Veil
			recipe.AddIngredient(ItemID.SweetheartNecklace); //Sweetheart Necklace
			recipe.AddIngredient(ItemID.FleshKnuckles); //Flesh Knuckles
			recipe.AddIngredient(ItemID.Shackle);
			recipe.AddIngredient(Mod, "WoodenShield");
			recipe.AddIngredient(Mod, "SoulFragment");
			recipe.AddTile(Mod, "Infinity");
			recipe.Register();
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D texture = (Texture2D)Mod.Assets.Request<Texture2D>("Items/Accessories/Runes/Neutron_Glow");

			spriteBatch.Draw(texture, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f),
				new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
		}
	}
}