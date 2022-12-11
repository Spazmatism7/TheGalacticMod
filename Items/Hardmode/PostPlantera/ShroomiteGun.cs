using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using GalacticMod.Items.PostML.Hellfire;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace GalacticMod.Items.Hardmode.PostPlantera
{
	public class ShroomiteGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("25% chance not to consume ammo" +
                "\nConverts regular bullets into Shroomite Bullets");
			DisplayName.SetDefault("Shroom Blaster");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 90;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 52;
			Item.height = 20;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 200000;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item11;
			Item.crit = 10;
			Item.autoReuse = false;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 8f;
			Item.useAmmo = AmmoID.Bullet;
		}

        public override bool CanConsumeAmmo(Item ammo, Player player) => Main.rand.NextFloat() >= .25f;

        public override Vector2? HoldoutOffset() => new Vector2(-8, 0);

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.Bullet || type == ProjectileID.BulletHighVelocity)
            {
                type = ProjectileType<ShroomiteBulletP>();
            }
        }

        public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<ShroomiteGun>());
			recipe.AddIngredient(ItemID.ShroomiteBar, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

    public class ShroomiteBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Looses speed every time it strikes a tile");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 14;
            Item.maxStack = 999;
            Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
            Item.knockBack = 1.5f;
            Item.rare = ItemRarityID.Pink;
            Item.shoot = ProjectileType<ShroomiteBulletP>();   //The projectile shoot when your weapon using this ammo
            Item.shootSpeed = 16f;                  //The speed of the projectile
            Item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<ShroomiteBullet>(), 50);
            recipe.AddIngredient(ItemID.MusketBall, 50);
            recipe.AddIngredient(ItemID.ShroomiteBar);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public class ShroomiteBulletP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Bullet");     //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;    //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;        //The recording mode
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;

            Projectile.aiStyle = 1;
            Projectile.light = 0.4f;

            Projectile.friendly = true;
            Projectile.penetrate = 5;

            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.Bullet;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 2;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = -Projectile.velocity;
            Projectile.velocity /= 2;

            return false;
        }
    }
}