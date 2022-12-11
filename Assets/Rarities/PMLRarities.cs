using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace GalacticMod.Assets.Rarities
{
	public class HellfireRarity : ModRarity
	{
		public override Color RarityColor => new Color(255, 64, 0);
	}

	public class GalacticRarity : ModRarity
	{
		public override Color RarityColor => new Color(68, 0, 255);
	}
}