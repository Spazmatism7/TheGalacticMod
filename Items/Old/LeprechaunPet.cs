using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Items.Old
{
    public class PotofGold : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pot o' Gold");
            Tooltip.SetDefault("A Relic of a Lost Age");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish);
            Item.width = 30;
            Item.height = 30;

            Item.shoot = ProjectileType<Leprechaun>(); // "Shoot" your pet projectile.
            Item.buffType = BuffType<PetLeprechaun>(); // Apply buff upon usage of the Item.
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
            Recipe recipe = Recipe.Create(ItemType<PotofGold>());
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }

    public class PetLeprechaun : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pet Leprechaun o'Fyffe");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) //This method gets called every frame your buff is active on your player.
        {
            player.buffTime[buffIndex] = 18000;

            int projType = ProjectileType<Leprechaun>();

            // If the player is local, and there hasn't been a pet projectile spawned yet - spawn it.
            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
            {
                var entitySource = player.GetSource_Buff(buffIndex);

                Projectile.NewProjectile(entitySource, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
            }
        }
    }

    public class Leprechaun : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.BlackCat);
            Projectile.height = 48;
            Projectile.width = 30;

            AIType = ProjectileID.BlackCat;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];

            player.blackCat = false; // Relic from aiType

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
            if (!player.dead && player.HasBuff(BuffType<PetLeprechaun>()))
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}