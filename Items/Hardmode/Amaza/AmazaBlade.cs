using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Amaza
{
	public class AmazaBlade : ModItem
	{
		public override string Texture => GetInstance<Assets.Config.PersonalConfig>().ClassicAmaza ? base.Texture + "_Old" : base.Texture;

		bool shoot = true;

		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 69;
			Item.DamageType = DamageClass.Melee; //melee weapon
			Item.width = 50;
			Item.height = 68;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = 1000; //sell value
			Item.rare = ItemRarityID.LightPurple;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening

			Item.shoot = ProjectileType<AmazaProjectile>();
			Item.shootSpeed = 8f;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (shoot)
			{
				shoot = false;
				return true;
			}
			else if (!shoot)
			{
				shoot = true;
				return false;
			}

			return false;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<AmazaBlade>());
			recipe.AddIngredient(Mod, "AmazaBar", 17);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class AmazaProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blue");
            Main.projFrames[Projectile.type] = 4;
        }

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = 44;
			AIType = 229;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.light = 0.2f;
            Projectile.noEnchantmentVisuals = true;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }

        public override void Kill(int timeLeft)
		{
            if (++Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }

            for (int i = 0; i < 16; i++)
			{
				Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Clentaminator_Blue);
				d.frame.Y = 0;
				d.velocity *= 2;
			}
		}

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(BuffID.Frostburn, 240);
		}
	}
}