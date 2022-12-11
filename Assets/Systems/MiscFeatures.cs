using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using Terraria.Audio;

namespace GalacticMod.Assets.Systems
{
    public class MiscFeatures : ModPlayer
    {
        public bool screenshaker; //Weapons that shake the screen
        public int shaketimer; //How long to shake the screen for

        //Proficiency
        public int proficiencyMelee;
        public int proficiencyRanged;
        public int proficiencyMagic;
        public int proficiencySummon;
        public int proficiencyThrown;

        public int playerimmunetime; //makes player immune to damage

        public override void ResetEffects() //Resets bools if the item is unequipped
        {
            screenshaker = false;
        }

        public override void UpdateDead()//Reset all ints and bools if dead======================
        {
            shaketimer = 0;
        }

        public override void ModifyScreenPosition()//screenshaker
        {
            if (screenshaker)
            {
                shaketimer = 10;
                screenshaker = false;
            }
            if (shaketimer > 0)
            {
                Main.screenPosition += new Vector2(Main.rand.Next(-7, 7), Main.rand.Next(-7, 7));
                shaketimer--;
            }
        }
    }
}