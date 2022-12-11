using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Desert
{
    public class Sandstorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 37;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;

            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
            Item.value = 9000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.noUseGraphic = true;
            Item.width = 36;
            Item.height = 36;

            Item.autoReuse = true;
            Item.shoot = ProjectileType<SandstormP>();
            Item.shootSpeed = 20f;
            Item.channel = true;
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class SandstormP : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.aiStyle = 3;
            Projectile.tileCollide = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstorm");
        }
    }
}