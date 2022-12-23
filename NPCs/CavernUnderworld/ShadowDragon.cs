using GalacticMod.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.Utilities;

namespace GalacticMod.NPCs.CavernUnderworld
{
    public class ShadowDragon : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Dragon");
            Main.npcFrameCount[NPC.type] = 5;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 5;
            AnimationType = NPCID.FlyingSnake;

            NPC.width = 70;
            NPC.height = 88;
            NPC.damage = 70;
            NPC.defense = 22;
            NPC.lifeMax = 1000;
            NPC.lavaImmune = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (NPC.downedMoonlord)
            {
                return SpawnCondition.Underworld.Chance * 0.1f;
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

            //Attack
            NPCUtils.TargetClosestBetsy(NPC, faceTarget: false);
            NPCAimedTarget targetData = NPC.GetTargetData();

            float num12 = 12f;
            float num13 = 40f;
            float num14 = 80f;
            float num15 = num13 + num14;

            NPC.ai[3]++;

            NPC.ai[1] += 1f;
            int num33 = (NPC.Center.X < targetData.Center.X) ? 1 : (-1);
            NPC.ai[2] = num33;
            if (NPC.ai[1] < num13)
            {
                Vector2 vector4 = targetData.Center + new Vector2(num33 * (0f - 450f), -150);
                Vector2 vector5 = NPC.DirectionTo(vector4) * num12;
                if (NPC.Distance(vector4) < num12)
                {
                    NPC.Center = vector4;
                }
                else
                {
                    NPC.position += vector5;
                }
                if (Vector2.Distance(vector4, NPC.Center) < 16f)
                {
                    NPC.ai[1] = num13 - 1f;
                }
            }
            if (NPC.ai[3] >= 3)
            {
                int num34 = (targetData.Center.X > NPC.Center.X) ? 1 : (-1);
                Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * 5f;
                NPC.direction = NPC.spriteDirection = num34;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(null, NPC.Center, velocity, ModContent.ProjectileType<ShadeBreath>(), NPC.damage, 0f, Main.myPlayer, 0f, NPC.whoAmI);
                }

                NPC.ai[3] = 0f;
            }
            if (NPC.ai[1] >= num13)
            {
                if (Math.Abs(targetData.Center.X - NPC.Center.X) > 550f && Math.Abs(NPC.velocity.X) < 20f)
                {
                    NPC.velocity.X += Math.Sign(NPC.velocity.X) * 0.5f;
                }
            }
            if (NPC.ai[1] >= num15)
            {
                NPC.ai[0] = 1f;
                NPC.ai[1] = 0f;
                NPC.ai[2] = 0f;
            }
        }
    }
}