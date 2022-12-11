using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using GalacticMod.Projectiles;
using Terraria.Audio;
using GalacticMod.Assets.Systems;

namespace GalacticMod.Items.Dev
{
    public class MurderWeapon : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 999999999;
            Item.knockBack = 999999999;
            Item.useAnimation = 1;
            Item.useTime = 1;

            Item.width = 36;
            Item.height = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.crit = 999999999;
            Item.shoot = ProjectileType<DeathLaserSupreme>();
            Item.shootSpeed = 50f;
        }
    }

    public class DeathLaserSupreme : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 2;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.light = 1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.Pi;
            }
        }
    }
}