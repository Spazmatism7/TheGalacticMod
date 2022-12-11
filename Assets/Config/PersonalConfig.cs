using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;


namespace GalacticMod.Assets.Config
{
    [BackgroundColor(5, 38, 48)] //13, 124, 158
    [Label("Personal Configurations")]
    public class PersonalConfig : ModConfig //configuration settings
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Sprite Settings")]

        [DefaultValue(false)]
        [ReloadRequired]
        [Label("$No Epilepsy")]
        [Tooltip("$Turns off item animations")]
        public bool NoEpilepsy;

        [DefaultValue(false)]
        [ReloadRequired]
        [Label("Reverts Amaza Sprites to the original Amaza sprites")]
        public bool ClassicAmaza;
    }
}