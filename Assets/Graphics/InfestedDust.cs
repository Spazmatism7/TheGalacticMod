using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Creative;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.Assets.Graphics
{
	public class Infested : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			_ = dust.velocity.Y == 0;
			_ = dust.velocity.X == 0;
			_ = dust.scale == Main.rand.Next((int)0.75, 2);
		}

		public override bool MidUpdate(Dust dust)
		{
			if (!dust.noGravity)
			{
				dust.velocity.Y += 0.01f;
			}

			return true;
		}
	}
}