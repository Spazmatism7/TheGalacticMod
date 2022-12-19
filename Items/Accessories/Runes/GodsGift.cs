using System;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using GalacticMod.Items.PostML.Celestial;
using GalacticMod.Items.CraftingStations;
using GalacticMod.Assets.Config;
using GalacticMod.Assets.Systems;
using GalacticMod.Buffs;
using System.Collections.Generic;

namespace GalacticMod.Items.Accessories.Runes
{
    [AutoloadEquip(EquipType.Wings)]
    public class GodsGift : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Insane Speed!!" +
                "\nAllows flight and slow fall" +
                "\nAllows the ability to climb walls and dash" +
                "\nGives a chance to dodge attacks" +
                "\nProvides the ability to walk on water, honey, lava, and ice" +
                "\nIncreases jump speed, jump height, fall resistance, damage, mana, crit chance, health, attack speed, life regen, knockback, and mining speed" +
                "\nGrants the ability to swim and greatly extends underwater breathing" +
                "\nGenerates a very subtle glow which becomes more vibrant underwater" +
                "\nAllows user to sextuple jump" +
                "\nGrants immunity to most debuffs, knockback, fire blocks, and temporarily, lava" +
                "\nCauses stars to fall, increases length of invincibility, releases bees and increases movement speed after taking damage" +
				"\nPuts a shell around the owner when below 50% life that reduces damage by 25%" +
				"\nReduces the cooldown of healing potions by 25%" +
				"\nEnemies are more likely to target you" +
                "\nIncreases pickup range for mana stars" +
                "\nLights Wooden Arrows ablaze and inflicts fire damage on attack" +
				"\nIncreases view range for guns (Right click to zoom out)" +
				"\n20% chance to not consume arrows" +
                "\n8% reduced mana usage" +
                "\nRestores mana when damaged and automatically use mana potions when needed" +
				"\nIncreases your max number of minions by 2" +
                "\nUsing weapons rains fire towards the cursor" +
                "\nTurns the holder into a werewolf at night and a merfolk when entering water");
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(600, 8.5f, 2.2f);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var key = GalacticMod.GraniteCoreHotkey.GetAssignedKeys();
            string keyName = "(Not bound, set in controls)";

            if (key.Count > 0)
            {
                keyName = key[0];
            }

            foreach (TooltipLine line in tooltips)
            {
                if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                {
                    line.Text = line.Text + "\nPress and hold '" + keyName + "' to place yourself in a form of stasis";
                }
            }
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Purple;
            Item.width = 28;
            Item.height = 44;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;
        }

        public override void UpdateEquip(Player player)
        {
            //Celestial Rune
            player.lifeRegen += 2;
            player.statDefense += 4;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
            player.GetDamage(DamageClass.Generic) += 0.1f;
            player.GetCritChance(DamageClass.Generic) += 2;
            player.pickSpeed -= 0.15f;
            player.GetKnockback(DamageClass.Summon) += 0.5f;

            player.GetDamage(DamageClass.Magic) += 0.5f;

            //Granite Core
            if (GalacticMod.GraniteCoreHotkey.Current)
            {
                player.AddBuff(BuffType<GraniteCoreBuff>(), 12);
            }

            //Life Insignia
            //Extra Health
            player.statLifeMax2 += 100;

            //Charm of Myths
            player.pStone = true;
            player.lifeRegen += 1;

            //Otherworldly Insignia
            Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.2f, 0.8f, 0.9f); //Glow

            //Fire Totem
            player.GetModPlayer<GalacticPlayer>().fireTotem = true;

            //Warrior Rune
            //Emblem
            player.GetDamage(DamageClass.Melee) += 0.15f;

            //Fire Gauntlet
            player.kbGlove = true;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
            player.GetDamage(DamageClass.Melee) += 0.1f;
            player.magmaStone = true;

            //Ranger Rune
            //Emblem
            player.GetDamage(DamageClass.Ranged) += 0.15f;

            //Sniper Scope
            player.scope = true;
            player.GetCritChance(DamageClass.Ranged) += 1;
            player.GetDamage(DamageClass.Ranged) += 0.1f;

            //Molten Quiver
            player.magicQuiver = true;
            player.arrowDamage += 0.1f;
            player.hasMoltenQuiver = true;

            //Sorcerer Rune
            //Emblem
            player.GetDamage(DamageClass.Magic) += 0.15f;

            //Celestial Cuffs
            player.manaMagnet = true;
            player.magicCuffs = true;

            //Mana Flower
            player.manaFlower = true;
            player.manaCost -= 0.08f;

            //Summoner Rune
            //Emblem
            player.GetDamage(DamageClass.Summon) += 0.15f;

            //Pygmy Necklace
            player.maxMinions++;

            //Papyrus Scarab
            //player.minionKB += 2f;
            player.GetDamage(DamageClass.Summon) += 0.15f;
            player.maxMinions++;

            //Neutron Insignia
            //Ankh Shield
            player.buffImmune[46] = true;
            player.noKnockback = true;
            player.fireWalk = true;
            player.buffImmune[33] = true;
            player.buffImmune[36] = true;
            player.buffImmune[30] = true;
            player.buffImmune[20] = true;
            player.buffImmune[32] = true;
            player.buffImmune[31] = true;
            player.buffImmune[35] = true;
            player.buffImmune[23] = true;
            player.buffImmune[22] = true;

            //Frozen Turtle Shell
            player.AddBuff(62, 5);

            //Pocket Mirror & Hand Warmer & Philosopher's Stone
            player.buffImmune[156] = true;
            player.buffImmune[46] = true;
            player.buffImmune[47] = true;
            player.pStone = true;

            //Flesh Knuckles
            player.aggro += 400;

            //Wooden Shield & Shackle & Ankh Shield/Flesh Knuckles Defense
            player.statDefense += 14;

            //Universal Insignia
            //Frog Legs
            player.jumpSpeedBoost += 2.4f;
            player.extraFall += 15;

            //Hellspark Boots
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 420;
            player.accRunSpeed = 12f;
            player.moveSpeed += 2f;
            player.iceSkate = true;
            player.rocketBoots = 3;

            //Master Ninja Gear
            player.blackBelt = true;
            player.dashType = 3;
            player.spikedBoots = 2;

            //Arctic Diving Gear
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            if (player.wet)
            {
                Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.2f, 0.8f, 0.9f);
            }

            //Bundle of Balloons
            player.hasJumpOption_Cloud = true;
            player.hasJumpOption_Sandstorm = true;
            player.hasJumpOption_Blizzard = true;
            player.jumpBoost = true;

            //Fart in a Balloon & Sharkron Balloon
            player.hasJumpOption_Fart = true;
            player.hasJumpOption_Sail = true;
        }

        //Universal Insignia
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.controlUp && player.controlJump && player.wingTime > 0)
            {
                if (Main.rand.NextBool(10))
                {
                    var dust = Dust.NewDustDirect(player.position, player.width, player.height, DustID.ShadowbeamStaff);
                    dust.scale = 2f;
                }
            }
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 2f;
            ascentWhenRising = 0.3f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2.2f;
            constantAscend = 0.15f;
        }

        //Neutron Insignia
        private void ApplyEquipFunctional(int itemSlot, Item currentItem, Player player)
        {
            //Star Veil
            player.starCloakItem = currentItem;
            player.longInvince = true;
            player.starCloakItem_starVeilOverrideItem = currentItem;

            //Sweetheart Necklace
            player.honeyCombItem = currentItem;
            player.panic = true;
        }

        public void VanillaUpdateAccessory(int i, Item item, bool hideVisual, ref bool flag, ref bool flag2, ref bool flag3, Player player)
        {
            player.accMerman = true;
            player.wolfAcc = true;
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemType<GodsGift>());
            recipe.AddIngredient(Mod, "LifeInsignia");
            recipe.AddIngredient(Mod, "NeutronInsignia");
            recipe.AddIngredient(Mod, "OtherworldlyInsignia");
            recipe.AddIngredient(Mod, "UniversalInsignia");
            recipe.AddIngredient(Mod, "GraniteCoreItem");
            recipe.AddIngredient(Mod, "CelestialRune");
            recipe.AddTile(Mod, "Infinity");
            recipe.Register();
        }
    }
}