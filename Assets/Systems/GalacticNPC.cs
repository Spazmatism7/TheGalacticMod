using GalacticMod.Buffs;
using GalacticMod.Items;
using GalacticMod.NPCs;
using GalacticMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;

namespace GalacticMod.Assets.Systems
{
    public class GalacticNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool Hellfire;
        public bool TerraBurn;
        public bool LunarBurn;
        public bool sandBlasted;
        public bool asteroidBlaze;
        public bool iridiumPoisoning;
        public bool truffle;
        public bool stunned;
        public bool elementalBlaze;

        public override void ResetEffects(NPC npc)
        {
            Hellfire = false;
            TerraBurn = false;
            LunarBurn = false;
            sandBlasted = false;
            asteroidBlaze = false;
            iridiumPoisoning = false;
            truffle = false;
            stunned = false;
            elementalBlaze = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (Hellfire)
            {
                npc.lifeRegen -= 100;
                damage = 50;
            }
            if (sandBlasted)
            {
                npc.lifeRegen -= 40;
                damage = 20;
            }
            if (TerraBurn)
            {
                npc.lifeRegen -= 90;
                damage = 30;
            }
            if (LunarBurn)
            {
                npc.lifeRegen -= 90;
                damage = 37;
            }
            if (asteroidBlaze)
            {
                npc.lifeRegen -= 75;
                damage = 30;
            }
            if (iridiumPoisoning)
            {
                npc.lifeRegen -= 98;
                damage = 30;
            }
            if (truffle)
            {
                npc.lifeRegen -= 20;
                damage = 10;
            }
            if (elementalBlaze)
            {
                npc.lifeRegen -= 95;
                damage = 45;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (Hellfire)
            {
                int dust = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Torch, npc.velocity.X * 1.2f, npc.velocity.Y * 1.2f, 0, default, 2.5f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                Main.dust[dust].noGravity = true; //this make so the dust has no gravity

                int dust2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Torch, npc.velocity.X, -1, 0, default, 1.5f);
                Main.dust[dust2].noGravity = true; //this make so the dust has no gravity
            }

            if (TerraBurn)
            {
                int dust = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.TerraBlade, npc.velocity.X * 1.2f, npc.velocity.Y * 1.2f, 0, default, 2.5f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                Main.dust[dust].noGravity = true; //this make so the dust has no gravity

                int dust2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Chlorophyte, npc.velocity.X, -1, 0, default, 1f);
                Main.dust[dust2].noGravity = true; //this make so the dust has no gravity
            }

            if (LunarBurn)
            {
                int dust = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Vortex, npc.velocity.X, npc.velocity.Y, 0, default, 2f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                Main.dust[dust].noGravity = true; //this make so the dust has no gravity

                int dust2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Electric, npc.velocity.X, npc.velocity.Y, 0, default, 2f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                Main.dust[dust2].noGravity = true; //this make so the dust has no gravity

                int dust3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.FishronWings, npc.velocity.X, -1, 0, default, 2.5f);
                Main.dust[dust3].noGravity = true; //this make so the dust has no gravity
            }
            if (asteroidBlaze)
            {
                int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Torch, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, default, 2.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1f;
                Main.dust[dust].velocity.Y -= 2f;
                int dust2 = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.MeteorHead, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 0, default, 2.5f);
                Main.dust[dust2].noGravity = true;
                Main.dust[dust2].velocity *= 1f;
                Main.dust[dust2].velocity.Y -= 2f;
            }

            if (truffle)
            {
                if (Main.rand.NextBool(3))
                    Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.DungeonWater, npc.velocity.X, -1, 150, default, 1.3f);
            }

            if (elementalBlaze)
            {
                int dust = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Torch, npc.velocity.X, npc.velocity.Y, 0, default, 2f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                Main.dust[dust].noGravity = true; //this make so the dust has no gravity

                int dust1 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.FlameBurst, npc.velocity.X, npc.velocity.Y, 0, default, 2f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                Main.dust[dust1].noGravity = true; //this make so the dust has no gravity

                int dust2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Shadowflame, npc.velocity.X, npc.velocity.Y, 0, default, 2f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
                Main.dust[dust2].noGravity = true; //this make so the dust has no gravity

                int dust3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, DustID.Frost, npc.velocity.X, -1, 0, default, 2.5f);
                Main.dust[dust3].noGravity = true; //this make so the dust has no gravity
            }
        }

        public override void AI(NPC npc)
        {
            if (npc.type is NPCID.Skeleton or NPCID.SkeletonAlien or NPCID.SkeletonArcher or NPCID.SkeletonAstonaut or NPCID.SkeletonCommando or NPCID.SkeletonSniper or NPCID.SkeletonTopHat
                or NPCID.BigPantlessSkeleton or NPCID.BigSkeleton or NPCID.ArmoredSkeleton or NPCID.BigHeadacheSkeleton or NPCID.BigMisassembledSkeleton or NPCID.BoneThrowingSkeleton or 
                NPCID.BoneThrowingSkeleton2 or NPCID.BoneThrowingSkeleton3 or NPCID.BoneThrowingSkeleton4 or NPCID.DD2SkeletonT1 or NPCID.DD2SkeletonT3 or NPCID.CursedSkull or 
                NPCID.GiantCursedSkull or NPCID.HeadacheSkeleton or NPCID.HeavySkeleton or NPCID.MisassembledSkeleton or NPCID.PantlessSkeleton or NPCID.GreekSkeleton or 
                NPCID.SmallHeadacheSkeleton or NPCID.SmallMisassembledSkeleton or NPCID.SmallPantlessSkeleton or NPCID.SmallSkeleton or NPCID.SporeSkeleton or NPCID.TacticalSkeleton
                or NPCID.SkeletronHead or NPCID.SkeletronHand or NPCID.PossessedArmor or NPCID.ShadowFlameApparition or NPCID.Sharkron or NPCID.Sharkron2 or NPCID.DD2LanePortal or NPCID.Probe
                or NPCID.PirateShip or NPCID.PirateShipCannon or NPCID.DungeonGuardian or NPCID.Ghost or NPCID.PirateGhost or NPCID.Wraith or NPCID.Reaper or NPCID.DesertDjinn
                or NPCID.TheDestroyer or NPCID.TheDestroyerBody or NPCID.TheDestroyerTail or NPCID.SkeletronPrime or NPCID.PrimeCannon or NPCID.PrimeLaser or NPCID.PrimeSaw or NPCID.PrimeVice
                or NPCID.AncientCultistSquidhead or NPCID.CultistBossClone or NPCID.DungeonSpirit or NPCID.ForceBubble or NPCID.CultistTablet or NPCID.CultistDragonHead or 
                NPCID.CultistDragonBody1 or NPCID.CultistDragonBody2 or NPCID.CultistDragonBody3 or NPCID.CultistDragonBody4 or NPCID.CultistDragonTail)
                npc.buffImmune[BuffType<TerraBurn>()] = true;

            if (npc.type is NPCID.SkeletronHead or NPCID.SkeletronHand or NPCID.ShadowFlameApparition or NPCID.Sharkron or NPCID.Sharkron2 or NPCID.DD2LanePortal or
                NPCID.DungeonGuardian or NPCID.AncientCultistSquidhead or NPCID.CultistBossClone or NPCID.ForceBubble or NPCID.CultistTablet or NPCID.CultistDragonHead or
                NPCID.CultistDragonBody1 or NPCID.CultistDragonBody2 or NPCID.CultistDragonBody3 or NPCID.CultistDragonBody4 or NPCID.CultistDragonTail)
                npc.buffImmune[BuffType<HellfireDebuff>()] = true;

            if (npc.type is NPCID.SkeletronHead or NPCID.SkeletronHand or NPCID.PossessedArmor or NPCID.ShadowFlameApparition or NPCID.Sharkron or NPCID.Sharkron2 or NPCID.DD2LanePortal or 
               NPCID.DungeonGuardian or NPCID.Ghost or NPCID.PirateGhost or NPCID.Wraith or NPCID.Reaper or NPCID.DesertDjinn or NPCID.AncientCultistSquidhead or NPCID.CultistBossClone or 
               NPCID.DungeonSpirit or NPCID.ForceBubble or NPCID.CultistTablet or NPCID.CultistDragonHead or NPCID.CultistDragonBody1 or NPCID.CultistDragonBody2 or NPCID.CultistDragonBody3 
               or NPCID.CultistDragonBody4 or NPCID.CultistDragonTail or NPCID.DungeonSpirit or NPCID.MartianSaucerCore or NPCID.MartianSaucerCannon or NPCID.MartianSaucerTurret or 
               NPCID.LunarTowerNebula or NPCID.NebulaBeast or NPCID.NebulaBrain or NPCID.NebulaHeadcrab or NPCID.NebulaSoldier or NPCID.RaggedCaster or NPCID.RaggedCasterOpenCoat)
            {
                npc.buffImmune[BuffType<SpiritCurse>()] = true;
                npc.buffImmune[BuffType<Infested>()] = true;
            }

            if (npc.type is NPCID.TheDestroyer or NPCID.TheDestroyerBody or NPCID.TheDestroyerTail or NPCID.ShadowFlameApparition or NPCID.Sharkron or NPCID.Sharkron2 or NPCID.DD2LanePortal or
                NPCID.DungeonGuardian or NPCID.AncientCultistSquidhead or NPCID.CultistBossClone or NPCID.ForceBubble or NPCID.CultistTablet or NPCID.CultistDragonHead or
                NPCID.CultistDragonBody1 or NPCID.CultistDragonBody2 or NPCID.CultistDragonBody3 or NPCID.CultistDragonBody4 or NPCID.CultistDragonTail or NPCID.EaterofWorldsBody or 
                NPCID.EaterofWorldsHead or NPCID.EaterofWorldsTail or NPCID.SkeletronPrime or NPCID.Skeleton or NPCID.SkeletonAlien or NPCID.SkeletonArcher or NPCID.SkeletonAstonaut or 
                NPCID.SkeletonCommando or NPCID.SkeletonSniper or NPCID.SkeletonTopHat or NPCID.BigPantlessSkeleton or NPCID.BigSkeleton or NPCID.ArmoredSkeleton or NPCID.BigHeadacheSkeleton 
                or NPCID.BigMisassembledSkeleton or NPCID.BoneThrowingSkeleton or NPCID.BoneThrowingSkeleton2 or NPCID.BoneThrowingSkeleton3 or NPCID.BoneThrowingSkeleton4 or 
                NPCID.DD2SkeletonT1 or NPCID.DD2SkeletonT3 or NPCID.CursedSkull or NPCID.GiantCursedSkull or NPCID.HeadacheSkeleton or NPCID.HeavySkeleton or NPCID.MisassembledSkeleton or 
                NPCID.PantlessSkeleton or NPCID.GreekSkeleton or  NPCID.SmallHeadacheSkeleton or NPCID.SmallMisassembledSkeleton or NPCID.SmallPantlessSkeleton or NPCID.SmallSkeleton or 
                NPCID.SporeSkeleton or NPCID.TacticalSkeleton or NPCID.SkeletronHead or NPCID.SkeletronHand or NPCID.PossessedArmor or NPCID.Probe or NPCID.PirateShip or 
                NPCID.PirateShipCannon or NPCID.Ghost or NPCID.PirateGhost or NPCID.Wraith or NPCID.Reaper or NPCID.DesertDjinn or NPCID.MoonLordCore or NPCID.MoonLordFreeEye or 
                NPCID.MoonLordHand or NPCID.MoonLordHead or NPCID.MoonLordLeechBlob or NPCID.BrainofCthulhu or NPCID.BrainScrambler or NPCID.NebulaBrain or NPCID.EmpressButterfly or 
                NPCID.HallowBoss or NPCID.Plantera or NPCID.PlanterasHook or NPCID.PlanterasTentacle or NPCID.Poltergeist or NPCID.ManEater or NPCID.Snatcher or NPCID.AngryTrapper
                or NPCID.DiggerBody or NPCID.DiggerHead or NPCID.DiggerTail or NPCID.WyvernHead or NPCID.WyvernBody or NPCID.WyvernBody2 or NPCID.WyvernBody3 or NPCID.WyvernLegs or 
                NPCID.WyvernTail or NPCID.CultistBoss or NPCID.CultistDevote or NPCID.GiantWormBody or NPCID.GiantWormHead or NPCID.GiantWormTail or NPCID.DevourerBody or NPCID.DevourerHead
                or NPCID.DevourerTail || npc.boss)
                npc.buffImmune[BuffType<Stunned>()] = true;

            int oldAI = 0;
            if (npc.aiStyle != 0)
            {
                oldAI = npc.aiStyle;
                npc.oldVelocity = npc.velocity;
            }

            if (stunned)
            {
                npc.aiStyle = 0;
                npc.velocity -= npc.velocity;
            }
            //-1 does cool stuff
            else
            {
                npc.aiStyle = oldAI;
                npc.velocity = npc.oldVelocity;
            }
        }
    }
}