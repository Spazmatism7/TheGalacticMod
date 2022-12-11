using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace GalacticMod.Items.Hardmode.Asteroid
{
    public class IridiumBlaster : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 20;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.mana = 7;
            Item.autoReuse = true;
            Item.damage = 80;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4f;
            Item.noMelee = true;

            Item.UseSound = SoundID.Item11;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.LightRed;

            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<IridiumPellet>();
        }

        public override Vector2? HoldoutOffset() => new Vector2(-2f, -2f);

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<IridiumBlaster>());
            recipe.AddIngredient(Mod, "IridiumBar", 14);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public class IridiumPellet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 34;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 200 * 60;
            Projectile.light = 1f;
        }

        public override void AI()
        {
            Projectile.velocity.Y += Projectile.ai[0];

            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.Pi;
            }

            if (++Projectile.frame >= Main.projFrames[Projectile.type])
            {
                Projectile.frame = 0;
            }

            var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, default, 1f);
            var dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MeteorHead, 0, 0, 130, default, 0.5f);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0, 0, 100, default, 1f);
                var dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MeteorHead, 0, 0, 130, default, 0.5f);
            }
        }
    }
}