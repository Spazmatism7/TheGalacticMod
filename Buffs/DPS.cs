using Terraria;
using Terraria.ModLoader;
using GalacticMod.Assets.Systems;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace GalacticMod.Buffs
{
    public class HellfireDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire");
            Description.SetDefault("This is fine");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GalacticPlayer>().Hellfire = true;
            int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Torch, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default, 2.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1f;
            Main.dust[dust].velocity.Y -= 2f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GalacticNPC>().Hellfire = true;
        }
    }

    public class SandBlasted : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Blasted!");
            Description.SetDefault("Suffering, Pain and Suffering, all in one!");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GalacticPlayer>().sandBlasted = true;
            int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Sandnado, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default, 2.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1f;
            Main.dust[dust].velocity.Y -= 2f;
            player.blind = true;
            player.slow = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GalacticNPC>().sandBlasted = true;
        }
    }

    public class TerraBurn : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Burn");
            Description.SetDefault("A unique burning sensation");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GalacticPlayer>().terraBurn = true;

            int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.TerraBlade, player.velocity.X * 1.2f, player.velocity.Y * 1.1f, 0, default, 2.5f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            Main.dust[dust].noGravity = true; //this make so the dust has no gravity

            int dust2 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.Chlorophyte, player.velocity.X, -1, 0, default, 0.8f);
            Main.dust[dust2].noGravity = true; //this make so the dust has no gravity
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GalacticNPC>().TerraBurn = true;
        }
    }

    public class SpiritCurse : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit's Curse");
            Description.SetDefault("Death by the dead");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GalacticPlayer>().spiritCurse = true;

            int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.Vortex, player.velocity.X, player.velocity.Y, 0, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            Main.dust[dust].noGravity = true; //this make so the dust has no gravity

            int dust2 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.Electric, player.velocity.X, player.velocity.Y, 0, default, 1f);   //this defines the flames dust and color, change DustID to wat dust you want from Terraria, or add mod.DustType("CustomDustName") for your custom dust
            Main.dust[dust2].noGravity = true; //this make so the dust has no gravity

            int dust3 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, DustID.FishronWings, player.velocity.X, -1, 0, default, 1.25f);
            Main.dust[dust3].noGravity = true; //this make so the dust has no gravity
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GalacticNPC>().LunarBurn = true;
        }
    }

    public class AsteroidBlaze : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asteriod Blaze");
            Description.SetDefault("Extraterrestrial flames eat away your body");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GalacticPlayer>().asteroidBlaze = true;
            int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Torch, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default, 2.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1f;
            Main.dust[dust].velocity.Y -= 2f;
            int dust2 = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.MeteorHead, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default, 1f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 1f;
            Main.dust[dust2].velocity.Y -= 2f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GalacticNPC>().asteroidBlaze = true;
        }
    }

    public class IridiumPoison : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iridium Poisoning");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GalacticPlayer>().iridiumPoisoning = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GalacticNPC>().iridiumPoisoning = true;
        }
    }

    public class Infested : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infested");
            Description.SetDefault("A debuff caused by truffles");
            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen -= 4;
            Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.DungeonWater, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 150, default, 1.3f);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GalacticNPC>().truffle = true;
        }
    }

    public class ElementalBlaze : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elemental Blaze");
            Description.SetDefault("The fury of the Elements consumes you");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<GalacticPlayer>().elementalBlaze = true;
            int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Torch, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default, 2.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1f;
            Main.dust[dust].velocity.Y -= 2f;
            int dust1 = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.FlameBurst, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default, 2.5f);
            Main.dust[dust1].noGravity = true;
            Main.dust[dust1].velocity *= 1f;
            Main.dust[dust1].velocity.Y -= 2f;
            int dust2 = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Shadowflame, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default, 2.5f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 1f;
            Main.dust[dust2].velocity.Y -= 2f;
            int dust3 = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Frost, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default, 2.5f);
            Main.dust[dust3].noGravity = true;
            Main.dust[dust3].velocity *= 1f;
            Main.dust[dust3].velocity.Y -= 2f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GalacticNPC>().elementalBlaze = true;
        }
    }

    public class NebulaFlameD : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nebula Flame");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen -= 15;
            int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Shadowflame, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default, 2.5f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 1f;
            Main.dust[dust].velocity.Y -= 2f;
            int dust1 = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.PinkTorch, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 0, default, 2.5f);
            Main.dust[dust1].noGravity = true;
            Main.dust[dust1].velocity *= 1f;
            Main.dust[dust1].velocity.Y -= 2f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GalacticNPC>().nebulaFlame = true;
        }
    }
}
