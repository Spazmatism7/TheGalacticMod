using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.Hardmode.Ancient
{
    public class CultistTome : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tome of Ancient Light");
            Tooltip.SetDefault("Summons bolts of Ancient Light");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 15;
            Item.UseSound = SoundID.Item9;
            Item.damage = 70;
            Item.knockBack = 4f;
            Item.shoot = ModContent.ProjectileType<Projectiles.AncientLight>();
            Item.shootSpeed = 17f;
            Item.noMelee = true; //Does the weapon itself inflict damage?
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 20; i++)
            {
                int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.IceTorch, 0, 0, 120, default, 1.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 2.5f;
            }

            float numberProjectiles = 3 + Main.rand.Next(3); // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(45);
            position += Vector2.Normalize(velocity) * 45f;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            return false;
        }
    }

    class Lunatic : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.CultistBoss)
            {
                npcLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<CultistTome>(), ModContent.ItemType<AncientSpear>(), ModContent.ItemType<AncientBow>(), ModContent.ItemType<AncientCane>()));
                npcLoot.Add(ItemDropRule.OneFromOptions(3, ModContent.ItemType<AncientMask>(), ModContent.ItemType<AncientCuirass>(), ModContent.ItemType<AncientGreaves>()));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientBar>(), 1, 10, 16));
            }
        }
    }
}