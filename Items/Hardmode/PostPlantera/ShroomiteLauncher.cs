using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace GalacticMod.Items.Hardmode.PostPlantera
{
    public class ShroomiteLauncher : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Cannon");
            Tooltip.SetDefault("Fires Shroomite Rockets which are surrounded by mushrooms");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 22;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 45;
            Item.useAnimation = 30;
            Item.useTurn = false;
            Item.autoReuse = false;
            Item.DamageType = DamageClass.Ranged;
            Item.shoot = ProjectileID.RocketI;
            Item.useAmmo = AmmoID.Rocket;
            Item.damage = 20;
            Item.crit = -2;
            Item.knockBack = 6f;
            Item.shootSpeed = 10f;
            Item.noMelee = true; //Does the weapon itself inflict damage?
        }

        public override Vector2? HoldoutOffset() => new Vector2(-10, 0);

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ModContent.ProjectileType<ShroomRocketProj>(), damage, knockback, player.whoAmI);

                SoundEngine.PlaySound(SoundID.Item61, position);
            }

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<ShroomiteLauncher>());
            recipe.AddIngredient(ItemID.ShroomiteBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public class ShroomRocketProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Rocket");
        }

        public override void SetDefaults()
        {
            Projectile.width = 16; //Actually 18
            Projectile.height = 26; //Actually 28
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

        int timeleft = 240;
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
            timeleft--;
            if (timeleft <= 0)
            {
                Projectile.Kill();
            }
            for (int i = 0; i < 2; i++)
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.Center.X - 5, Projectile.Center.Y - 5), 10, 10, DustID.UnusedWhiteBluePurple, 0f, 0f, 100, default, 1.5f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].velocity *= 0.5f;
                Main.dust[dustIndex].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
            }
            int dustIndex2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueCrystalShard, 0f, 0f, 100, default, 1f);
            Main.dust[dustIndex2].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex2].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex2].noGravity = true;

            if (Projectile.owner == Main.myPlayer)
            {
                int numberProjectiles = 3;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(0, -12).RotatedByRandom(MathHelper.ToRadians(360));

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y),
                        ProjectileType<Shroom>(), (int)(Projectile.damage * 0.4f), 1, Projectile.owner);
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);

            for (int i = 0; i < 25; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.BlueCrystalShard);
                dust.noGravity = true;
                dust.scale = 1.5f;
                dust.velocity *= 4f;
                dust.fadeIn = 1f;
            }
            for (int i = 0; i < 25; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.UnusedWhiteBluePurple);
                dust.noGravity = true;
                dust.scale = 2f;
                dust.velocity *= 4f;
                dust.fadeIn = 1f;
            }
        }
    }

    public class Shroom : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom");
        }

        public override void SetDefaults()
        {
            Projectile.width = 12; //22
            Projectile.height = 14; //24
            Projectile.CloneDefaults(131);
            AIType = 131;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.timeLeft = 50;
            Projectile.tileCollide = false;

        }

        int damagetime;

        public override void AI()
        {
            Projectile.penetrate = 1;
            damagetime++;
        }

        public override bool? CanDamage()
        {
            if (damagetime < 8)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.UnusedWhiteBluePurple);
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Color.White;
            color.A = 50;
            return color;
        }
    }
}