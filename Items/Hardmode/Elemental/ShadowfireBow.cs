using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using GalacticMod.Buffs;

namespace GalacticMod.Items.Hardmode.Elemental
{
    public class ShadowfireBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 18;
            Item.height = 36;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Pink;
            Item.crit = 1;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<ShadowfireBow>());
            recipe.AddIngredient(ItemID.MoltenFury);
            recipe.AddIngredient(ItemID.ShadowFlameBow);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ProjectileType<ShadowfireArrow>();
        }
    }

    public class ShadowfireArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.aiStyle = 1;
            Projectile.width = 14;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.penetrate = 4;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d.noGravity = true;
            }
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.FlameBurst, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d.noGravity = true;
            }
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame, Vector2.Zero);
                d.frame.Y = Main.rand.NextBool(2) ? 0 : 10;
                d.noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Torch);
                d.velocity *= 2;
                d.noGravity = true;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.FlameBurst);
                d.velocity *= 2;
                d.noGravity = true;
            }
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Shadowflame);
                d.velocity *= 2;
                d.noGravity = true;
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffID.ShadowFlame, 15 * 60);
            target.AddBuff(BuffID.OnFire, 15 * 60);
        }

        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 15 * 60);
            target.AddBuff(BuffID.OnFire, 15 * 60);
        }
    }
}