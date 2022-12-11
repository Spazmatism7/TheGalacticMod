using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;

namespace GalacticMod.Assets.Menu
{
	public class GalacticModMenu : ModMenu
	{
		private const string menuAssetPath = "GalacticMod/Assets/Menu"; // Creates a constant variable representing the texture path, so we don't have to write it out multiple times

		public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>($"{menuAssetPath}/Logo");

		public override Asset<Texture2D> SunTexture => base.SunTexture;

		public override Asset<Texture2D> MoonTexture => base.MoonTexture;

		public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Sounds/Music/A Long Journey");

		public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<GalacticBackgroundStyle>();

		public override string DisplayName => "Galactic Style";
	}
}