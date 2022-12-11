using GalacticMod.Buffs;
using GalacticMod.Items;
using GalacticMod.NPCs;
using GalacticMod.NPCs.Bosses;
using GalacticMod.Projectiles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.GameContent.Drawing;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace GalacticMod.Assets.Systems
{
    public class GalacticProjectile : GlobalProjectile
    {
        public void vanadiumHeal(int damage, Vector2 Position, Entity victim, Projectile projectile)
        {
            float num = 0.2f;
            num -= projectile.numHits * 0.05f;
            if (num <= 0f)
            {
                return;
            }
            float num2 = damage * num;
            if ((int)num2 <= 0 || Main.player[Main.myPlayer].lifeSteal <= 0f)
            {
                return;
            }
            Main.player[Main.myPlayer].lifeSteal -= num2;
            float num3 = 0f;
            int num4 = projectile.owner;
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active && !Main.player[i].dead && ((!Main.player[projectile.owner].hostile && !Main.player[i].hostile) || Main.player[projectile.owner].team == 
                    Main.player[i].team) && Math.Abs(Main.player[i].position.X + (Main.player[i].width / 2) - projectile.position.X + (projectile.width / 2)) + 
                    Math.Abs(Main.player[i].position.Y + (Main.player[i].height / 2) - projectile.position.Y + (projectile.height / 2)) < 1200f && (Main.player[i].statLifeMax2 - 
                    Main.player[i].statLife) > num3)
                {
                    num3 = Main.player[i].statLifeMax2 - Main.player[i].statLife;
                    num4 = i;
                }
            }
            Projectile.NewProjectile(null, Position.X, Position.Y, 0f, 0f, ProjectileID.SpiritHeal, 0, 0f, projectile.owner, num4, num2);
        }
    }
}