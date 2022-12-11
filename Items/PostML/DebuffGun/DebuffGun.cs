using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using GalacticMod.Items.PostML.Celestial;
using GalacticMod.Items.Hardmode.Ancient;
using GalacticMod.Items.PostML.ImmediateOres;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Terraria.Audio;
using GalacticMod.Assets.Rarities;

namespace GalacticMod.Items.PostML.DebuffGun
{
	public class DebuffGun : ModItem
	{
		int shots = 0;

		public override void SetStaticDefaults()
        {
			Tooltip.SetDefault("The power of the Elements" +
				"\nRight click to keep the same projectile type");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 50; //Used to be 130
			Item.DamageType = DamageClass.Ranged;
			Item.width = 54;
			Item.height = 20;
			Item.useTime = 10; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 10; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTurn = true;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = 10000; // how much the item sells for (measured in copper)
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true; //Autofire
			Item.shootSpeed = 16f;
			Item.shoot = ProjectileType<FireProjectile>();
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
		}

		public override bool AltFunctionUse(Player player) => true;

		public override bool CanUseItem(Player player) => true;

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if (player.altFunctionUse != 2)
				shots++;
			if (shots > 4)
				shots = 0;
			int shot;
			Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
			switch (shots)
            {
				case 0:
					shot = ProjectileType<FireProjectile>();
					Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), shot, damage, knockback, player.whoAmI);
					break;
				case 1:
					shot = ProjectileType<IceProjectile>();
					Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), shot, damage, knockback, player.whoAmI);
					break;
				case 2:
					shot = ProjectileType<CursedFlamesProjectile>();
					Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), shot, damage, knockback, player.whoAmI);
					break;
				case 3:
					shot = ProjectileType<IchorProjectile>();
					Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), shot, damage, knockback, player.whoAmI);
					break;
				case 4:
					shot = ProjectileType<VenomProjectile>();
					Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), shot, damage, knockback, player.whoAmI);
					break;
			}

			int dustIndex = Dust.NewDust(Item.Center, Item.width, Item.height, DustID.WitherLightning, 0f, 0f, 100, default, 1f);
			Main.dust[dustIndex].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
			Main.dust[dustIndex].noGravity = true;

			return false;
		}

		//Old Randomised shooting
		/*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			type = Main.rand.Next(new int[] { type, ProjectileType<IceProjectile>(), ProjectileType<VenomProjectile>(), ProjectileType<IchorProjectile>(), ProjectileType<CursedFlamesProjectile>() });
		}*/

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<DebuffGun>());
            recipe.AddIngredient(ItemID.FlareGun);
            recipe.AddIngredient(ItemID.LunarBar, 10);
			recipe.AddIngredient(ItemID.FragmentVortex, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}

    public class DebuffGun2 : ModItem
    {
        int shots = 0;

        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("Debuff Gun Mk. 2");
            Tooltip.SetDefault("The power of the Elements" +
                "\nRight click to keep the same projectile type");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 110;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 72;
            Item.height = 20;
            Item.useTime = 15; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = 15; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTurn = true;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 4;
            Item.value = 10000; // how much the item sells for (measured in copper)
            Item.rare = RarityType<GalacticRarity>();
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true; //Autofire
            Item.shootSpeed = 16f;
            Item.shoot = ProjectileType<FireProjectile>();
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
        }

        public override bool AltFunctionUse(Player player) => true;

		public override bool CanUseItem(Player player) => true;

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2)
                shots++;
            if (shots > 4)
                shots = 0;
            int shot = 0;
            Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(0));
            switch (shots)
            {
                case 0:
                    shot = ProjectileType<FireProjectile>();
                    break;
                case 1:
                    shot = ProjectileType<IceProjectile>();
                    break;
                case 2:
                    shot = ProjectileType<CursedFlamesProjectile>();
                    break;
                case 3:
                    shot = ProjectileType<IchorProjectile>();
                    break;
                case 4:
                    shot = ProjectileType<VenomProjectile>();
                    break;
            }

            int numberProjectiles = 15; //This defines how many projectiles to shot.
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed2 = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(15)); // This defines the projectiles random spread; 5 degree spread.
                Projectile.NewProjectile(source, new Vector2(position.X, position.Y), new Vector2(perturbedSpeed2.X, perturbedSpeed2.Y), shot, damage, knockback, player.whoAmI);
            }
            SoundEngine.PlaySound(SoundID.Item36 with { Volume = 1f, Pitch = 0.5f }, player.Center);

            int dustIndex = Dust.NewDust(Item.Center, Item.width, Item.height, DustID.WitherLightning, 0f, 0f, 100, default, 1f);
            Main.dust[dustIndex].scale = 1f + (float)Main.rand.Next(5) * 0.1f;
            Main.dust[dustIndex].noGravity = true;

            return false;
        }
    }
}