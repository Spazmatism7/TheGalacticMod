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

namespace GalacticMod.Items.Hardmode.Terra
{
	public class TerraStave : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Casts a beam of pure energy" +
                "\nRight click to fire 3 Terra Shards");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
			Item.staff[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 74;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 8;
			Item.width = 54;
			Item.height = 54;
			Item.noMelee = true;
			Item.useTurn = true; //player can turn while animation is happening
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 5;
			Item.value = 10000;
			Item.UseSound = SoundID.Item20;
            Item.rare = ItemRarityID.Lime;
            Item.crit = 1;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<TerraBeam>();
			Item.shootSpeed = 18f;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2) //Right Click
            {
                type = ModContent.ProjectileType<TerraShards>();
                float numberProjectiles = 3;
                float rotation = MathHelper.ToRadians(45);
                position += Vector2.Normalize(velocity) * 45f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(source, position, perturbedSpeed, type, damage / 3, knockback, player.whoAmI);
                }
                SoundEngine.PlaySound(SoundID.Item, player.Center);
            }
            else //left Click
            {
                Projectile.NewProjectile(source, position, velocity, type, damage * 2, knockback, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item, player.Center);
            }
            return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<TerraStave>());
            recipe.AddIngredient(Mod, "RoyalScepter");
            recipe.AddIngredient(Mod, "NightStaff");
            recipe.AddIngredient(ItemID.VenomStaff);
            recipe.AddIngredient(Mod, "BarOfLife", 7);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class TerraBeam : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 30;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 200 * 60;
		}

		public override void AI()
		{
			int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TerraBlade, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust].velocity *= -0.3f;

			int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TerraBlade, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 2f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust2].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust2].velocity *= -0.3f;

			int dust3 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Clentaminator_Green, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust3].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust3].velocity *= -0.3f;

			int dust4 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Chlorophyte, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust4].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust4].velocity *= -0.3f;

			int dust5 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Confetti_Green, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust5].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust5].velocity *= -0.3f;

			int dust6 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Plantera_Green, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
			Main.dust[dust6].noGravity = true; //this make so the dust has no gravity
			Main.dust[dust6].velocity *= -0.3f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int terraSplosion = Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.velocity - Projectile.velocity, ProjectileType<TerraSplosion>(), damage, 0, Projectile.owner);
            Main.projectile[terraSplosion].timeLeft *= 2;
        }

        public override void Kill(int timeLeft)
        {
            int terraSplosion = Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.velocity - Projectile.velocity, ProjectileType<TerraSplosion>(), Projectile.damage, 0, Projectile.owner);
            Main.projectile[terraSplosion].timeLeft *= 2;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(BuffType<TerraBurn>(), 15 * 60);
        }

        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit) => target.AddBuff(BuffType<TerraBurn>(), 15 * 60);
    }

    internal class TerraShards : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 9;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 10 * 60;
            Projectile.extraUpdates = 1;
            Projectile.scale = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.tileCollide = false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffID.Venom, 15 * 60);
        }

        public override void AI()
        {
            int Adust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TerraBlade, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            Main.dust[Adust].noGravity = true; //this make so the dust has no gravity
            Main.dust[Adust].velocity *= -0.3f;

            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

            Lighting.AddLight(Projectile.Center, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0.1f) / 255f, ((255 - Projectile.alpha) * 0.1f) / 255f);   //this is the light colors
            /*if (Projectile.timeLeft > 125)
            {
                Projectile.timeLeft = 125;
            }*/
            if (Projectile.ai[0] > 0f)  //this defines where the flames starts
            {
                if (Main.rand.NextBool(2))     //this defines how many dust to spawn
                {
                    int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.TerraBlade, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 0.5f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                    Main.dust[dust].noGravity = true; //this make so the dust has no gravity
                    Main.dust[dust].velocity *= -0.3f;
                    int dust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Chlorophyte, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                    Main.dust[dust2].noGravity = true; //this make so the dust has no gravity
                    Main.dust[dust2].velocity *= -0.3f;
                }
            }
            else
            {
                Projectile.ai[0] += 1f;
            }
            if (Projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref Projectile.velocity);
                Projectile.localAI[0] = 1f;
            }
            Vector2 move = Vector2.Zero;
            float distance = 250f;
            bool target = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && Main.npc[k].type != NPCID.TargetDummy)
                {
                    if (Collision.CanHit(Projectile.Center, 0, 0, Main.npc[k].Center, 0, 0))
                    {
                        Vector2 newMove = Main.npc[k].Center - Projectile.Center;
                        float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                        if (distanceTo < distance)
                        {
                            move = newMove;
                            distance = distanceTo;
                            target = true;
                        }
                    }
                }
            }
            if (target)
            {
                AdjustMagnitude(ref move);
                Projectile.velocity = (10 * Projectile.velocity + move) / 6f;
                AdjustMagnitude(ref Projectile.velocity);
            }

        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 4f)
            {
                vector *= 4f / magnitude;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Pixie, 0, 0, 130, default, 0.5f);
                var dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Pixie, 0, 0, 130, default, 0.5f);
            }
        }
    }
}