using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.Utilities;
using GalacticMod.Items.Accessories;

namespace GalacticMod.NPCs
{
    public class GraniteCore : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            Main.npcFrameCount[NPC.type] = 8;

            NPC.width = 22;
            NPC.height = 24;

            NPC.aiStyle = 5;
            AIType = NPCID.Parrot;
            NPC.noGravity = true;

            NPC.lifeMax = 400;
            NPC.damage = 70;
            NPC.defense = 10;
            NPC.knockBackResist = 1f;

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
            float distance = (float)Math.Sqrt((double)(distanceX * distanceX + distanceY * distanceY));

            NPC.spriteDirection = NPC.direction;

            NPC.TargetClosest();

            NPC.spriteDirection = NPC.direction;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,

                new FlavorTextBestiaryInfoElement("When Granite Elementals collide, their bodies warp together into a Granite Core")
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.player[Player.FindClosest(NPC.position, NPC.width, NPC.height)].ZoneGranite && Main.hardMode)
                return SpawnCondition.Sky.Chance * 1f;
            else
                return SpawnCondition.Sky.Chance * 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemType<GraniteCoreItem>(), 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.Granite, 1, 5, 10));
            npcLoot.Add(ItemDropRule.Common(ItemID.NightVisionHelmet, 30));
            npcLoot.Add(ItemDropRule.Common(ItemID.Spaghetti, 50));
            npcLoot.Add(ItemDropRule.Common(ItemID.Geode, 20));
        }

        int npcframe = 0;
        public override void FindFrame(int frameHeight)//Animations
        {
            NPC.frame.Y = npcframe * frameHeight;

            //NPC.spriteDirection = NPC.direction;
            NPC.frameCounter++;
            if (NPC.frameCounter > 8)
            {
                npcframe++;
                NPC.frameCounter = 0;
            }
            if (npcframe >= 8)
            {
                npcframe = 0;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = (Texture2D)Mod.Assets.Request<Texture2D>("NPCs/GraniteCore_Glow");
            Vector2 drawPos = NPC.Center - Main.screenPosition;

            spriteBatch.Draw(texture, drawPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() / 2f, NPC.scale, NPC.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_14").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_14").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_15").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_15").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                }
            }
        }
    }
}
