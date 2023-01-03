using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace GalacticMod.NPCs.Legacy
{
	public class MythicalWyvernBody : ModNPC
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Mythical Wyvern");

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0) {
				Hide = true
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);

			NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData {
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

		public override void SetDefaults() {
			int width = 32; int height = width;
			NPC.Size = new Vector2(width, height);

			NPC.aiStyle = NPCAIStyleID.Worm;

			NPC.damage = 40;
			NPC.defense = 20;
			NPC.lifeMax = 4000;

			NPC.noGravity = true;
			NPC.noTileCollide = true;

			NPC.HitSound = SoundID.NPCHit7;
			NPC.DeathSound = SoundID.NPCDeath8;

			NPC.knockBackResist = 0.0f;

			NPC.netAlways = true;
			NPC.dontCountMe = true;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position) => new bool?(false);
	
		public override void AI() {			
			if (!Main.npc[(int)NPC.ai[1]].active) {
				NPC.life = 0;
				NPC.HitEffect(0, 10.0);
				NPC.active = false;
			}
			if (NPC.position.X > Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = 1;
			if (NPC.position.X < Main.npc[(int)NPC.ai[1]].position.X) NPC.spriteDirection = -1;	
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
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
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_23").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                }
            }
        }
    }

    public class MythicalWyvernBody2 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythical Wyvern");

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

            NPC.damage = 40;
            NPC.defense = 20;
            NPC.lifeMax = 4000;

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
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_23").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                }
            }
        }
    }

    public class MythicalWyvernBody3 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythical Wyvern");

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

            NPC.damage = 40;
            NPC.defense = 20;
            NPC.lifeMax = 4000;

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
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("Gore_23").Type, 1f);
                for (int i = 0; i < 10; i++)
                {
                    Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Blood);
                }
            }
        }
    }
}