using GalacticMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using GalacticMod.Items.Swords.CosmicEdgePath;
using Terraria.DataStructures;
using System;

namespace GalacticMod.Items.PreHM.Mage
{
	public class BorealWand : ModItem
	{
		public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons flurries of snow from the sky");
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 17;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 4;
			Item.width = 24;
			Item.height = 32;
			Item.useTime = 25;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 5;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<Snowflake1>();
			Item.shootSpeed = 2f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<BorealWand>());
			recipe.AddIngredient(ItemID.BorealWood, 20);
			recipe.AddIngredient(ItemID.IceBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			type = Main.rand.Next(new int[] { type, ProjectileType<Snowflake2>(), ProjectileType<Snowflake3>() });
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int index = 0; index < 3; ++index)
            {
                Vector2 vector2_1 = new Vector2((float)(player.position.X + player.width * 0.5 + (Main.rand.Next(201) * -player.direction) + (Main.mouseX + (double)Main.screenPosition.X - player.position.X)), (float)(player.position.Y + player.height * 0.5 - 600.0));   //this defines the projectile width, direction and position
                vector2_1.X = (float)(((double)vector2_1.X + player.Center.X) / 2.0) + Main.rand.Next(-200, 201);
                vector2_1.Y -= 100 * index;
                float num12 = Main.mouseX + Main.screenPosition.X - vector2_1.X;
                float num13 = Main.mouseY + Main.screenPosition.Y - vector2_1.Y;
                if ((double)num13 < 0.0) 
                    num13 *= -1f;
                if ((double)num13 < 20.0) 
                    num13 = 20f;

                float num14 = (float)Math.Sqrt((double)num12 * (double)num12 + (double)num13 * (double)num13);
                float num15 = Item.shootSpeed / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float SpeedX = num16 + Main.rand.Next(-20, 20) * 0.02f;  //this defines the projectile X position speed and randomnes
                float SpeedY = num17 + Main.rand.Next(-20, 20) * 0.02f;  //this defines the projectile Y position speed and randomnes
                Projectile.NewProjectile(source, new Vector2(vector2_1.X, vector2_1.Y), new Vector2(SpeedX, SpeedY), type, damage, knockback, player.whoAmI, 0.0f, Main.rand.Next(5));
            }
            return false;
        }
    }

	public class Snowflake1 : ModProjectile
	{
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Snowflake");
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 500;
            Projectile.aiStyle = 0;
            Projectile.light = 0.5f;
        }
    }

	public class Snowflake2 : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snowflake");
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 500;
            Projectile.aiStyle = 0;
            Projectile.light = 0.5f;
        }
    }

	public class Snowflake3 : ModProjectile
	{
		public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Snowflake");
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;

            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 500;
            Projectile.aiStyle = 0;
            Projectile.light = 0.5f;
        }
    }
}