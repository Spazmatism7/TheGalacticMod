using GalacticMod.Buffs;
using GalacticMod.Items.Hardmode.PostPlantera;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace GalacticMod.Items.Hardmode.Asteroid
{
    public class IridiumLauncher : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a special rocket that inflicts Iridium Poisoning");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.rare = ItemRarityID.LightPurple;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 67;
            Item.knockBack = 5f;
            Item.noMelee = true;
            Item.shootSpeed = 16f;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.useTurn = false;
            Item.shoot = ProjectileID.RocketI;
            Item.useAmmo = AmmoID.Rocket;
            Item.knockBack = 6f;
            Item.crit = -5;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<IridiumLauncher>());
            recipe.AddIngredient(Mod, "IridiumBar", 14);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset() => new Vector2(2f, -2f);

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<IridiumRocket>(), damage, knockback, player.whoAmI);

                SoundEngine.PlaySound(SoundID.Item61, position);
            }

            return false;
        }
    }

    public class IridiumRocket : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 14; //Actually 16
            Projectile.height = 22; //Actually 24
            Projectile.light = 0.2f;
            Projectile.friendly = true;
            Projectile.aiStyle = 0;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 300;
            DrawOffsetX = 0;
            DrawOriginOffsetY = 0;
            Projectile.alpha = 100;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            for (int i = 0; i < 2; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X - 5, Projectile.Center.Y - 5), 10, 10, DustID.YellowTorch, 0f, 0f, 100, default, 1.5f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 0.5f;
                Main.dust[dustIndex].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
            }
            int dustIndex2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Silver, 0f, 0f, 100, default, 1f);
            Main.dust[dustIndex2].scale = 0.1f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex2].fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex2].noGravity = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<IridiumPoison>(), 240);
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            for (int i = 0; i < 25; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.YellowTorch);
                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.velocity *= 4f;
                dust.fadeIn = 1f;
            }
            for (int i = 0; i < 25; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Silver);
                dust.noGravity = true;
                dust.scale = 2f;
                dust.velocity *= 4f;
                dust.fadeIn = 1f;
            }
        }
    }
}