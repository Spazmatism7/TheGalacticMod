using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using GalacticMod.Projectiles;
using Terraria.GameContent.ItemDropRules;

namespace GalacticMod.Items.Hardmode.PostPlantera
{
	public class RockEdge : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Saxum");
			Tooltip.SetDefault("The Jewel of the Earth");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.height = 48;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.autoReuse = true;

			Item.DamageType = DamageClass.Melee;
			Item.damage = 100;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(gold: 5);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.shoot = ProjectileID.EnchantedBeam; // ID of the projectiles the sword will shoot
			Item.shootSpeed = 8f; // Speed of the projectiles the sword will shoot
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
			Vector2 heading = target - position;

			if (heading.Y < 0f)
			{
				heading.Y *= -1f;
			}

			if (heading.Y < 20f)
			{
				heading.Y = 20f;
			}

			heading.Normalize();
			heading *= velocity.Length();
			heading.Y += Main.rand.Next(-40, 41) * 0.02f;
			Projectile.NewProjectile(source, position, heading, ModContent.ProjectileType<RockBoulder>(), damage, knockback, player.whoAmI, 1f, ceilingLimit);

			return false;
		}
	}

	public class Golem : GlobalNPC
	{
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			if (npc.type == NPCID.Golem && !Main.expertMode)
			{
				if (Main.rand.NextBool(6))
				{
					npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RockEdge>()));
				}
			}
		}
	}
}