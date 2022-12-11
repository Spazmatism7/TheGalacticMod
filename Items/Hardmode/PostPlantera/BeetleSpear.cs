using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using System.Threading.Tasks;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.PostPlantera
{
	public class BeetleSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Weevil Poker");
            Tooltip.SetDefault("Fires a beams randomly");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 84;
			Item.useStyle = ItemUseStyleID.Thrust;
			Item.useAnimation = 18;
			Item.useTime = 24;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.width = 32;
			Item.height = 32;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(silver: 10);

			Item.DamageType = DamageClass.Melee;
			Item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			Item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

			Item.UseSound = SoundID.Item1;
			Item.shoot = ModContent.ProjectileType<BeetleSpearP>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<BeetleSpear>());
			recipe.AddIngredient(ItemID.BeetleHusk, 13);
			recipe.AddIngredient(ItemID.ChlorophyteBar, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
				//Emit dusts when the sword is swung
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.MartianHit);
			}
		}

		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[Item.shoot] < 1;
		}
	}

	public class BeetleSpearP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beetle Spear");
		}

		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = 19;
			Projectile.penetrate = -1;
			Projectile.scale = 1.3f;
			Projectile.alpha = 0;

			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.tileCollide = false;
			Projectile.friendly = true;
		}

		public float movementFactor
		{
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public override void AI()
		{
			Player projOwner = Main.player[Projectile.owner];
			Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, true);
			Projectile.direction = projOwner.direction;
			projOwner.heldProj = Projectile.whoAmI;
			projOwner.itemTime = projOwner.itemAnimation;
			Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
			Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
			if (!projOwner.frozen)
			{
				if (movementFactor == 0f)
				{
					movementFactor = 3f;
					Projectile.netUpdate = true;
				}
				if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3)
				{
					movementFactor -= 2.4f;
				}
				else
				{
					movementFactor += 2.1f;
				}
			}
			Projectile.position += Projectile.velocity * movementFactor;
			if (projOwner.itemAnimation == 0)
			{
				Projectile.Kill();
			}
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
			if (Projectile.spriteDirection == -1)
			{
				Projectile.rotation -= MathHelper.ToRadians(90f);
			}
			SummonBeetleBolt();
		}

		private int damageBeam = 60;

		private void SummonBeetleBolt()
        {
			if (Projectile.localAI[0] > 0f)
			{
				return;
			}
			Projectile.localAI[0] = 1000f;
			Vector2 center = Projectile.Center;
			Vector2 zero = Vector2.Zero;
			int num = Main.rand.Next(2) * 2 - 1;
			Vector2 vector = new Vector2((float)num * (4f + (float)Main.rand.Next(3)), 0f);
			Vector2 vector2 = center + new Vector2(-num * 120, 0f);
			vector += (center + zero * 15f - vector2).SafeNormalize(Vector2.Zero) * 2f;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector2, vector, ModContent.ProjectileType<BeetleBeam>(), damageBeam, 0f, Projectile.owner);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];

			player.AddBuff(ModContent.BuffType<Buffs.BeetleBuff>(), 120);
		}
	}

	public class BeetleBeam : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true;
			Projectile.light = 3f;
			Projectile.scale = 1.1f;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.spriteDirection = Projectile.direction;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Player player = Main.player[Projectile.owner];

			player.AddBuff(ModContent.BuffType<Buffs.BeetleBuff>(), 120);
		}
	}
}