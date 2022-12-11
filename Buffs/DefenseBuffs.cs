using GalacticMod.Assets.Systems;
using Terraria;
using Terraria.ModLoader;

namespace GalacticMod.Buffs
{
    public class CobaltDefense : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cobalt Defense");
            Description.SetDefault("Grants defense" +
                "\nHaving to many Defense buffs will reduce your endurance");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 12; //Grant a +4 defense boost to the player while the buff is active.
            player.GetModPlayer<GalacticPlayer>().defenseBuffs++;
        }
    }

    public class PalladiumDefense : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Defense");
            Description.SetDefault("Grants defense" +
                "\nHaving to many Defense buffs will reduce your endurance");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 12; //Grant a +4 defense boost to the player while the buff is active.
            player.GetModPlayer<GalacticPlayer>().defenseBuffs++;
        }
    }

    public class MythrilDefenseBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythril Defense");
            Description.SetDefault("Grants defense" +
                "\nHaving to many Defense buffs will reduce your endurance");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 14; //Grant a +4 defense boost to the player while the buff is active.
            player.GetModPlayer<GalacticPlayer>().defenseBuffs++;
        }
    }

    public class OrichalcumDefenseBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orichalcum Defense");
            Description.SetDefault("Grants defense" +
                "\nHaving to many Defense buffs will reduce your endurance");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 14; //Grant a +4 defense boost to the player while the buff is active.
            player.GetModPlayer<GalacticPlayer>().defenseBuffs++;
        }
    }

    public class AdamantiteDefense : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Defense");
            Description.SetDefault("Grants defense" +
                "\nHaving to many Defense buffs will reduce your endurance");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 17; //Grant a +4 defense boost to the player while the buff is active.
            player.GetModPlayer<GalacticPlayer>().defenseBuffs++;
        }
    }

    public class TitaniumDefense : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Defense");
            Description.SetDefault("Grants defense" +
                "\nHaving to many Defense buffs will reduce your endurance");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 16; //Grant a +4 defense boost to the player while the buff is active.
            player.GetModPlayer<GalacticPlayer>().defenseBuffs++;
        }
    }

    public class DefenseOfTheJungle : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Defense of the Jungle");
            Description.SetDefault("Grants the defense of the Jungle" +
                "\nHaving to many Defense buffs will reduce your endurance");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 24; //Grant a +4 defense boost to the player while the buff is active.
            player.GetModPlayer<GalacticPlayer>().defenseBuffs++;
        }
    }

    public class BeetleBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beetle Guard");
            Description.SetDefault("Increases defense");
            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false; //Add this so the nurse doesn't remove the buff when healing
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense += 26; //Grant a +4 defense boost to the player while the buff is active.
        }
    }
}
