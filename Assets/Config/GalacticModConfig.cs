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
    [Label("Configurations")]
    public class GalacticModConfig : ModConfig //configuration settings
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Gameplay")]

        [DefaultValue(false)]
        [Label("Disable Ore Generation")]
        [Tooltip("This will prevent the ores in this mod from generating, Requires a Reload")]
        [ReloadRequired]
        public bool PreventOreSpawn { get; set; }

        [DefaultValue(false)]
        [Label("No Alt Slimes")]
        [Tooltip("Modded alternate slimes won't spawn")]
        public bool NoWeirdoBlobs { get; set; }

        [DefaultValue(false)]
        [Label("Disable Vanilla Throwing Revertion")]
        [Tooltip("Vanilla thrown weapons stay ranged and Boomerangs will stay melee, Requires a Reload")]
        [ReloadRequired]
        public bool NoVThrown { get; set; }

        [Header("Special Features")]

        [DefaultValue(20)]
        [Label("Boss Summon Max Stack Count")]
        [Tooltip("Adjusts the Max Stack of boss summoning items")]
        [Increment(5)]
        [Range(1, 100)]
        [Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
        public int BossSummonStackCount;

        [DefaultValue(20)]
        [Label("Voodoo Doll Max Stack Count")]
        [Tooltip("Adjusts the Max Stack of Voodoo Dolls")]
        [Increment(20)]
        [Range(1, 100)]
        [Slider] // The Slider attribute makes this field be presented with a slider rather than a text input. The default ticks is 1.
        public int VoodooDollStackCount;

        [DefaultValue(false)]
        [Label("Starting Bag")]
        [Tooltip("Spawn with a starting bag")]
        public bool StartBag { get; set; }
    }
}