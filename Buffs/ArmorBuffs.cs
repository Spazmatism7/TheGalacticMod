using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GalacticMod.Buffs
{
    public class VanadiumHealing : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vanadium Healing");
            Description.SetDefault("The energies of the fallen heal you");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 2;
        }
    }

    public class OsmiumStrength : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Osmium Might");
            Description.SetDefault("The energies of the dead empower you");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.1f;
        }
    }

    public class Bloodthirsty : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodthirsty");
            Description.SetDefault("Power is warping your mind");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += 0.1f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
            player.statDefense -= 6;
            player.GetCritChance(DamageClass.Generic) += 11;
        }
    }

    public class ZirconiumRun : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zirconium Speed");
            Description.SetDefault("The energies of the fallen speed you on your way");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed += 0.2f;
        }
    }

    public class ZirconiumDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zirconium Slow");
            Description.SetDefault("The energy of your warp slows your movement");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed -= 0.2f;
        }
    }

    public class LutetiumArmor : ModBuff
    {
        //Old armor ability was +30 defense

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lutetium Armor");
            Description.SetDefault("Reduces damage taken and grants immunity to knockback, but reduces movement speed");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.velocity.X *= 0.95f;
            player.velocity.Y *= 0.95f;
            player.extraFall = 20;
            player.iceSkate = false;
            player.endurance += 0.2f;
            player.noKnockback = true;
            if (Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.LunarOre, player.velocity.X, player.velocity.Y, 0, default, 1.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }

    public class LutetiumCooldown : ModBuff
    {
        //public override bool canBeCleared => false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lutetium Cooldown");
            Description.SetDefault("You are unable to harden");
            Main.debuff[Type] = true;
        }
    }

    public class OsmiumCooldown : ModBuff
    {
        //public override bool canBeCleared => false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Osmium Cooldown");
            Description.SetDefault("You are unable to enrage");
            Main.debuff[Type] = true;
        }
    }

    public class GraniteCoreBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Granite Stasis");
            Description.SetDefault("You can no longer take damage, but you cannot move");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.velocity.X *= 0f;
            player.velocity.Y *= 0f;
            player.extraFall = 20;
            player.iceSkate = false;
            player.endurance = 100f;
            player.noKnockback = true;
            player.lifeRegen = 0;
            player.GetDamage(DamageClass.Generic) -= 1f;
            player.GetDamage(DamageClass.Default) -= 1f;
            player.GetDamage(DamageClass.Melee) -= 1f;
            if (Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Granite, player.velocity.X, player.velocity.Y, 0, default, 1.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }
}
