using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace GalacticMod.Items.Old
{
    [AutoloadEquip(EquipType.Head)]
    public class MythicalLionMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythical Lion Mask");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            int width = 30; int height = 18;
            Item.Size = new Vector2(width, height);

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(silver: 70);
            Item.vanity = true;
        }
    }

    [AutoloadEquip(EquipType.Body)]
	public class MythicalRobe : ModItem
	{
		public override void Load() {
			string robeTexture = "GalacticMod/Items/Old/MythicalRobe_Extension";
			if (Main.netMode != NetmodeID.Server)
				EquipLoader.AddEquipTexture(Mod, robeTexture, EquipType.Legs, this);
		}

		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Mythical Robe");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults() {
			int width = 30; int height = 18;
			Item.Size = new Vector2(width, height);

			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(silver: 60);
			Item.vanity = true;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes) {
			var robeSlot = ModContent.GetInstance<MythicalRobe>();
			equipSlot = EquipLoader.GetEquipSlot(Mod, robeSlot.Name, EquipType.Legs);
			robes = true;
		}
	}
}
