using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Bestiary;
using GalacticMod.Assets.Systems;
using GalacticMod.Items.Boss;
using GalacticMod.Items.PreHM.Nautilus;
using static Terraria.ModLoader.ModContent;
using GalacticMod.Items.Boss.Master;

namespace GalacticMod.NPCs.Bosses.PreHM
{
    [AutoloadBossHead]
    public class JormungandrHead : ModNPC
    {
        int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jörmungandr");
        }

        public override void SetDefaults()
        {
            NPC.width = 50; 
            NPC.height = 58;
            NPC.aiStyle = NPCAIStyleID.Worm;
            NPC.boss = true;
            NPC.damage = 200;
            NPC.defense = 4;
            NPC.lifeMax = 14000;
            NPC.knockBackResist = 0.0f;

            NPC.noGravity = true;
            NPC.noTileCollide = true;

            NPC.netAlways = true;
            NPC.alpha = 255;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath8;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
                new FlavorTextBestiaryInfoElement("")
            });
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            timer++;

            if (timer < 3 * 60)
            {
                NPC.immortal = true;
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.immortal = false;
                NPC.dontTakeDamage = false;
            }

            if (NPC.target < 0 || NPC.target == 250 || player.dead) 
                NPC.TargetClosest(true);
            if (player.dead && NPC.timeLeft > 300) 
                NPC.timeLeft = 300;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 0f)
                {
                    NPC.ai[3] = NPC.whoAmI;
                    NPC.realLife = NPC.whoAmI;
                    int num8 = NPC.whoAmI;
                    int numofSegments = 67;
                    if (Main.expertMode) numofSegments = 72;
                    for (int l = 0; l < numofSegments; l++)
                    {
                        int num9;
                        if (!Main.expertMode)
                        {
                            num9 = NPCType<JormungandrBody>();
                            switch (l)
                            {
                                case 66:
                                    num9 = NPCType<JormungandrTail>();
                                    break;
                            }
                        }
                        else
                        {
                            num9 = NPCType<JormungandrBody>();
                            switch (l)
                            {
                                case 0 or 2 or 4 or 6 or 8 or 10 or 12 or 14 or 16 or 18 or 20 or 22 or 24 or 26 or 28 or 30 or 32 or 34 or 36 or 38 or 40 or 42 or 44 or 46 or 48 or 50 or 52 or 54 or 56 or 58 or 60 or 62 or 64 or 66 or 68 or 70:
                                    num9 = NPCType<JormungandrBody_Spine>();
                                    break;
                                case 71:
                                    num9 = NPCType<JormungandrTail>();
                                    break;
                            }
                        }
                        int num7 = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.position.X + NPC.width / 2), (int)(NPC.position.Y + NPC.height), num9, NPC.whoAmI);
                        Main.npc[num7].ai[3] = NPC.whoAmI;
                        Main.npc[num7].realLife = NPC.whoAmI;
                        Main.npc[num7].ai[1] = num8;
                        Main.npc[num7].CopyInteractions(Main.npc[num8]);
                        Main.npc[num8].ai[0] = num7;
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num7);
                        num8 = num7;
                    }
                }
            }

            int num107 = (int)(NPC.position.X / 16f) - 1;
            int num108 = (int)((NPC.position.X + NPC.width) / 16f) + 2;
            int num109 = (int)(NPC.position.Y / 16f) - 1;
            int num110 = (int)((NPC.position.Y + NPC.height) / 16f) + 2;

            if (num107 < 0) num107 = 0;
            if (num108 > Main.maxTilesX) num108 = Main.maxTilesX;
            if (num109 < 0) num109 = 0;
            if (num110 > Main.maxTilesY) num110 = Main.maxTilesY;
            if (NPC.velocity.X < 0f) NPC.spriteDirection = 1;
            if (NPC.velocity.X > 0f) NPC.spriteDirection = -1;

            float num115 = 16f;
            float num116 = 0.4f;

            Vector2 vector14 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float num118 = Main.rand.Next(-500, 501) + player.position.X + (player.width / 2);
            float num119 = Main.rand.Next(-500, 501) + player.position.Y + (player.height / 2);
            num118 = ((int)(num118 / 16f) * 16);
            num119 = ((int)(num119 / 16f) * 16);
            vector14.X = ((int)(vector14.X / 16f) * 16);
            vector14.Y = ((int)(vector14.Y / 16f) * 16);
            num118 -= vector14.X;
            num119 -= vector14.Y;
            float num120 = (float)Math.Sqrt((num118 * num118 + num119 * num119));

            float num123 = Math.Abs(num118);
            float num124 = Math.Abs(num119);
            float num125 = num115 / num120;
            num118 *= num125;
            num119 *= num125;

            bool flag14 = false;
            if (((NPC.velocity.X > 0f && num118 < 0f) || (NPC.velocity.X < 0f && num118 > 0f) || (NPC.velocity.Y > 0f && num119 < 0f) || (NPC.velocity.Y < 0f && num119 > 0f)) && Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) > num116 / 2f && num120 < 300f)
            {
                flag14 = true;
                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num115) NPC.velocity *= 1.1f;
            }
            if (NPC.position.Y > player.position.Y || (player.position.Y / 16f) > Main.worldSurface || player.dead)
            {
                flag14 = true;
                if (Math.Abs(NPC.velocity.X) < num115 / 2f)
                {
                    if (NPC.velocity.X == 0f) NPC.velocity.X = NPC.velocity.X - NPC.direction;
                    NPC.velocity.X = NPC.velocity.X * 1.1f;
                }
                else
                {
                    if (NPC.velocity.Y > -num115) NPC.velocity.Y = NPC.velocity.Y - num116;
                }
            }
            if (!flag14)
            {
                if ((NPC.velocity.X > 0f && num118 > 0f) || (NPC.velocity.X < 0f && num118 < 0f) || (NPC.velocity.Y > 0f && num119 > 0f) || (NPC.velocity.Y < 0f && num119 < 0f))
                {
                    if (NPC.velocity.X < num118) NPC.velocity.X = NPC.velocity.X + num116;
                    else
                    {
                        if (NPC.velocity.X > num118) NPC.velocity.X = NPC.velocity.X - num116;
                    }
                    if (NPC.velocity.Y < num119) NPC.velocity.Y = NPC.velocity.Y + num116;
                    else
                    {
                        if (NPC.velocity.Y > num119) NPC.velocity.Y = NPC.velocity.Y - num116;
                    }
                    if (Math.Abs(num119) < num115 * 0.2 && ((NPC.velocity.X > 0f && num118 < 0f) || (NPC.velocity.X < 0f && num118 > 0f)))
                    {
                        if (NPC.velocity.Y > 0f) NPC.velocity.Y = NPC.velocity.Y + num116 * 2f;
                        else NPC.velocity.Y = NPC.velocity.Y - num116 * 2f;
                    }
                    if (Math.Abs(num118) < num115 * 0.2 && ((NPC.velocity.Y > 0f && num119 < 0f) || (NPC.velocity.Y < 0f && num119 > 0f)))
                    {
                        if (NPC.velocity.X > 0f) NPC.velocity.X = NPC.velocity.X + num116 * 2f;
                        else NPC.velocity.X = NPC.velocity.X - num116 * 2f;
                    }
                }
                else
                {
                    if (num123 > num124)
                    {
                        if (NPC.velocity.X < num118) NPC.velocity.X = NPC.velocity.X + num116 * 1.1f;
                        else
                        {
                            if (NPC.velocity.X > num118) NPC.velocity.X = NPC.velocity.X - num116 * 1.1f;
                        }
                        if ((Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < num115 * 0.5)
                        {
                            if (NPC.velocity.Y > 0f) NPC.velocity.Y = NPC.velocity.Y + num116;
                            else NPC.velocity.Y = NPC.velocity.Y - num116;
                        }
                    }
                    else
                    {
                        if (NPC.velocity.Y < num119) NPC.velocity.Y = NPC.velocity.Y + num116 * 1.1f;
                        else
                        {
                            if (NPC.velocity.Y > num119) NPC.velocity.Y = NPC.velocity.Y - num116 * 1.1f;
                        }
                        if ((Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y)) < num115 * 0.5)
                        {
                            if (NPC.velocity.X > 0f) NPC.velocity.X = NPC.velocity.X + num116;
                            else NPC.velocity.X = NPC.velocity.X - num116;
                        }
                    }
                }
            }
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (Main.masterMode)
                npcLoot.Add(ItemDropRule.Common(ItemType<JormungandrRelic>()));

            npcLoot.Add(ItemDropRule.Common(ItemID.WoodenCrate, 1, 4, 6));
            npcLoot.Add(ItemDropRule.Common(ItemID.IronCrate, 1, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.GoldenCrate, 2, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemID.OceanCrate, 6, 1, 2));
            npcLoot.Add(ItemDropRule.Common(ItemType<BenthicBox>(), 1, 2, 4));

            //Item.NewItem(NPC.getRect(), ItemType<JormungandrTrophy>(), 10);

            if (Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.BossBag(ItemType<JormungandrBag>()));
            }
            else
            {
                //Item.NewItem(npc.getRect(), ItemType<JormungandrMask>(), 7);

                npcLoot.Add(ItemDropRule.OneFromOptions(1, ItemType<BenthicBugNet>(), ItemType<BenthicFishingRod>()));
                npcLoot.Add(ItemDropRule.OneFromOptions(2, ItemID.WaterWalkingBoots, ItemID.SandcastleBucket, ItemID.Flipper, ItemID.BreathingReed));
                npcLoot.Add(ItemDropRule.OneFromOptions(2, ItemID.SharkBait, ItemID.FloatingTube, ItemID.BeachBall));
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = (Texture2D)Request<Texture2D>(Texture);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1) 
                effects = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, 
                new Vector2(NPC.position.X - Main.screenPosition.X + (NPC.width / 2) - texture.Width / 2f + origin.X, NPC.position.Y - Main.screenPosition.Y +  4f + origin.Y), 
                new Rectangle?(NPC.frame), drawColor, NPC.rotation, origin, NPC.scale, effects, 0f);
            return false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.masterMode)
                target.AddBuff(BuffID.Venom, 12 * 60);
            else if (Main.expertMode)
                target.AddBuff(BuffID.Venom, 7 * 60);
            else if (!Main.expertMode)
                target.AddBuff(BuffID.Poisoned, 7 * 60);
        }

        public override void OnKill()
        {
            Downeds.DownedSeaSerpent = true;
        }
    }

    public class JormungandrBody : ModNPC
    {
        int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jörmungandr");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 38; 
            NPC.height = 46;

            NPC.aiStyle = NPCAIStyleID.TheDestroyer;
            NPC.boss = true;

            NPC.damage = 40;
            NPC.defense = 8;
            NPC.lifeMax = 14000;

            NPC.noGravity = true;
            NPC.noTileCollide = true;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath8;

            NPC.knockBackResist = 0.0f;

            NPC.netAlways = true;
            NPC.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => new bool?(false);

        public override void AI()
        {
            timer++;

            if (timer < 3 * 60)
            {
                NPC.immortal = true;
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.immortal = false;
                NPC.dontTakeDamage = false;
            }

            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
        }
    }

    public class JormungandrBody_Spine : ModNPC
    {
        int timer;
        int spineTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jörmungandr");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 62;
            NPC.height = 46;

            NPC.aiStyle = NPCAIStyleID.TheDestroyer;
            NPC.boss = true;

            NPC.damage = 40;
            NPC.defense = 8;
            NPC.lifeMax = 14000;

            NPC.noGravity = true;
            NPC.noTileCollide = true;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath8;

            NPC.knockBackResist = 0.0f;

            NPC.netAlways = true;
            NPC.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => new bool?(false);

        public override void AI()
        {
            timer++;
            spineTimer++;

            if (timer < 3 * 60)
            {
                NPC.immortal = true;
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.immortal = false;
                NPC.dontTakeDamage = false;
            }

            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.masterMode)
                target.AddBuff(BuffID.Venom, 12 * 60);
            else if (Main.expertMode)
                target.AddBuff(BuffID.Venom, 7 * 60);
            else if (!Main.expertMode)
                target.AddBuff(BuffID.Poisoned, 7 * 60);
        }
    }

    public class JormungandrTail : ModNPC
    {
        int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jörmungandr");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 50;
            NPC.height = 66;

            NPC.aiStyle = NPCAIStyleID.TheDestroyer;
            NPC.boss = true;

            NPC.damage = 200;
            NPC.defense = 6;
            NPC.lifeMax = 14000;

            NPC.noGravity = true;
            NPC.noTileCollide = true;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath8;

            NPC.knockBackResist = 0.0f;

            NPC.netAlways = true;
            NPC.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => new bool?(false);

        public override void AI()
        {
            timer++;

            if (timer < 3 * 60)
            {
                NPC.immortal = true;
                NPC.dontTakeDamage = true;
            }
            else
            {
                NPC.immortal = false;
                NPC.dontTakeDamage = false;
            }

            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10);
                NPC.active = false;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.masterMode)
                target.AddBuff(BuffID.Venom, 12 * 60);
            else if (Main.expertMode)
                target.AddBuff(BuffID.Venom, 7 * 60);
            else if (!Main.expertMode)
                target.AddBuff(BuffID.Poisoned, 7 * 60);
        }
    }
}
