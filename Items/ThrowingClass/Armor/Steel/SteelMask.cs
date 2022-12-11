using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using GalacticMod.Assets.Systems;
using GalacitcMod.Items;

namespace GalacticMod.Items.ThrowingClass.Armor.Steel
{
    [AutoloadEquip(EquipType.Head)]
    public class SteelMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Steel Mask");
            Tooltip.SetDefault("10% Increased Throwing Damage");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Pink;

            Item.width = 26;
            Item.height = 16;
            ModContent.GetInstance<RarityToCostArmor>().modArmor = true;

            Item.defense = 16;
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ModContent.ItemType<SteelMask>());
            recipe.AddIngredient(Mod, "SteelBar", 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SteelChestplate>() && legs.type == ModContent.ItemType<SteelBoots>();
        }

        public override void UpdateArmorSet(Player Player)
        {
            Player.setBonus = "Critical hits grant Ammo Box abilities";
            Player.GetModPlayer<GalacticPlayer>().SteelBonus = true;
        }

        public override void UpdateEquip(Player Player)
        {
            //player.thrownVelocity += 0.2f;
            Player.GetDamage(DamageClass.Throwing) += 0.1f;
        }
    }
}