using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Buffs;

namespace GalacticMod.Items.Hardmode.Ancient
{
	public class AncientBow : ModItem
	{
        public override string Texture => "GalacticMod/Items/Hardmode/Ancient/BowOfShock";

        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Bow of Shock");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Tooltip.SetDefault("Converts arrows into arcing beams of energy");
		}

		public override void SetDefaults()
		{
			Item.damage = 77;
			Item.DamageType = DamageClass.Ranged; 
			Item.width = 22;
			Item.height = 58;
			Item.useTime = 14;
			Item.useAnimation = 14; 
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000; 
			Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 20f;
			Item.useAmmo = AmmoID.Arrow;
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) => type = ProjectileType<Shock>();
	}

    public class Shock : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ray of Shock");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.MaxUpdates = 2;
            //Projectile.timeLeft = 180;
            Projectile.ignoreWater = true;
            Projectile.alpha = 250;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.penetrate = 4;
            Projectile.arrow = true;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, new Vector3(33, 89, 77));

            for (int i = 0; i < 10; i++)
            {
                float X = Projectile.Center.X - Projectile.velocity.X / 10f * i;
                float Y = Projectile.Center.Y - Projectile.velocity.Y / 10f * i;

                int dust2 = Dust.NewDust(new Vector2(X, Y), 0, 0, DustID.Electric, 0, 0, 100, new Color(33, 89, 77), 1f);
                Main.dust[dust2].position.X = X;
                Main.dust[dust2].position.Y = Y;
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 0f;
            }

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D texture = (Texture2D)Request<Texture2D>("GalacticMod/Assets/Graphics/LightTrail_1");
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            SpriteEffects effects = (Projectile.spriteDirection == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            for (int k = 0; k < Projectile.oldPos.Length - 1; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                Color color = new Color(33, 89, 77);
                spriteBatch.Draw(texture, drawPos, null, color * 0.45f, Projectile.oldRot[k] + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
                spriteBatch.Draw(texture, drawPos - Projectile.oldPos[k] * 0.5f + Projectile.oldPos[k + 1] * 0.5f, null, color * 0.45f, Projectile.oldRot[k] * 0.5f + Projectile.oldRot[k + 1] * 0.5f + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
            }
            return true;
        }
    }
}