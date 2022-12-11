using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Nautilus
{
	public class NautilusTome : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Fires bouncing oceean forage");
        }

		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 14;
			Item.width = 26;
			Item.height = 26;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.channel = true; //Channel so that you can held the weapon [Important]
			Item.knockBack = 8;
			Item.value = Item.sellPrice(silver: 50);
			Item.rare = ItemRarityID.Blue;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item9;
			Item.shootSpeed = 10f;
			Item.shoot = ProjectileType<NautilusShell>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<NautilusTome>());
			recipe.AddIngredient(ItemID.Seashell, 3); //Seashells
			recipe.AddIngredient(ItemID.Coral); //Coral
			recipe.AddIngredient(ItemID.Starfish); //Starfish
			recipe.AddIngredient(ItemID.Book);
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = Main.rand.Next(new int[] { type, ProjectileType<NautilusStarfish>(), ProjectileType<JunoniaShell>(), ProjectileType<LightningWhelkShell>(), ProjectileType<TulipShell>() });
        }
    }
}