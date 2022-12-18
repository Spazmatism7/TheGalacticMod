using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Dyes;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.ModLoader.UI;
using static Terraria.ModLoader.ModContent;
using GalacticMod.NPCs.Bosses.PreHM;
using GalacticMod.NPCs.Bosses.Hardmode;
using GalacticMod.NPCs.Bosses.PostML;
using GalacticMod.Items.PreHM.Desert;
using GalacticMod.Items.PostML.Hellfire;
using GalacticMod.Items.PostML.Galactic;
using GalacticMod.Assets.Systems;
using GalacticMod.Items.Hardmode.Asteroid;
using GalacticMod.Items.Boss;
using GalacticMod.NPCs.Old;
using GalacticMod.Items.Old;
using GalacticMod.Items.PreHM.Star;
using GalacticMod.Items.PreHM.Nautilus;
using GalacticMod.Items.Boss.Master;

namespace GalacticMod
{
	public class GalacticMod : Mod
	{
        public static ModKeybind ArmourSpecialHotkey;
        public static ModKeybind GraniteCoreHotkey;

        public GalacticMod()
		{
		}

        public override void Load()
        {
            ArmourSpecialHotkey = KeybindLoader.RegisterKeybind(this, "Armor Ability", "Q");
            GraniteCoreHotkey = KeybindLoader.RegisterKeybind(this, "Granite Core", "Z");

            /*ModLoader.TryGetMod("Wikithis", out Mod wikithis);
            if (wikithis != null && !Main.dedServ)
            {
                wikithis.Call("", this, "");

                wikithis.Call(0, this, "");
            }*/

            //https://steamcommunity.com/sharedfiles/filedetails/?id=2832487441
        }

        public override void Unload()
        {
            ArmourSpecialHotkey = null;
            GraniteCoreHotkey = null;
        }

        public override void PostSetupContent()
		{
			if (ModLoader.HasMod("BossChecklist"))
			{
				Mod bossChecklist = ModLoader.GetMod("BossChecklist");
				if (bossChecklist != null)
				{
					bossChecklist.Call("AddBoss", 3.5f, NPCType<DesertSpirit>(), this, "Desert Spirit", (() => Downeds.DownedDesertSpirit), () => true,
                        new List<int> {  ItemType<DesertSpiritTrophy>(), /*ItemType<Relic>(), ItemType<Pet>() },*/ ItemType<DesertSpiritBag>(), ItemType<SandyRing>()/*, ItemType<Mask>()*/ },
                        new List<int> { ItemType<SandShotgun>(), ItemType<LightningCaller>(), ItemType<Sandstorm>(), ItemType<StormSpear>(), ItemType<SandWhip>(), ItemID.HealingPotion },
						"Activate the [i:" + ItemType<DesertMedallion>() + "] in the Desert", null);

                    bossChecklist.Call("AddBoss", 4.5f, NPCType<SkyGod>(), this, "Sky God", (() => Downeds.DownedSkyGod), () => true,
                        new List<int> { /*ItemType<DesertSpiritTrophy>(), //ItemType<MusicBoxStormySands>()*/ },
                        new List<int> { ItemType<SkyGodBag>(), ItemType<SkyEssence>(), ItemType<Cepheus>(), ItemType<Cassiopeia>(), ItemType<Pyxis>(), ItemType<Orion>(), ItemType<Sagitta>(), 
                            ItemType<CanesVenatici>(), ItemType<StarCaller>(), ItemType<StarHelmet>(), ItemType<StarChestplate>(), ItemType<StarLeggings>(), ItemID.HealingPotion },
                        "Activate the [i:"/* + ItemType<DesertMedallion>()*/ + "] in the Sky", null);

                    bossChecklist.Call("AddBoss", 5.5f, NPCType<JormungandrHead>(), this, "Jormungandr", (() => Downeds.DownedSeaSerpent), () => true,
                        new List<int> { /*ItemType<Trophy>(),*/ ItemType<JormungandrRelic>()/*, ItemType<HellEgg>()*/, ItemType<JormungandrBag>(), ItemType<SpineScarfItem>() },
                        new List<int> { ItemType<JormungandrBag>(), ItemID.WoodenCrate, ItemID.IronCrate, ItemID.GoldenCrate, ItemID.OceanCrate, ItemType<BenthicBox>(),
                            ItemType<BenthicBugNet>(), ItemType<BenthicFishingRod>(), ItemID.WaterWalkingBoots, ItemID.SandcastleBucket, ItemID.Flipper, ItemID.BreathingReed, ItemID.SharkBait,
                            ItemID.FloatingTube, ItemID.BeachBall, ItemID.HealingPotion },
                        "Awaken with a [i:" + ItemType<ScaleOfSeas>() + "]", null);

                    bossChecklist.Call("AddMiniBoss", 10f, NPCType<MythicalWyvernHead>(), this, "Mythical Wyvern", (() => Downeds.downedMythicalWyvern), () => true,
                        new List<int> { },
                        new List<int> { ItemType<RedEnvelope>(), ItemID.SoulofFlight, ItemID.ReleaseLantern, ItemType<MythicalLionMask>(), ItemType<MythicalRobe>() },
                        "Awakens only in times of celebration, when it parades through the sky", null);

                    bossChecklist.Call("AddBoss", 13f, NPCType<AsteroidBehemoth>(), this, "Asteroid Behemoth", (() => Downeds.DownedAsteroidBoss), () => true,
                        new List<int> { /*ItemType<HellfireDragonTrophy>(),*/ ItemType<AsteroidBehemothRelic>(), /*ItemType<HellEgg>().*/ ItemType<AsteroidBehemothBag>()/*, ItemType<GalacticAmulet>()*/ },
                        new List<int> { ItemType<AsteroidBehemothBag>(), ItemType<IridiumShard>(), ItemType<AsteroidBar>(), ItemType<AsteroidShardItem>(), ItemID.GreaterHealingPotion },
                        "Awaken with a [i:" + ItemType<AsteroidRelic>() + "]", null);

                    bossChecklist.Call("AddBoss", 19f, NPCType<HellfireDragon>(), this, "Hellfire Dragon", (() => Downeds.DownedHellDragonBoss), () => true,
                        new List<int> { ItemType<HellfireDragonTrophy>(), ItemType<HellfireDragonRelic>(), ItemType<HellEgg>(), ItemType<HellfireDragonBag>(), ItemType<HellfireShield>() },
						new List<int> { ItemType<HellfireBar>(), ItemType<HellfireWings>(), ItemID.FireGauntlet, ItemID.FlameWakerBoots, ItemID.SuperHealingPotion },
						"Anger with a [i:" + ItemType<DragonEgg>() + "]", null);

					bossChecklist.Call("AddBoss", 19.1f, NPCType<GalacticPeril>(), this, "The Galactic Peril", (() => Downeds.DownedGalacticPeril), () => true,
                        new List<int> { /*ItemType<HellfireDragonTrophy>(),*/ ItemType<GalacticPerilRelic>(), /*ItemType<HellEgg>().*/ ItemType<GalacticPerilBag>(), ItemType<GalacticAmulet>() },
						new List<int> { ItemType<GalacticPerilBag>(), ItemType<GalaxyBlade>(), ItemType<GalacticFireRod>(), ItemType<GalaxyArrowStaff>(), ItemType<GalaxyBow>(), ItemID.SuperHealingPotion },
						"Use a [i:" + ItemType<GalacticSigil>() + "] to challenge its power", null);
				}
			}

            ModLoader.TryGetMod("PhaseIndicator", out Mod phaseIndicator);
            if (phaseIndicator != null && !Main.dedServ)
            {
                //phaseIndicator.Call("PhaseIndicator", NPCType<HellfireDragon>(), (NPC npc, float difficulty) => 0.5f);

                // ... If you will call those two at the same time, then they both will appear in boss bar, correspondently on 48% and 24% of boss health bar
                // Most of the time you will need at 0.5 (aka 50%) call, probably.

                //phaseIndicator.Call("PhaseIndicator", NPCType<AsteroidBehemoth>(), (NPC npc, float difficulty) => 0.5f);

                phaseIndicator.Call("PhaseIndicator", NPCType<GalacticPeril>(), (NPC npc, float difficulty) => 0.8f);
                phaseIndicator.Call("PhaseIndicator", NPCType<GalacticPeril>(), (NPC npc, float difficulty) => 0.6f);
                phaseIndicator.Call("PhaseIndicator", NPCType<GalacticPeril>(), (NPC npc, float difficulty) => 0.4f);
                phaseIndicator.Call("PhaseIndicator", NPCType<GalacticPeril>(), (NPC npc, float difficulty) => 0.2f);
                if (Main.expertMode)
                    phaseIndicator.Call("PhaseIndicator", NPCType<GalacticPeril>(), (NPC npc, float difficulty) => 0.05f);

            }

            ModLoader.TryGetMod("CalamityMod", out Mod calamity);
            if (calamity != null && !Main.dedServ)
            {
            }
        }

        public override object Call(params object[] args)
        {
            // Make sure the call doesn't include anything that could potentially cause exceptions.
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args), "Arguments cannot be null!");
            }

            if (args.Length == 0)
            {
                throw new ArgumentException("Arguments cannot be empty!");
            }

            // This check makes sure that the argument is a string using pattern matching.
            // Since we only need one parameter, we'll take only the first item in the array..
            if (args[0] is string content)
            {
                // ..And treat it as a command type.
                switch (content)
                {
                    case "downedDesertSpirit":
                        return Downeds.DownedDesertSpirit;
                    case "downedSkyGod":
                        return Downeds.DownedSkyGod;
                    case "DownedSeaSerpent":
                        return Downeds.DownedSeaSerpent;
                    case "downedAsteroidBoss":
                        return Downeds.DownedAsteroidBoss;
                    case "downedHellfireDragon":
                        return Downeds.DownedHellDragonBoss;
                    case "downedGalacticPeril":
                        return Downeds.DownedGalacticPeril;
                }
            }

            // If the arguments provided don't match anything we wanted to return a value for, we'll return a 'false' boolean.
            // This value can be anything you would like to provide as a default value.
            return false;
        }
    }
}