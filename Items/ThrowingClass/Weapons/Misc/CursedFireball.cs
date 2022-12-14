using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.ThrowingClass.Weapons.Misc
{
	internal class CursedFireball : ModItem
	{
		public override void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.rare = ItemRarityID.Pink;
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 17;
			Item.UseSound = SoundID.Item13;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shootSpeed = 8f;
			Item.useAnimation = 17;
			Item.shoot = ProjectileType<CFP>();
			Item.DamageType = DamageClass.Throwing;

			Item.noUseGraphic = true;
			Item.consumable = false;
			Item.noMelee = true;
			Item.autoReuse = true;

			Item.UseSound = SoundID.Item1;
			Item.value = Item.sellPrice(copper: 75);
		}
	}

	public class CFP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Fireball");
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.timeLeft /= 2;
			Projectile.penetrate = 8;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.light = 3f;
		}

        public override void AI()
        {
            Projectile.velocity.Y += Projectile.ai[0];
            int dust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.CursedTorch, Projectile.velocity.X, Projectile.velocity.Y, 130, default, 2f); 
            Main.dust[dust].noGravity = true; //this make so the dust has no gravity
            Main.dust[dust].velocity *= -0.3f;
        }

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item25, Projectile.position);
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffID.CursedInferno, 12000);
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			target.AddBuff(BuffID.CursedInferno, 12000);
		}
	}

	class Spazmatism : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.type == NPCID.Spazmatism)
			{
				npcLoot.Add(ItemDropRule.Common(ItemType<CursedFireball>(), 1));
			}
		}
	}
}
