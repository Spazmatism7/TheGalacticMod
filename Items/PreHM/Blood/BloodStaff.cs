using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Input;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Blood
{
	public class BloodStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Item.staff[Item.type] = true;
        }

		public override void SetDefaults()
		{
			Item.damage = 27;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 8;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 30;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<BloodDrop>();
			Item.shootSpeed = 9f;
		}

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<BloodStaff>());
            recipe.AddIngredient(Mod, "BloodyDrop", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    public class BloodDrop : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 200;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;

            if (Projectile.ai[0] > 0f)  //this defines where the flames starts
            {
                if (Main.rand.NextBool(2))     //this defines how many dust to spawn
                {
                    int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                    Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                    Main.dust[dust].velocity *= -0.3f;
                    int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Water_BloodMoon, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                    Main.dust[dust2].noGravity = true; //this make so the dust has no gravity
                    Main.dust[dust2].velocity *= -0.3f;
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, 0, 0, 130, default, 0.5f);
                var dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Water_BloodMoon, 0, 0, 130, default, 0.5f);
            }
        }
    }

}