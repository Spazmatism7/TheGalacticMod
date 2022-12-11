using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using GalacticMod.Projectiles;
using GalacticMod.Items.PostML.Celestial;
using GalacticMod.Buffs;
using System;
using Terraria.Audio;

namespace GalacticMod.Items.Swords.CosmicEdgePath
{
	public class ElementalEdge : ModItem
    {
        //public override string Texture => $"GalacticMod/Items/Swords/CosmicEdgePath/ElementalEdge2";

        public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Elemental Destruction");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 58; //72
			Item.height = 58; //72
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.autoReuse = true;

			Item.DamageType = DamageClass.Melee;
			Item.damage = 110;
			Item.knockBack = 6;
			Item.crit = 6;
			Item.value = Item.buyPrice(gold: 5);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<ElementalTyphoon>();
			Item.shootSpeed = 20f; // Speed of the projectiles the sword will shoot
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<ElementalEdge>());
			recipe.AddIngredient(Mod, "IcyCrypt");
			recipe.AddIngredient(Mod, "SacredTide");
			recipe.AddIngredient(Mod, "SolarBlade");
			recipe.AddIngredient(Mod, "RockEdge");
			recipe.AddIngredient(ItemID.BrokenHeroSword);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}

	public class ElementalTyphoon : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 12;
		}

		public override void SetDefaults()
		{
			Projectile.timeLeft = 7 * 60;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 2;
			Projectile.height = 54;
			Projectile.width = 54;
			Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.aiStyle = 71;
			Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }

		public override void AI()
		{
			if (++Projectile.frameCounter >= 13)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= Main.projFrames[Projectile.type])
					Projectile.frame = 0;
			}

			if (Projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref Projectile.velocity);
				Projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 250f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && Main.npc[k].type != NPCID.TargetDummy)
				{
					if (Collision.CanHit(Projectile.Center, 0, 0, Main.npc[k].Center, 0, 0))
					{
						Vector2 newMove = Main.npc[k].Center - Projectile.Center;
						float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
						if (distanceTo < distance)
						{
							move = newMove;
							distance = distanceTo;
							target = true;
						}
					}
				}
			}
			if (target)
			{
				AdjustMagnitude(ref move);
				Projectile.velocity = (10 * Projectile.velocity + move) / 6f;
				AdjustMagnitude(ref Projectile.velocity);
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 4f)
			{
				vector *= 4f / magnitude;
			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.ai[0] += 0.1f;
			if (Projectile.velocity.X != oldVelocity.X)
			{
				Projectile.velocity.X = -oldVelocity.X;
			}
			if (Projectile.velocity.Y != oldVelocity.Y)
			{
				Projectile.velocity.Y = -oldVelocity.Y;
			}
			Projectile.velocity *= 0.75f;
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			return false;
		}
	}
}