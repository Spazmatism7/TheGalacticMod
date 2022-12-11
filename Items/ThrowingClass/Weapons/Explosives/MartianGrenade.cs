using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Buffs;
using GalacticMod.Projectiles;

namespace GalacticMod.Items.ThrowingClass.Weapons.Explosives
{
    public class MartianGrenade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Inflicts Electrified on enemies");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.damage = 108;
            Item.DamageType = DamageClass.Throwing;
            Item.width = 10;
            Item.height = 14;
            Item.consumable = true;
            Item.maxStack = 999;
            Item.useTime = 40;
            Item.crit = 10;
            Item.useAnimation = 40;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 8;
            Item.value = Item.sellPrice(0, 0, 0, 16);
            Item.rare = ItemRarityID.Blue;
            Item.shootSpeed = 5f;
            Item.shoot = ProjectileType<MartianGrenadeP>();
            Item.autoReuse = false;
            Item.noMelee = true;

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            SoundEngine.PlaySound(SoundID.Item1, position);

            return true;
        }
    }

    public class MartianGrenadeP : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Martian Grenade");
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.light = 0.1f;
            Projectile.friendly = true;
            Projectile.CloneDefaults(30);
            AIType = 30;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            if (Projectile.owner == Main.myPlayer && Projectile.timeLeft <= 3)
            {
                Projectile.tileCollide = false;
                // Set to transparent. This projectile technically lives as  transparent for about 3 frames
                Projectile.alpha = 255;
                // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
                Projectile.position = Projectile.Center;

                Projectile.width = 100;
                Projectile.height = 100;
                Projectile.Center = Projectile.position;

                Projectile.velocity.X = 0;
                Projectile.velocity.Y = 0;
                Projectile.knockBack = 3f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.timeLeft > 3)
            {
                Projectile.timeLeft = 3;
            }
            if (Main.rand.NextBool(1)) //the chance
            {
                target.AddBuff(BuffID.Electrified, 600);
            }
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);

            Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), new Vector2(0), ProjectileType<MartianExplosion>(), 0, 0, Projectile.owner);

            for (int i = 0; i < 30; i++) //Grey dust circle
            {
                Vector2 perturbedSpeed = new Vector2(0, -2f).RotatedByRandom(MathHelper.ToRadians(360));
                var dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Smoke, perturbedSpeed.X, perturbedSpeed.Y);

                dust.noGravity = true;
                dust.scale = 2f;
                dust.velocity *= 2f;

            }
            for (int i = 0; i < 30; i++) //Grey dust fade
            {
                int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 0, default, 1f);
                Main.dust[dustIndex].scale = 0.1f + (Main.rand.Next(5) * 0.1f);
                Main.dust[dustIndex].fadeIn = 1.5f + Main.rand.Next(5) * 0.1f;
                Main.dust[dustIndex].noGravity = true;
            }
        }
    }

    class Martian : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.MartianEngineer)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<MartianGrenade>(), 7));
            }
        }
    }

    public class MartianExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.timeLeft = 20;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.scale = 1.5f;
            Projectile.aiStyle = -1;
            DrawOffsetX = 25;
            DrawOriginOffsetY = 25;
            Projectile.light = 0.9f;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 2) // This will change the sprite every 8 frames (0.13 seconds). Feel free to experiment.
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            Color color = Color.Cyan;
            color.A = 50;
            return color;
        }
    }
}