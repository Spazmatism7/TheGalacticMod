using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Creative;
using GalacticMod.Buffs;
using Microsoft.Xna.Framework;

namespace GalacticMod.Items.PreHM.Truffle
{
	public class TruffleSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Infests enemies with a glowing parasite");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 18;
			Item.DamageType = DamageClass.Melee; //melee weapon
			Item.width = 51;
			Item.height = 51;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000; // how much the item sells for (measured in copper)
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ModContent.ItemType<TruffleSword>());
			recipe.AddIngredient(ItemID.GlowingMushroom, 30);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3)) // Emit dusts when the sword is swung
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.DungeonWater, 0f, 0f, 150, default(Color), 1.3f);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<Infested>(), 240);
		}

		public override void OnHitPvp(Player player, Player target, int damage, bool crit)
		{
            target.AddBuff(ModContent.BuffType<Infested>(), 240);
        }
	}
}