using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Rarities;
using GalacticMod.Projectiles;

namespace GalacticMod.Items.PreHM.Mage
{
	public class PrototypePrism : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("v001");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
		}

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 8;
			Item.width = 24;
			Item.height = 32;
			Item.noMelee = true;
			Item.useTurn = true; //player can turn while animation is happening
			Item.useTime = 22;
			Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Rapier;
			Item.noUseGraphic = true;
			Item.knockBack = 5;
			Item.value = 10000;
			Item.UseSound = SoundID.Item20;
			Item.rare = ItemRarityID.Blue;
			Item.autoReuse = false;
			Item.shoot = ProjectileType<PrototypeBlast>();
			Item.shootSpeed = 16f;
		}
	}
}