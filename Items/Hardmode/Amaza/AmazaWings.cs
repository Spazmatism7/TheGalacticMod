using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.Hardmode.Amaza
{
	[AutoloadEquip(EquipType.Wings)]
	public class AmazaWings : ModItem
	{
		public override string Texture => GetInstance<Assets.Config.PersonalConfig>().ClassicAmaza ? base.Texture + "_Old" : base.Texture;

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Allows flight and slow fall");
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(190, 8.5f, 2.2f);
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            WingsLayer.RegisterData(Item.wingSlot, new DrawLayerData()
            {
                Texture = Request<Texture2D>(Texture + "_Wings_Glow")
            });
        }

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 28;
			Item.value = 10000;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightPurple;
			Item.canBePlacedInVanityRegardlessOfConditions = true;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
		{
			ascentWhenFalling = 2f;
			ascentWhenRising = 0.3f;
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 2.2f;
			constantAscend = 0.15f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<AmazaWings>());
			recipe.AddIngredient(ItemType<AmazaBar>(), 20);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = (Texture2D)Mod.Assets.Request<Texture2D>("Items/Hardmode/Amaza/AmazaWings_Glow");

            spriteBatch.Draw(texture, new Vector2(Item.position.X - Main.screenPosition.X + Item.width * 0.5f, Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f),
                new Rectangle(0, 0, texture.Width, texture.Height), Color.White, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
        }
    }
}
