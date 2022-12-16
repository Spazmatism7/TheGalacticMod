using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Projectiles;
using GalacticMod.Items.Swords.CosmicEdgePath;
using Terraria.DataStructures;
using GalacticMod.Items.PreHM.Blood;
using Terraria.GameContent.ItemDropRules;

namespace GalacticMod.Items.PreHM
{
	public class Shadowshot : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Tooltip.SetDefault("Holds the Darkest of Souls");
		}

		public override void SetDefaults()
		{
			Item.damage = 32;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 22;
			Item.height = 44;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 4;
			Item.value = 10000;
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.VilePowder;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.Arrow;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<Shadowshot>());
			recipe.AddRecipeGroup("GalacticMod:EvilBow");
            recipe.AddIngredient(ItemType<TideTurner>());
			recipe.AddIngredient(ItemID.BeesKnees);
			recipe.AddIngredient(ItemID.MoltenFury);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			if (type == ProjectileID.FireArrow || type == ProjectileID.UnholyArrow || type == ProjectileID.WoodenArrowFriendly || type == ProjectileID.FrostArrow)
			{
				type = ProjectileID.ShadowFlameArrow;
			}
			if (type == ProjectileID.JestersArrow || type == ProjectileID.HellfireArrow)
            {
				type = ProjectileID.HellfireArrow;
                knockback *= 2.5f;
			}
		}
	}

    public class TideTurner : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 26;
            Item.height = 56;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.VilePowder;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
            Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<Tide>(), damage, knockback, player.whoAmI);

            return false;
        }
    }

    class TideNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            //Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDungeon
            if (npc.type is NPCID.AngryBones or NPCID.AngryBonesBig or NPCID.AngryBonesBigHelmet or NPCID.AngryBonesBigMuscle or NPCID.DarkCaster)
            {
                npcLoot.Add(ItemDropRule.Common(ItemType<TideTurner>(), 200));
            }
        }
    }

    public class Tide : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 10;
            Projectile.light = 0.5f;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 9;
        }

        public override void AI()
        {
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = Projectile.position;
            dust = Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.Water, 0f, 0f, 0, new Color(65, 124, 228, 50), 1f);
            dust.noGravity = true;

            for (int i = 0; i < 10; i++)
            {
                float X = Projectile.Center.X - Projectile.velocity.X / 10f * i;
                float Y = Projectile.Center.Y - Projectile.velocity.Y / 10f * i;

                int dust2 = Dust.NewDust(new Vector2(X, Y), 0, 0, DustID.WaterCandle, 0, 0, 100, default, 1f);
                Main.dust[dust2].position.X = X;
                Main.dust[dust2].position.Y = Y;
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 0f;
            }

            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
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
                Color color = new Color(65, 124, 228, 50);
                spriteBatch.Draw(texture, drawPos, null, color * 0.45f, Projectile.oldRot[k] + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
                spriteBatch.Draw(texture, drawPos - Projectile.oldPos[k] * 0.5f + Projectile.oldPos[k + 1] * 0.5f, null, color * 0.45f, Projectile.oldRot[k] * 0.5f + Projectile.oldRot[k + 1] * 0.5f + (float)Math.PI / 2, drawOrigin, Projectile.scale - k / (float)Projectile.oldPos.Length, effects, 0f);
            }
            return true;
        }
    }
}