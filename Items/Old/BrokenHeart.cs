using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Items.Old
{
    public class BrokenHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A Relic of a Lost Age");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.width = 30;
            Item.height = 30;

            Item.shoot = ProjectileType<Cupid>(); // "Shoot" your pet projectile.
            Item.buffType = BuffType<PetCupid>(); // Apply buff upon usage of the Item.
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<BrokenHeart>());
            recipe.AddIngredient(ItemID.Amethyst);
            recipe.AddIngredient(ItemID.Topaz);
            recipe.AddIngredient(ItemID.Sapphire);
            recipe.AddIngredient(ItemID.Emerald);
            recipe.AddIngredient(ItemID.Ruby);
            recipe.AddIngredient(ItemID.Amber);
            recipe.AddIngredient(ItemID.Diamond);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    public class PetCupid : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pet Cupid");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) //This method gets called every frame your buff is active on your player.
        { 
            player.buffTime[buffIndex] = 18000;

            int projType = ProjectileType<Cupid>();

            // If the player is local, and there hasn't been a pet projectile spawned yet - spawn it.
            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
            {
                var entitySource = player.GetSource_Buff(buffIndex);

                Projectile.NewProjectile(entitySource, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
            }
        }
    }

    public class Cupid : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ZephyrFish); // Copy the stats of the Zephyr Fish
            Projectile.height = 50;
            Projectile.width = 50;

            AIType = ProjectileID.ZephyrFish; // Copy the AI of the Zephyr Fish.
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];

            player.zephyrfish = false; // Relic from aiType

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
            if (!player.dead && player.HasBuff(BuffType<PetCupid>()))
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}