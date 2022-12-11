using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Amaza
{
	public class AmazaChakram : ModItem
	{
		public override string Texture => GetInstance<Assets.Config.PersonalConfig>().ClassicAmaza ? base.Texture + "_Old" : base.Texture;

		public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("Chases after enemies");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Throwing;

			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = 9000;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.noUseGraphic = true;
			Item.width = 18;
			Item.autoReuse = true;
			Item.height = 32;

			//item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<AmazaChakramP>();
			Item.shootSpeed = 13f;
			Item.channel = true;
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<AmazaChakram>());
			recipe.AddIngredient(Mod, "AmazaBar", 14);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	internal class AmazaChakramP : ModProjectile
	{
		public override string Texture => GetInstance<Assets.Config.PersonalConfig>().ClassicAmaza ? base.Texture + "_Old" : base.Texture;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Amaza Chakram");
		}

		public override void SetDefaults()
		{
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.CloneDefaults(182);
			AIType = 182;
		}
	}
}