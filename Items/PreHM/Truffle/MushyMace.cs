using GalacticMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace GalacticMod.Items.PreHM.Truffle
{
	public class MushyMace : ModItem
	{
		public override	void SetStaticDefaults()
        {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            SacrificeTotal = 1;

            // This line will make the damage shown in the tooltip twice the actual Item.damage. This multiplier is used to adjust for the dynamic damage capabilities of the projectile.
            // When thrown directly at enemies, the flail projectile will deal double Item.damage, matching the tooltip, but deals normal damage in other modes.
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
        }

		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 34;
			Item.value = 15000; // how much the item sells for (measured in copper)
			Item.rare = ItemRarityID.Blue;
			Item.noMelee = true;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 40;
			Item.useTime = 40;
			Item.knockBack = 4f;
			Item.damage = 22;
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<MushyMaceProjectile>();
			Item.shootSpeed = 12f;
			Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.crit = 9;
			Item.channel = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<MushyMace>());
			recipe.AddIngredient(ItemID.Chain);
			recipe.AddIngredient(ItemID.GlowingMushroom, 30);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}