using GalacticMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using static Terraria.ModLoader.ModContent;

namespace GalacticMod.NPCs.CavernUnderworld
{
    public class FireSpitter : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire-Spitter");
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
            AnimationType = NPCID.FlyingSnake;

            NPC.width = 70;
            NPC.height = 96;
            NPC.damage = 20;
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
            if (!NPC.AnyNPCs(NPCType<FireSpitter>()))
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

            NPC.ai[0]++;

            if (NPC.ai[0] >= 20)
            {
                float projectileSpeed = 10f;
                Vector2 velocity = Vector2.Normalize(new Vector2(player.Center.X, player.Center.Y) - new Vector2(NPC.Center.X, NPC.Center.Y)) * projectileSpeed;
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10));

                Projectile.NewProjectile(null, new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(perturbedSpeed.X, perturbedSpeed.Y), ProjectileType<Fireball2>(), NPC.damage, .5f, 0);
                //NPC.GetSpawnSource_ForProjectile()
                SoundEngine.PlaySound(SoundID.Item34, NPC.Center);

                NPC.ai[0] = 0;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_6").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_10").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_8").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                }
            }
        }
    }
}