using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacitcMod.Items;

namespace GalacticMod.Items.Hardmode.Asteroid
{
    [AutoloadEquip(EquipType.Head)]
    public class AsteroidHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% Increased damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.LightRed;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 12;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<AsteroidHelmet>());
            recipe.AddIngredient(Mod, "AsteroidBar", 25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<AsteroidChestplate>() && legs.type == ModContent.ItemType<AsteroidLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "10% increased damage" +
                "\nIncreased critical strike chance";
            player.GetDamage(DamageClass.Generic) += 0.10f;
            player.GetCritChance(DamageClass.Generic) += 6;
        }

        public override void ArmorSetShadows(Player player)
        {
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.Torch, player.velocity.X * 1f, player.velocity.Y * 1f, 130, default, 1.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.5f;;
            }
            player.armorEffectDrawShadow = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.10f;
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class AsteroidChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased Critical Strike Chance by 2");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.LightRed;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 15;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.GetCritChance(DamageClass.Generic) += 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<AsteroidChestplate>());
            recipe.AddIngredient(Mod, "AsteroidBar", 35);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();

        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class AsteroidLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% increased movement speed");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.LightRed;
            Item.width = 26;
            Item.height = 16;
            Item.defense = 12;

            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;
        }

        public override void UpdateEquip(Player Player)
        {
            Player.moveSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<AsteroidLeggings>());
            recipe.AddIngredient(Mod, "AsteroidBar", 30);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}