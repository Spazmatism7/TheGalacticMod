using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GalacticMod.Assets.Systems;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Rarities;
using GalacticMod.Projectiles;
using Mono.Cecil;
using Terraria.Audio;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace GalacticMod.Items.PostML.Hellfire
{
    [AutoloadEquip(EquipType.Head)]
    public class HellflameHelmet : ModItem
    {
        int cooldown;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Increases Damage by 20%" +
                "\nIncreases life regen by 4" +
                "\nIncreases Minion capacity by 2");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ModContent.RarityType<HellfireRarity>();
            Item.width = 28;
            Item.height = 30;

            Item.defense = 26;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<HellflameHelmet>());
            recipe.AddIngredient(Mod, "HellfireBar", 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<HellflameChestplate>() && legs.type == ModContent.ItemType<HellflameGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            var list = GalacticMod.ArmourSpecialHotkey.GetAssignedKeys();
            string keyName = "(Not bound, set in controls)";

            if (list.Count > 0)
            {
                keyName = list[0];
            }

            player.setBonus = "Press '" + keyName + "' to create an explosion at the cursor" +
                "\nCannot be set on fire";
            player.buffImmune[24] = true;

            if (GalacticMod.ArmourSpecialHotkey.JustPressed && cooldown <= 0)
            {
                cooldown = 1 * 60;
                Vector2 mousePosition = Main.MouseWorld;
                SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);

                Projectile.NewProjectile(null, mousePosition, new Vector2(0), ModContent.ProjectileType<HellflameArmorProj>(), 250, 10f, player.whoAmI);
            }
            else
                cooldown--;
        }

        public override void ArmorSetShadows(Player player)
        {
            if (Main.rand.NextBool(2))
            {
                int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.Flare, player.velocity.X * 1f, player.velocity.Y * 1f, 130, default, 1.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.5f;
                int dust2 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.Pixie, player.velocity.X, player.velocity.Y, 130, default, 0.5f);
            }
            player.armorEffectDrawShadowLokis = true;
            player.GetModPlayer<GalacticPlayer>().Smoke = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.2f;
            player.lifeRegen += 4;
            player.maxMinions += 2;
        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Body)]
    public class HellflameChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hellflame Chestplate");
            Tooltip.SetDefault("Increases Health by 100" +
                "\nIncreases maximum mana by 100" +
                "\nIncreases Minion capacity by 1");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ModContent.RarityType<HellfireRarity>();

            Item.defense = 33;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 100;
            player.statManaMax2 += 100;
            player.maxMinions++;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<HellflameChestplate>());
            recipe.AddIngredient(Mod, "HellfireBar", 26);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();

        }
    }

    //____________________________________________________________________________________________________________
    [AutoloadEquip(EquipType.Legs)]
    public class HellflameGreaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Tooltip.SetDefault("Increased Critical Strike chance by 6" +
                "\n10% Increased movement speed" +
                "\nIncreases Minion capacity by 2");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 7);
            Item.rare = ModContent.RarityType<HellfireRarity>();

            Item.defense = 25;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 6;
            player.maxMinions += 2;
            player.moveSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<HellflameGreaves>());
            recipe.AddIngredient(Mod, "HellfireBar", 23);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}