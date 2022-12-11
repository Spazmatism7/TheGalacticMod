using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.Bestiary;
using System.Collections.Generic;
using GalacticMod.Items.PostML.Hellfire;
using GalacticMod.Projectiles.Boss;

using Terraria.GameContent.ItemDropRules;
using GalacticMod.Projectiles;

namespace GalacticMod.NPCs.CavernUnderworld
{
	public class BetsyBreathDragon : ModNPC
	{
        int aiType = 0;
        int timer;

        private int hoverHeight = 350;
        private int hoverWidth = 500;
        private int speedSlow = 20;
        private int speedFast = 25;
        private bool left = true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Baby Dragon");
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
		{
            Main.npcFrameCount[NPC.type] = 5;
            AnimationType = NPCID.FlyingSnake;

            NPC.width = 84;
            NPC.height = 96;
            NPC.damage = 30;
            NPC.defense = 10;
            NPC.lifeMax = 200;
            NPC.lavaImmune = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
            if (Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)].ZoneUnderworldHeight)
            {
                return 0.5f;
            }
            else
            {
                return 0f;
            }
        }

        public override void AI()
        {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;
            NPC.rotation = 0.0f;
            NPC.netAlways = true;
            NPC.TargetClosest(true);
            NPC.spriteDirection = NPC.direction;

            //Immunities
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.OnFire] = true;

            int distance = (int)Vector2.Distance(target, NPC.Center);
            timer++;

            switch (timer)
            {
                case < 60:
                    aiType = 0;
                    break;
                case > 60 and < 180:
                    aiType = 1;
                    break;
                case > 180 and < 300:
                    aiType = 2;
                    break;
                case > 300:
                    timer = 0;
                    aiType = 0;
                    break;
                default:
                    aiType = 0;
                    break;
            }

            if (aiType == 0)
            {
                if (Main.player[NPC.target].position.X < NPC.position.X)
                {
                    if (NPC.velocity.X > -8) NPC.velocity.X -= 0.22f;
                }

                if (Main.player[NPC.target].position.X > NPC.position.X)
                {
                    if (NPC.velocity.X < 8) NPC.velocity.X += 0.22f;
                }

                if (Main.player[NPC.target].position.Y < NPC.position.Y + 300)
                {
                    if (NPC.velocity.Y < 0) // <--
                    {
                        if (NPC.velocity.Y > -4) NPC.velocity.Y -= 0.7f;
                    }
                    else NPC.velocity.Y -= 0.8f;
                }

                if (Main.player[NPC.target].position.Y > NPC.position.Y + 300)
                {
                    if (NPC.velocity.Y > 0) // <--
                    {
                        if (NPC.velocity.Y < 4) NPC.velocity.Y += 0.7f;
                    }
                    else NPC.velocity.Y += 0.8f;
                }
            }

            if (aiType == 1)
            {
                target.Y -= hoverHeight;
                target.X -= (left ? -hoverWidth : hoverWidth);
                MoveTowards(NPC, target, (distance > 300 ? speedFast : speedSlow), 30f);
            }

            if (aiType == 2)
            {
                target.Y -= hoverHeight;
                target.X -= (left ? hoverWidth : hoverWidth);
                MoveTowards(NPC, target, (distance > 300 ? speedFast : speedSlow), 30f);
            }

            NPC.ai[0]++;
        }

        private void MoveTowards(NPC npc, Vector2 target, float speed, float turnResistance)
        {
            var move = target - npc.Center;
            float length = move.Length();
            if (length > speed)
            {
                move *= speed / length;
            }
            move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
            length = move.Length();
            if (length > speed)
            {
                move *= speed / length;
            }
            npc.velocity = move;
        }
    }
}