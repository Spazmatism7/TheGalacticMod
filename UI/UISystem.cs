using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Input;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.UI;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.IO;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Assets.Systems;

namespace GalacticMod.UI
{
    class UI : ModSystem
    {
        private UserInterface _osmiumSoulBarUserInterface;
        internal OsmiumSoulBar OsmiumSoulBar;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                _osmiumSoulBarUserInterface = new UserInterface();
                _osmiumSoulBarUserInterface.SetState(OsmiumSoulBar);

                OsmiumSoulBar = new OsmiumSoulBar();
                OsmiumSoulBar.Activate(); // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
            }
        }

        public override void Unload()
        {
            //OsmiumSoulBar?.Unload(); // If you hold data that needs to be unloaded, call it in OO-fashion
            OsmiumSoulBar = null;
        }

        private GameTime _lastUpdateUiGameTime;

        public override void UpdateUI(GameTime time) //change to override
        {
            _lastUpdateUiGameTime = time;
            if (_osmiumSoulBarUserInterface?.CurrentState != null)
            {
                _osmiumSoulBarUserInterface.Update(time);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) //change to override
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "GalacticMod: Osmium Soul Bar",
                    delegate
                    {
                        if (_osmiumSoulBarUserInterface != null && _osmiumSoulBarUserInterface?.CurrentState != null)
                        {
                            _osmiumSoulBarUserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
            }
        }
    }
}