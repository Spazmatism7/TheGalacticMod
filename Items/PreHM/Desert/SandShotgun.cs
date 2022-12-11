using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Input;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Desert
{
	public class SandShotgun : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 16;
			Item.useTime = 16;
			Item.autoReuse = true;
			Item.width = 59;
			Item.height = 32;
			Item.shoot = ProjectileID.SandBallGun;
			Item.useAmmo = AmmoID.Sand;
			Item.UseSound = SoundID.Item11;
			Item.damage = 42;
			Item.shootSpeed = 12f;
			Item.noMelee = true;
			Item.knockBack = 5f;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.DamageType = DamageClass.Ranged;
		}
	}
}