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
using GalacticMod.Buffs;
using GalacticMod.Items.ThrowingClass.Weapons.Knives;

namespace GalacticMod.Items.Hardmode.Terra
{
    public class Terrager : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Launches 3 Terra Daggers");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 74;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 22;
            Item.height = 44;
            Item.noMelee = true;
            Item.useTurn = true; //player can turn while animation is happening
            Item.noUseGraphic = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 5;
            Item.value = 10000;
            Item.UseSound = SoundID.Item20;
            Item.rare = ItemRarityID.Lime;
            Item.crit = 1;
            Item.autoReuse = true;
            Item.shoot = ProjectileType<TerragerP>();
            Item.shootSpeed = 6f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 3;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(30)); // This defines the projectiles random spread; 5 degree spread.
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
            }
            SoundEngine.PlaySound(SoundID.Item, player.Center);
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<Terrager>());
            recipe.AddIngredient(Mod, "HolyDagger");
            recipe.AddIngredient(Mod, "NightDagger");
            recipe.AddIngredient(Mod, "BarOfLife", 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }

    public class TerragerP : ModProjectile
    {
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrager Blades");
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 44;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.light = .75f;
        }

        public override void AI()
        {
            timer++;

            Projectile.rotation += 1.57f / 4;

            Lighting.AddLight(Projectile.Center, Color.LimeGreen.ToVector3() * 0.78f);

            if (timer >= 90)
                Projectile.velocity.Y += .1f;

            if (Main.rand.NextBool())
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TerraBlade, 0f, 0f, 100, default, 1f);
                Main.dust[dustIndex].scale = 1f + Main.rand.Next(5) * 0.1f;
                Main.dust[dustIndex].noGravity = true;
                int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Chlorophyte, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                Main.dust[dust2].noGravity = true; //this make so the dust has no gravity
                Main.dust[dust2].velocity *= -0.3f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) => target.AddBuff(BuffType<TerraBurn>(), 15 * 60);
    }
}