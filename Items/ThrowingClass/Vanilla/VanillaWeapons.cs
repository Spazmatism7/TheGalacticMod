using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Config;

namespace GalacticMod.Items.ThrowingClass.Vanilla
{
	public class VanillaWeapons : GlobalItem
	{
		public override void SetDefaults(Item Item)
		{
			//int Throwing = Item.DamageType = DamageClass.Throwing;

			if (!GetInstance<GalacticModConfig>().NoVThrown)
			{
				if (Item.type == ItemID.BoneGlove) //Bone Glove
				{
					Item.DamageType = DamageClass.Throwing;
				}

				if (Item.type == ItemID.AleThrowingGlove) //Ale Tosser
				{
					Item.DamageType = DamageClass.Throwing;
				}

				if (Item.type is ItemID.Snowball or ItemID.Shuriken or ItemID.RottenEgg or ItemID.ThrowingKnife or 287 or 1130 or 3379)
				{
					Item.DamageType = DamageClass.Throwing;
				}

				if (Item.type is 1913 or 161 or 3094 or 3197 or 154 or 2590 or 3378) //Star Anise, Spiky Ball, Javelin, Frost Daggerfish, Bone, Molotov Cocktail, Bone Javelin
				{
					Item.DamageType = DamageClass.Throwing;
				}

				if (Item.type is 3548 or 168 or 2586 or 3116) //Grenades
				{
					Item.DamageType = DamageClass.Throwing;
				}
			}
		}
	}

	public class VanillaWeaponsP : GlobalProjectile
	{
		public override void SetDefaults(Projectile Projectile)
		{
			if (!GetInstance<GalacticModConfig>().NoVThrown)
			{
				if (Projectile.type == ItemID.BoneGlove) //Bone Glove
				{
					Projectile.DamageType = DamageClass.Throwing;
				}

				if (Projectile.type == ItemID.AleThrowingGlove) //Ale Tosser
				{
					Projectile.DamageType = DamageClass.Throwing;
				}

				if (Projectile.type is ProjectileID.SnowBallFriendly or ProjectileID.Shuriken or ProjectileID.RottenEgg or ProjectileID.ThrowingKnife or ProjectileID.PoisonedKnife 
					or ProjectileID.Beenade or ProjectileID.BoneDagger)
				{
					Projectile.DamageType = DamageClass.Throwing;
				}

				if (Projectile.type is ProjectileID.StarAnise or ProjectileID.SpikyBall or ProjectileID.JavelinFriendly or ProjectileID.FrostDaggerfish or ProjectileID.Bone 
					or ProjectileID.MolotovCocktail or ProjectileID.BoneJavelin) 
					//Star Anise, Spiky Ball, Javelin, Frost Daggerfish, Bone, Molotov Cocktail, Bone Javelin
				{
					Projectile.DamageType = DamageClass.Throwing;
				}

				if (Projectile.type is ProjectileID.Grenade or ProjectileID.StickyGrenade or ProjectileID.BouncyGrenade or ProjectileID.HappyBomb) //Grenades
				{
					Projectile.DamageType = DamageClass.Throwing;
				}
			}
		}
	}
}