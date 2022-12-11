using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;
using GalacitcMod.Items;

namespace GalacticMod.Items.PreHM.Goblin
{
    [AutoloadEquip(EquipType.Head)]
    public class TheifsBandana : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin Thief's Bandana");
            Tooltip.SetDefault("10% increased movement speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 24;
            Item.height = 20;
            Item.defense = 4;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<TheifsVest>() && legs.type == ItemType<TheifsPants>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased throwing damage" +
                "\n10% increased movement speed";
            player.GetDamage(DamageClass.Throwing) += 0.1f;
            player.moveSpeed += 0.1f;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.moveSpeed += 0.1f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class TheifsVest : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin Thief's Vest");
            Tooltip.SetDefault("10% increased movement speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 5;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.moveSpeed += 0.1f;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class TheifsPants : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin Thief's Pants");
            Tooltip.SetDefault("20% increased movement speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 4;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.moveSpeed += 0.2f;
        }
    }

    public class GoblinThiefSet : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.GoblinThief)
            {
                npcLoot.Add(ItemDropRule.OneFromOptions(20, ItemType<TheifsBandana>(), ItemType<TheifsVest>(), ItemType<TheifsPants>()));
                npcLoot.Add(ItemDropRule.Common(ItemType<GoblinKnife>(), 20));
            }
        }
    }

    internal class GoblinKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.rare = ItemRarityID.Green;
            Item.width = 18;
            Item.height = 20;
            Item.useTime = 22;
            Item.UseSound = SoundID.Item13;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 14f;
            Item.useAnimation = 20;
            Item.shoot = ProjectileType<GoblinKnifeProj>();
            Item.shootSpeed = 8f;
            Item.DamageType = DamageClass.Throwing;

            // Alter any of these values as you see fit, but you should probably keep useStyle on 1, as well as the noUseGraphic and noMelee bools
            Item.noUseGraphic = true;
            Item.consumable = false;
            Item.maxStack = 1;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(silver: 5);
        }
    }

    public class GoblinKnifeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Goblin Knife");
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 600;
            Projectile.DamageType = DamageClass.Throwing;
        }

        public override void AI()
        {
            Projectile.rotation += 1.57f / 6;
            Projectile.velocity.Y += .1f;
        }
    }
}