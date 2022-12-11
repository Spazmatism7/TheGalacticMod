using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod;
using Terraria.DataStructures;
using System;
using Terraria.Audio;

namespace GalacticMod.Items.Hardmode.Mage
{
	public class RoyalScepter : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Casts a blood-drawing beam");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 45;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 10;
			Item.width = 44;
			Item.height = 44;
			Item.noMelee = true;
			Item.useTurn = true; //player can turn while animation is happening
			Item.useTime = 10;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 5;
			Item.value = 10000;
			Item.UseSound = SoundID.Item20;
			Item.rare = ItemRarityID.Orange;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<RoyalBeam>();
			Item.shootSpeed = 18f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<RoyalScepter>());
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddIngredient(ItemID.SoulofFright, 2);
			recipe.AddIngredient(ItemID.SoulofMight, 2);
			recipe.AddIngredient(ItemID.SoulofSight, 2);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class RoyalBeam : ModProjectile
	{
		public int bounces = 7;

		public override void SetDefaults()
		{
			Projectile.width = 7;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 200 * 60;
			Projectile.extraUpdates = 6;
		}

		public override void AI()
		{
			int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust].velocity *= -0.3f;

			int dust3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.RedTorch, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust3].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust3].velocity *= -0.3f;

			int dust4 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.GemRuby, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust4].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust4].velocity *= -0.3f;

			int dust5 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TheDestroyer, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust5].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust5].velocity *= -0.3f;

			int dust6 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TheDestroyer, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust6].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust6].velocity *= -0.3f;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (bounces <= 0)
            {
                Projectile.Kill();
            }
            else
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
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            }
            bounces--;

            return false;
        }

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => target.AddBuff(BuffID.Bleeding, 20 * 60);
	}
}