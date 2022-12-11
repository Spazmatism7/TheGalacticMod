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
	public class Boomerangs : GlobalItem
	{
        public override void SetDefaults(Item Item)
		{
			if (!GetInstance<GalacticModConfig>().NoVThrown)
			{
				if (Item.type is ItemID.Bananarang or ItemID.BloodyMachete or ItemID.CombatWrench or ItemID.EnchantedBoomerang or ItemID.Flamarang or ItemID.FruitcakeChakram
					or ItemID.LightDisc or ItemID.PaladinsHammer or ItemID.PossessedHatchet or ItemID.Shroomerang or ItemID.ThornChakram or ItemID.WoodenBoomerang) 
				{
					Item.DamageType = DamageClass.Throwing;
				}

				//Maybe keep melee
				//Flying Knife and Sergeant United Shield
				if (Item.type is ItemID.FlyingKnife or ItemID.BouncingShield)
				{
					Item.DamageType = DamageClass.Throwing;
				}
			}

			if (Item.type == ItemID.LightDisc) //Light Discs
			{
				Item.damage = 59;
			}

			if (Item.type is ItemID.EnchantedBoomerang or ItemID.Shroomerang) //Enchanted Boomerang, Shroomerang
			{
				Item.autoReuse = true;
			}

			if (Item.type == ItemID.BloodyMachete)
			{
				Item.crit += 2;
			}
		}
	}

	public class BoomerangsP : GlobalProjectile
	{
        public override void SetDefaults(Projectile Projectile)
		{
			if (!GetInstance<GalacticModConfig>().NoVThrown)
			{
				//Bananarang, Bloody Machete, Combat Wrench, Enchanted Boomerang, Flamarang, Fruitcake Chakram, Ice Boomerang, Light Discs, Paladin's Hammer, Possessed Hatchet, Shoomerang, Thorn Chakram, Wooden Boomerang
				if (Projectile.type is ProjectileID.Bananarang or ProjectileID.BloodyMachete or ProjectileID.CombatWrench or ProjectileID.EnchantedBoomerang 
					or ProjectileID.Flamarang or ProjectileID.FruitcakeChakram or ProjectileID.IceBoomerang or ProjectileID.LightDisc or ProjectileID.PaladinsHammerFriendly 
					or ProjectileID.PossessedHatchet or ProjectileID.Shroomerang or ProjectileID.WoodenBoomerang)
				{
					Projectile.DamageType = DamageClass.Throwing;
				}

				//Maybe keep melee
				//Flying Knife and Sergeant United Shield
				if (Projectile.type is ProjectileID.FlyingKnife or ProjectileID.BouncingShield)
				{
					Projectile.DamageType = DamageClass.Throwing;
				}
			}

			if (Projectile.type is ProjectileID.IceBoomerang or ProjectileID.FruitcakeChakram or ProjectileID.ThornChakram)
			{
				Projectile.penetrate++;
			}
		}

		public override void ModifyHitNPC(Projectile Projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (Projectile.type is ProjectileID.WoodenBoomerang or ProjectileID.EnchantedBoomerang or ProjectileID.Shroomerang)
			{
				target.AddBuff(BuffID.Confused, 4 * 60);
			}
		}
	}
}