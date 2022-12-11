using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using GalacticMod.Buffs;

namespace GalacticMod.Items.Hardmode.Frost
{
	public class FrostSickle : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Inflicts Frostbite");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 48;
			Item.DamageType = DamageClass.Melee; //melee weapon
			Item.width = 58;
			Item.height = 64;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 1000; //sell value
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true; //autoswing
			Item.useTurn = true; //player can turn while animation is happening
		}

		public override void AddRecipes()
		{
			Recipe recipe = Recipe.Create(ItemType<FrostSickle>());
			recipe.AddIngredient(Mod, "FrostBar", 17);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                // Emit dusts when the sword is swung
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Frost, 0, 0, 0, default, 0.5f);
            }
        }

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            target.AddBuff(BuffID.Frostburn2, 300);
        }

		public override void ModifyHitPvp(Player player, Player target, ref int damage, ref bool crit)
        {
            target.AddBuff(BuffID.Frostburn2, 300);
        }
    }
}