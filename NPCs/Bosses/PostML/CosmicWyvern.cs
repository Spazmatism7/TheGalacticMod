using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;

namespace GalacticMod.NPCs.Bosses.PostML
{
    public class CosmicWyvernHead : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Wyvern");

            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    BuffID.OnFire,
                    BuffID.OnFire3,
                    BuffID.ShadowFlame,
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                //CustomTexturePath = "",
                Position = new Vector2(20f, 14f),
                PortraitPositionXOverride = 30f,
                PortraitPositionYOverride = -6f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }

        public override void SetDefaults()
        {
            int width = 32; int height = width;
            NPC.Size = new Vector2(width, height);
            NPC.aiStyle = NPCAIStyleID.Worm;

            NPC.damage = 130;
            NPC.defense = 20;
            NPC.lifeMax = 50000;
            NPC.knockBackResist = 0.0f;
            NPC.npcSlots = 5f;

            NPC.noGravity = true;
            NPC.noTileCollide = true;

            NPC.netAlways = true;
            NPC.alpha = 255;

            NPC.HitSound = SoundID.NPCHit7;
            NPC.DeathSound = SoundID.NPCDeath8;
            Banner = NPC.type;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
                new FlavorTextBestiaryInfoElement("A colossal dragon-like creature that roams the cosmos. They can only be controlled by beings of unrivaled might")
            });
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.1f;
            return new bool?();
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            if (NPC.target < 0 || NPC.target == 250 || player.dead) NPC.TargetClosest(true);
            if (player.dead && NPC.timeLeft > 300) NPC.timeLeft = 300;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 0f)
                {
                    NPC.ai[3] = NPC.whoAmI;
                    NPC.realLife = NPC.whoAmI;
                    int num8 = NPC.whoAmI;
                    for (int l = 0; l < 14; l++)
                    {
                        int num9 = ModContent.NPCType<CosmicWyvernBody>();
                        switch (l)
                        {
                            case 1:
                            case 8:
                                num9 = ModContent.NPCType<CosmicWyvernLegs>();
                                break;
                            case 11:
                                num9 = ModContent.NPCType<CosmicWyvernBody2>();
                                break;
                            case 12:
                                num9 = ModContent.NPCType<CosmicWyvernBody3>();
                                break;
                            case 13:
                                num9 = ModContent.NPCType<CosmicWyvernTail>();
                                break;
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

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1) effects = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (NPC.width / 2) - texture.Width * NPC.scale / 2f + origin.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + NPC.height - texture.Height * NPC.scale + 4f + origin.Y * NPC.scale + 56f), new Rectangle?(NPC.frame), drawColor, NPC.rotation, origin, NPC.scale, effects, 0f);
            return false;
        }
    }

    public class CosmicWyvernLegs : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Wyvern");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    BuffID.OnFire,
                    BuffID.OnFire3,
                    BuffID.ShadowFlame,
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
        }

        public override void SetDefaults()
        {
            int width = 32; int height = width;
            NPC.Size = new Vector2(width, height);

            NPC.aiStyle = NPCAIStyleID.Worm;

            NPC.damage = 60;
            NPC.defense = 35;
            NPC.lifeMax = 50000;

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
            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10);
                NPC.active = false;
            }
            if (NPC.position.X > Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = 1;
            if (NPC.position.X < Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = -1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1) effects = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (NPC.width / 2) - texture.Width * NPC.scale / 2f + origin.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + NPC.height - texture.Height * NPC.scale + 4f + origin.Y * NPC.scale + 56f), new Rectangle?(NPC.frame), drawColor, NPC.rotation, origin, NPC.scale, effects, 0f);
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_48").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.MartianHit);
                }
            }
        }
    }

    public class CosmicWyvernBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Wyvern");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    BuffID.OnFire,
                    BuffID.OnFire3,
                    BuffID.ShadowFlame,
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
        }

        public override void SetDefaults()
        {
            int width = 32; int height = width;
            NPC.Size = new Vector2(width, height);

            NPC.aiStyle = NPCAIStyleID.Worm;

            NPC.damage = 60;
            NPC.defense = 35;
            NPC.lifeMax = 50000;

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
            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.active = false;
            }
            if (NPC.position.X > Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = 1;
            if (NPC.position.X < Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = -1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1) effects = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (NPC.width / 2) - texture.Width * NPC.scale / 2f + origin.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + NPC.height - texture.Height * NPC.scale + 4f + origin.Y * NPC.scale + 56f), new Rectangle?(NPC.frame), drawColor, NPC.rotation, origin, NPC.scale, effects, 0f);
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_48").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.MartianHit);
                }
            }
        }
    }

    public class CosmicWyvernBody2 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Wyvern");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    BuffID.OnFire,
                    BuffID.OnFire3,
                    BuffID.ShadowFlame,
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
        }

        public override void SetDefaults()
        {
            int width = 32; int height = width;
            NPC.Size = new Vector2(width, height);

            NPC.aiStyle = NPCAIStyleID.Worm;

            NPC.damage = 60;
            NPC.defense = 35;
            NPC.lifeMax = 50000;

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
            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10);
                NPC.active = false;
            }
            if (NPC.position.X > Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = 1;
            if (NPC.position.X < Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = -1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1) effects = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (NPC.width / 2) - texture.Width * NPC.scale / 2f + origin.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + NPC.height - texture.Height * NPC.scale + 4f + origin.Y * NPC.scale + 56f), new Rectangle?(NPC.frame), drawColor, NPC.rotation, origin, NPC.scale, effects, 0f);
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_48").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.MartianHit);
                }
            }
        }
    }

    public class CosmicWyvernBody3 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Wyvern");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    BuffID.OnFire,
                    BuffID.OnFire3,
                    BuffID.ShadowFlame,
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
        }

        public override void SetDefaults()
        {
            int width = 32; int height = width;
            NPC.Size = new Vector2(width, height);

            NPC.aiStyle = NPCAIStyleID.Worm;

            NPC.damage = 60;
            NPC.defense = 35;
            NPC.lifeMax = 50000;

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
            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10);
                NPC.active = false;
            }
            if (NPC.position.X > Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = 1;
            if (NPC.position.X < Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = -1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1) effects = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (NPC.width / 2) - texture.Width * NPC.scale / 2f + origin.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + NPC.height - texture.Height * NPC.scale + 4f + origin.Y * NPC.scale + 56f), new Rectangle?(NPC.frame), drawColor, NPC.rotation, origin, NPC.scale, effects, 0f);
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (NPC.life <= 0)          //this make so when the NPC has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_48").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.MartianHit);
                }
            }
        }
    }

    public class CosmicWyvernTail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Wyvern");

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                    BuffID.Poisoned,
                    BuffID.OnFire,
                    BuffID.OnFire3,
                    BuffID.ShadowFlame,
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);
        }

        public override void SetDefaults()
        {
            int width = 32; int height = width;
            NPC.Size = new Vector2(width, height);

            NPC.aiStyle = NPCAIStyleID.Worm;

            NPC.damage = 60;
            NPC.defense = 35;
            NPC.lifeMax = 50000;

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
            if (!Main.npc[(int)NPC.ai[1]].active)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10);
                NPC.active = false;
            }
            if (NPC.position.X > Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = 1;
            if (NPC.position.X < Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = -1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            SpriteEffects effects = SpriteEffects.None;
            if (NPC.spriteDirection == 1) effects = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, new Vector2(NPC.position.X - Main.screenPosition.X + (NPC.width / 2) - texture.Width * NPC.scale / 2f + origin.X * NPC.scale, NPC.position.Y - Main.screenPosition.Y + NPC.height - texture.Height * NPC.scale + 4f + origin.Y * NPC.scale + 56f), new Rectangle?(NPC.frame), drawColor, NPC.rotation, origin, NPC.scale, effects, 0f);
            return false;
        }
    }
}