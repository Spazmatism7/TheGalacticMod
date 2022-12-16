using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Items.Boss.Master
{
    public class MysteriousCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.DD2BetsyPetItem);
            Item.width = 30;
            Item.height = 30;

            Item.shoot = ProjectileType<GalacticPerilPet>(); // "Shoot" your pet projectile.
            Item.buffType = BuffType<MysteriousCrystalBuff>(); // Apply buff upon usage of the Item.
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }
    }

    public class MysteriousCrystalBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Destroyer of Very Small Universes");
            Description.SetDefault("*Hissing noises*");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) //This method gets called every frame your buff is active on your player.
        {
            player.buffTime[buffIndex] = 18000;

            int projType = ProjectileType<GalacticPerilPet>();

            // If the player is local, and there hasn't been a pet projectile spawned yet - spawn it.
            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
            {
                var entitySource = player.GetSource_Buff(buffIndex);

                Projectile.NewProjectile(entitySource, player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
            }
        }
    }

    public class GalacticPerilPet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.DD2BetsyPet); // Copy the stats of the Zephyr Fish
            Projectile.height = 50;
            Projectile.width = 50;

            AIType = ProjectileID.DD2BetsyPet; // Copy the AI of the Zephyr Fish.
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];

            player.petFlagDD2BetsyPet = false; // Relic from aiType

            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
            if (!player.dead && player.HasBuff(BuffType<HellBabyBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}