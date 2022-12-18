using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod;
using Terraria.DataStructures;
using System;
using Terraria.Audio;
using GalacticMod.Items.Hardmode;
using GalacticMod.Buffs;

namespace GalacticMod.Items.PostML.Celestial
{
    public class NebulaFlameItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nebula Flamer");
            Tooltip.SetDefault("Casts a stream of shadowfire");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 6));

            ItemID.Sets.ItemIconPulse[Item.type] = true; // The item pulses while in the player's inventory
        }

        public override void SetDefaults()
        {
            Item.mana = 5;
            Item.damage = 70;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 16;
            Item.height = 30;
            Item.UseSound = SoundID.Item20;
            Item.useAnimation = 30;
            Item.useTime = 4;
            Item.rare = ItemRarityID.Red;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.knockBack = 6.5f;
            Item.DamageType = DamageClass.Magic;
            Item.shoot = ProjectileType<NebulaFlame>();
            Item.shootSpeed = 4f;
            Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<NebulaFlameItem>());
            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(ItemID.FragmentNebula, 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 3), new Vector2(velocity.X, velocity.Y), type, damage / 2, knockback, player.whoAmI, 0, 0);
            return false;
        }
    }

    public class NebulaFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;

        }
        public override void SetDefaults()
        {
            Projectile.width = 300;
            Projectile.height = 300;
            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
            Projectile.extraUpdates = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.scale = 0.1f;
            DrawOffsetX = -35;
            DrawOriginOffsetY = -35;
            Projectile.light = 0.8f;
            Projectile.ArmorPenetration = 15;
        }

        public override bool? CanDamage()
        {
            if (Projectile.ai[1] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        int dustoffset;
        public override void AI()
        {
            dustoffset++;
            Projectile.rotation += 0.1f;

            if (Main.rand.NextBool(10))     //this defines how many dust to spawn
            {
                int dust = Dust.NewDust(new Vector2(Projectile.position.X - (dustoffset / 2), Projectile.position.Y - (dustoffset / 2)), Projectile.width + dustoffset, Projectile.height + dustoffset, DustID.Shadowflame, Projectile.velocity.X * 1f, Projectile.velocity.Y * 1f, 130, default, 1f);
                Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                Main.dust[dust].velocity *= 0.5f;
                //int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 55, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);
            }

            if (Projectile.scale <= 1f)
                Projectile.scale += 0.012f;

            else
            {
                Projectile.alpha += 3;

                Projectile.velocity.X *= 0.98f;
                Projectile.velocity.Y *= 0.98f;

                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 10) // This will change the sprite every 8 frames (0.13 seconds). Feel free to experiment.
                {
                    Projectile.frame++;
                    Projectile.frameCounter = 0;
                }
            }

            if (Projectile.alpha > 150 || Projectile.wet)
                Projectile.Kill();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.damage = Projectile.damage * 9 / 10;
            target.AddBuff(BuffType<NebulaFlameD>(), 240);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffType<NebulaFlameD>(), 240);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity *= 0;
            return false;
        }
    }
}