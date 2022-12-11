using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;
using GalacticMod.Projectiles;
using Terraria.Audio;
using GalacticMod.Buffs;

namespace GalacticMod.Items.Hardmode.Terra
{
	public class TerraBolt : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Casts a orb of Terran flames");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 8;
			Item.width = 24;
			Item.height = 32;
			Item.noMelee = true;
			Item.useTurn = true; //player can turn while animation is happening
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 5;
			Item.value = 10000;
			Item.UseSound = SoundID.Item20;
			Item.rare = ItemRarityID.Lime;
			Item.autoReuse = true;
			Item.shoot = ProjectileType<TerraBlast>();
			Item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<TerraBolt>());
			recipe.AddIngredient(Mod, "FrostflareBolt");
			recipe.AddIngredient(ItemID.WaterBolt);
            recipe.AddIngredient(Mod, "BarOfLife", 7);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

	public class TerraBlast : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 5;
			Projectile.timeLeft = 600;
			Projectile.alpha = 210; // How transparent to draw this projectile. 0 to 255. 255 is completely transparent.
		}

		public override void AI()
		{
			Projectile.velocity.Y += Projectile.ai[0];
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

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.penetrate--;
			if (Projectile.penetrate <= 0)
			{
				Projectile.Kill();
			}
			else
			{
				Projectile.ai[0] += 0.1f;
				if (Projectile.velocity.X != oldVelocity.X)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}
				Projectile.velocity *= 0.75f;
				SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			}
			Projectile.NewProjectile(null, new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.velocity - Projectile.velocity, ProjectileType<TerraSplosion>(), 80, 0, Projectile.owner);
			
			return false;
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			Projectile.ai[0] += 0.1f;
			Projectile.velocity *= 0.75f;
		}

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) => target.AddBuff(BuffType<TerraBurn>(), 15 * 60);

        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit) => target.AddBuff(BuffType<TerraBurn>(), 15 * 60);
    }

	public class TerraSplosion : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terrasplosion");
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.friendly = true;
            Projectile.timeLeft = 20;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.scale = 1.5f;
            Projectile.aiStyle = -1;
            Projectile.alpha = 0;
            DrawOffsetX = 25;
            DrawOriginOffsetY = 25;
            Projectile.light = 0.9f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1;
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
            Color color = Color.LimeGreen;
            color.A = 50;
            return color;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffType<TerraBurn>(), 420);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffType<TerraBurn>(), 420);
        }
    }
}