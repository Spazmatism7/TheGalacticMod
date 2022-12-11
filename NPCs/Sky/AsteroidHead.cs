using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Items.Hardmode.Asteroid;
using Terraria.ModLoader.Utilities;

namespace GalacticMod.NPCs.Sky
{
    public class AsteroidHead : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 7;

            NPC.width = 26;
            NPC.height = 26;

            NPC.aiStyle = 74;
            AIType = NPCID.SolarCorite;
            AnimationType = NPCID.FlyingSnake;

            NPC.lifeMax = 500;
            NPC.damage = 80;
            NPC.defense = 38;
            NPC.knockBackResist = 80f;

            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath43;
            NPC.value = Item.buyPrice(0, 0, 25, 0);

            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.OnFire3] = true;
            NPC.buffImmune[BuffID.Confused] = true;
        }

        public override bool? CanFallThroughPlatforms() => true;

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;
            float distanceX = player.Center.X - NPC.Center.X;
            float distanceY = player.Center.Y - NPC.Center.Y;
            float distance = (float)System.Math.Sqrt((double)(distanceX * distanceX + distanceY * distanceY));
            NPC.velocity.X *= 0.95f;
            NPC.velocity.Y *= 0.95f;

            NPC.spriteDirection = NPC.direction;

            NPC.TargetClosest();

            NPC.spriteDirection = NPC.direction;

            if (Main.rand.NextBool(4))
            {
                var dust2 = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.YellowStarDust);
                dust2.noGravity = true;
                dust2.scale = 1.5f;
                dust2.velocity *= 2;
            }

            if (!Main.dedServ)
            {
                Lighting.AddLight(NPC.Center, Color.WhiteSmoke.ToVector3() * 0.5f * Main.essScale);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Meteor,

                new FlavorTextBestiaryInfoElement("Some Meteor Heads are infected with the ancient spirits of darkness, causing them to have great power.")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)].ZoneMeteor && Main.hardMode)
                return SpawnCondition.Meteor.Chance * 1f;            
            else
                return SpawnCondition.Meteor.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<AsteroidBar>(), 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Meteorite, 5, 1, 2));
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit) => target.AddBuff(BuffType<Buffs.AsteroidBlaze>(), 7 * 60);
    }

    public class AsteroidBlitzer : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 7;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 7;

            NPC.width = 26;
            NPC.height = 26;

            NPC.aiStyle = 74;
            AIType = NPCID.SolarCorite;
            AnimationType = NPCID.FlyingSnake;

            NPC.lifeMax = 200;
            NPC.damage = 95;
            NPC.defense = 20;
            NPC.knockBackResist = 100f;

            NPC.lavaImmune = true;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath43;
            NPC.value = Item.buyPrice(0, 0, 25, 0);

            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.OnFire3] = true;
            NPC.buffImmune[BuffID.Confused] = true;
        }

        public override bool? CanFallThroughPlatforms() => true;

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            Vector2 target = NPC.HasPlayerTarget ? player.Center : Main.npc[NPC.target].Center;
            float distanceX = player.Center.X - NPC.Center.X;
            float distanceY = player.Center.Y - NPC.Center.Y;
            float distance = (float)System.Math.Sqrt((double)(distanceX * distanceX + distanceY * distanceY));
            NPC.velocity.X *= 1.02f;
            NPC.velocity.Y *= 1.02f;

            NPC.spriteDirection = NPC.direction;

            NPC.TargetClosest();

            NPC.spriteDirection = NPC.direction;

            if (Main.rand.NextBool(4))
            {
                var dust2 = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.YellowStarDust);
                dust2.noGravity = true;
                dust2.scale = 1.5f;
                dust2.velocity *= 2;
            }

            if (!Main.dedServ)
            {
                Lighting.AddLight(NPC.Center, Color.WhiteSmoke.ToVector3() * 0.5f * Main.essScale);
            }
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Meteor,

                new FlavorTextBestiaryInfoElement("Some Meteor Heads are infected with the ancient spirits of darkness, causing them to have great power.")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)].ZoneMeteor && Main.hardMode)
                return SpawnCondition.Meteor.Chance * 1f;
            else
                return SpawnCondition.Meteor.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<AsteroidBar>(), 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.Meteorite, 5, 1, 2));
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit) => target.AddBuff(BuffType<Buffs.AsteroidBlaze>(), 7 * 60);
    }
}
