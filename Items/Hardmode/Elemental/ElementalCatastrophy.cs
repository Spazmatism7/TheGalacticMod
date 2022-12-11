using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using System.Reflection.Metadata;
using Terraria.Audio;
using GalacticMod.Buffs;

namespace GalacticMod.Items.Hardmode.Elemental
{
    public class ElementalCatastrophy : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Elemental Destruction");
        }

        public override void SetDefaults()
        {
            Item.damage = 110;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 62;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
            Item.crit = 1;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Arrow;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player) => true;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ProjectileType<ElementalArrow>();

            if (player.altFunctionUse == 2) //Right Click
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y + 50), new Vector2(perturbedSpeed.X, perturbedSpeed.Y - 2), type, damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), type, damage, knockback, player.whoAmI);
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y - 50), new Vector2(perturbedSpeed.X, perturbedSpeed.Y + 2), type, damage, knockback, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item36 with { Volume = 1f, Pitch = 0.5f }, player.Center);
            }
            else
            {
                Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                float ceilingLimit = target.Y;
                if (ceilingLimit > player.Center.Y - 200f)
                {
                    ceilingLimit = player.Center.Y - 200f;
                }
                // Loop these functions 6 times.
                for (int i = 0; i < 6; i++)
                {
                    position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                    position.Y -= 100 * i;
                    Vector2 heading = target - position;

                    if (heading.Y < 0f)
                    {
                        heading.Y *= -1f;
                    }

                    if (heading.Y < 20f)
                    {
                        heading.Y = 20f;
                    }

                    heading.Normalize();
                    heading *= velocity.Length();
                    heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                    Projectile.NewProjectile(source, position, heading, type, damage, knockback, player.whoAmI, 0f, ceilingLimit);
                }
            }

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<ElementalCatastrophy>());
            recipe.AddIngredient(Mod, "ShadowfireBow");
            recipe.AddIngredient(ItemID.IceBow);
            recipe.AddIngredient(ItemID.DaedalusStormbow);
            recipe.AddIngredient(Mod, "OsmiumBar", 5);
            recipe.AddIngredient(Mod, "BarOfLife", 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }

    public class ElementalArrow : ModProjectile
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
            Projectile.extraUpdates = 2;
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
            if (Projectile.timeLeft < 3598)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Frost, Vector2.Zero);
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
            for (int i = 0; i < 12; i++)
            {
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Frost);
                d.velocity *= 2;
                d.noGravity = true;
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            /*target.AddBuff(BuffID.ShadowFlame, 15 * 60);
            target.AddBuff(BuffID.OnFire, 15 * 60);
            target.AddBuff(BuffID.Frostburn, 15 * 60);*/
            target.AddBuff(BuffType<ElementalBlaze>(), 15 * 60);
        }

        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            /*target.AddBuff(BuffID.ShadowFlame, 15 * 60);
            target.AddBuff(BuffID.OnFire, 15 * 60);
            target.AddBuff(BuffID.Frostburn, 15 * 60);*/
            target.AddBuff(BuffType<ElementalBlaze>(), 15 * 60);
        }
    }
}