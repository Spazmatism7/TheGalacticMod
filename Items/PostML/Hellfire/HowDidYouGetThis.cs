using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using GalacticMod.Projectiles;

namespace GalacticMod.Items.PostML.Hellfire
{
	public class HowDidYouGetThis : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Works Great against the DoG");
		}

		public override void SetDefaults()
		{
			Item.damage = 69420;
			Item.DamageType = DamageClass.Melee; //melee weapon
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 0;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 100000; //sell value
			Item.rare = -12;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
			Item.shoot = ModContent.ProjectileType<HellSickleProjectile>();
			Item.shootSpeed = 200f;
		}
	}
}