using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Nautilus
{
	public class NautilusBlade : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Tooltip.SetDefault("Fires random oceean forage");
		}

		public override void SetDefaults()
		{
			Item.damage = 20;
			Item.DamageType = DamageClass.Melee; //melee weapon
			Item.width = 32;
			Item.height = 42;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 100000; //sell value
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
			Item.shootSpeed = 8f;
			Item.shoot = ProjectileType<NautilusShell>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<NautilusBlade>());
			recipe.AddIngredient(ItemID.Seashell, 3); //Seashells
			recipe.AddIngredient(ItemID.Coral); //Coral
			recipe.AddIngredient(ItemID.Starfish); //Starfish
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			type = Main.rand.Next(new int[] { type, ProjectileType<NautilusStarfish>(), ProjectileType<JunoniaShell>(), ProjectileType<LightningWhelkShell>(), ProjectileType<TulipShell>() });
		}
	}
}