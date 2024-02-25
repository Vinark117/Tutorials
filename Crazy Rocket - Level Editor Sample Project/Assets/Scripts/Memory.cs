using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VinTools.Tweening;

public static class Memory
{
    public struct GameValues
    {
        public enum EndlessMode { RocketLabs, Hardcore, Impossible }
        public enum LevelMode { Vanilla, Custom }
        public enum GameMode { Level, Endless }

        public static EndlessMode CurrentEndlessMode;
        public static GameMode CurrentLevelEditorMode;
        public static LevelMode CurrentLevelMode;
        public static GameMode CurrentGameMode;

        public static LevelData levelToLoad;
        public static LevelData levelToTest;
        public static EndlessSection sectionToTest;
        public static bool levelEditorTesting = false;
        public static bool returnToTitleAfterTesting = false;
        public static bool onlyCustomSections = false;
        public static bool playDialogue = false;

        public static int currentLvl = -1;
        public static bool returnToPlayScreen = false;
        public static float playScreenPos;

        public static int GetSelectedTab(Transform parent)
        {
                return PlayerPrefs.GetInt($"{parent.ToString()}_selectedTab", 0);
        }
        public static void SetSelectedTab(Transform parent, int value)
        {

            PlayerPrefs.SetInt($"{parent.ToString()}_selectedTab", value);
        }

        public static int mainMenuBuildIndex = 0;
        public static int levelBuildIndex = 1;
        public static int levelEditorBuildIndex = 2;
        public static Color fadeColor = Color.HSVToRGB(240f / 360f, 27f / 100f, 21f / 100f);
        public static EaseCurve sceneTransitionType = EaseCurve.cubicInOut;
    }

    public struct Values
    {
        public static int ScrewAmount
        {
            get
            {
                return PlayerPrefs.GetInt("ScrewAmount", 0);
            }
            set
            {
                PlayerPrefs.SetInt("ScrewAmount", value);
            }
        }

        public static int CurrentLevel
        {
            get
            {
                return PlayerPrefs.GetInt("CurrentCampaignLevel", 0);
            }
            set
            {
                //if (CurrentLevel < value) 
                    PlayerPrefs.SetInt("CurrentCampaignLevel", value);
            }
        }
    }

    public struct HighScores
    {
        public static int CurrentGameMode
        {
            get
            {
                switch (GameValues.CurrentEndlessMode)
                {
                    case GameValues.EndlessMode.RocketLabs:
                        return RocketLabs;
                    case GameValues.EndlessMode.Hardcore:
                        return Hardcore;
                    case GameValues.EndlessMode.Impossible:
                        return Impossible;
                    default:
                        return 0;
                }
            }
            set
            {
                switch (GameValues.CurrentEndlessMode)
                {
                    case GameValues.EndlessMode.RocketLabs:
                        RocketLabs = value;
                        break;
                    case GameValues.EndlessMode.Hardcore:
                        Hardcore = value;
                        break;
                    case GameValues.EndlessMode.Impossible:
                        Impossible = value;
                        break;
                }
            }
        }

        public static int RocketLabs
        {
            get
            {
                return PlayerPrefs.GetInt("HighScores-RocketLabs", 0);
            }
            set
            {
                PlayerPrefs.SetInt("HighScores-RocketLabs", value);
            }
        }
        public static int Hardcore
        {
            get
            {
                return PlayerPrefs.GetInt("HighScores-Hardcore", 0);
            }
            set
            {
                PlayerPrefs.SetInt("HighScores-Hardcore", value);
            }
        }
        public static int Impossible
        {
            get
            {
                return PlayerPrefs.GetInt("HighScores-Impossible", 0);
            }
            set
            {
                PlayerPrefs.SetInt("HighScores-Impossible", value);
            }
        }
    }

    public struct Settings
    {
        public enum ControlType { Mouse, Touch, Gamepad }

        public static ControlType ControlScheme
        {
            get
            {
                int defaultScheme = 1;
                //if (Project.isCurrentPlatformPC) defaultScheme = 0;
                //if (Project.isCurrentPlatformConsole) defaultScheme = 2;

                switch (PlayerPrefs.GetInt("Settings_ControlScheme", defaultScheme))
                {
                    case 0:
                        return ControlType.Mouse;
                    case 1:
                        return ControlType.Touch;
                    case 2:
                        return ControlType.Gamepad;
                    default:
                        return ControlType.Mouse;
                }
            }
            set
            {
                int intvalue = 0;

                switch (value)
                {
                    case ControlType.Mouse:
                        intvalue = 0;
                        break;
                    case ControlType.Touch:
                        intvalue = 1;
                        break;
                    case ControlType.Gamepad:
                        intvalue = 2;
                        break;
                    default:
                        intvalue = 0;
                        break;
                }

                PlayerPrefs.SetInt("Settings_ControlScheme", intvalue);
            }
        }
        public static bool AutomaticPowerups
        {
            get
            {
                return PlayerPrefs.GetInt("Settings_AutomaticPowerups", 1) == 1 ? true : false;
            }
            set
            {
                PlayerPrefs.SetInt("Settings_AutomaticPowerups", value ? 1 : 0);
            }
        }

        /*public static float Volume_Master
        {
            get
            {
                return PlayerPrefs.GetFloat("Settings_Volume_Master", 1f);
            }
            set
            {
                PlayerPrefs.SetFloat("Settings_Volume_Master", value);
            }
        }
        public static float Volume_SFX
        {
            get
            {
                return PlayerPrefs.GetFloat("Settings_Volume_Gameplay", 1f);
            }
            set
            {
                PlayerPrefs.SetFloat("Settings_Volume_Gameplay", value);
            }
        }
        public static float Volume_UI
        {
            get
            {
                return PlayerPrefs.GetFloat("Settings_Volume_UI", 1f);
            }
            set
            {
                PlayerPrefs.SetFloat("Settings_Volume_UI", value);
            }
        }
        public static float Volume_Music
        {
            get
            {
                return PlayerPrefs.GetFloat("Settings_Volume_Music", 1f);
            }
            set
            {
                PlayerPrefs.SetFloat("Settings_Volume_Music", value);
            }
        }*/
    }
    public struct Statistics
    {
        //time spent
        public static float timeInApp = 0;
        public static float timePlayed = 0;
        public static float timeInLevelEditor = 0;
        public static float timeBoosted = 0;

        //screws
        public static int screwsTotal;
        public static int screwsPickedUp;
        public static int screwsGotFromTasks;
        public static int screwsGotFromLevelRewards;
        public static int screwsGotFromAds;
        public static int screwsSpent;
        public static int screwsSpentOnUpgrades;
        public static int screwsSpentOnCustomization;

        //fuel
        public static int fuelsPickedUp;

        //collision
        public static float damageTaken = 0;
        public static float collisionDamage = 0;
        public static int timesTookDamage = 0;
        public static int timesCollided = 0;

        //deaths
        public static int totalDeaths = 0;
        public static int deathsByNoFuel = 0;
        public static int deathsByLazer = 0;
        public static int deathsByCollision = 0;

        //powerups
        public static int timesUsedPowerupShield = 0;
        public static int timesUsedPowerupSpring = 0;
        public static int timesUsedPowerupExtrafuel = 0;

        //upgrades
        public static int totalUpgradesBought = 0;
    }


    #region upgrades
    /// <summary>
    /// Values of each level of an upgrade
    /// </summary>
    public struct UpgradeAmount
    {
        public static float[] RocketFuelTank { get; private set; } = { 200, 250, 300, 350, 400, 450, 500, 550, 600, 650, 700 };
        public static float[] RocketShieldDurability { get; private set; } = { 100, 125, 150, 175, 200, 250, 300, 350, 400, 450, 500 };
        public static float[] RocketShieldRecharge { get; private set; } = { 0.1f, 0.15f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f };
        public static float[] ShieldRecharge { get; private set; } = { 500, 480, 460, 440, 420, 400, 380, 360, 340, 320, 300 };
        public static float[] ShieldUse { get; private set; } = { 400, 450, 500, 550, 600, 650, 700, 750, 800, 900, 1000 };
        public static float[] CatapultRecharge { get; private set; } = { 200, 180, 160, 140, 120, 100, 90, 80, 70, 60, 50 };
        public static float[] Extrafuel { get; private set; } = { 100, 120, 140, 160, 180, 200, 220, 240, 260, 280, 300 };
        public static float[] ExtrafuelRecharge { get; private set; } = { 40, 38, 36, 34, 32, 30, 28, 26, 24, 22, 20 };
        public static float[] MagnetStrenght { get; private set; } = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        public static float[] ScoreMultiplier { get; private set; } = { 1, 1.5f, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        public static float[] ScrewChance { get; private set; } = { 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
    }
    /// <summary>
    /// Costs to upgrade
    /// </summary>
    public struct UpgradeCost
    {
        public static int[] RocketFuelTank { get; private set; } = { 0, 300, 700, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 0 };
        public static int[] RocketShieldDurability { get; private set; } = { 0, 50, 100, 200, 300, 500, 1000, 2000, 3000, 4000, 5000, 0 };
        public static int[] RocketShieldRecharge { get; private set; } = { 0, 50, 100, 300, 500, 1000, 2000, 3000, 4000, 5000, 6000, 0 };
        public static int[] ShieldRecharge { get; private set; } = { 100, 300, 500, 750, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 0 };
        public static int[] ShieldUse { get; private set; } = { 0, 200, 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 5000, 0 };
        public static int[] CatapultRecharge { get; private set; } = { 0, 300, 500, 750, 1000, 1200, 1400, 1600, 1800, 2000, 2500, 0 };
        public static int[] Extrafuel { get; private set; } = { 0, 50, 100, 150, 200, 300, 500, 750, 1000, 2000, 3000, 0 };
        public static int[] ExtrafuelRecharge { get; private set; } = { 0, 300, 500, 750, 1000, 1250, 1500, 1750, 2000, 2500, 3000, 0 };
        public static int[] MagnetStrenght { get; private set; } = { 0, 50, 100, 200, 300, 500, 1000, 1500, 2000, 3000, 5000, 0 };
        public static int[] ScoreMultiplier { get; private set; } = { 0, 500, 800, 1200, 2000, 3000, 5000, 7000, 10000, 12000, 15000, 0 };
        public static int[] ScrewChance { get; private set; } = { 0, 50, 100, 300, 500, 1000, 2000, 3000, 4000, 5000, 6000, 0 };
    }
    public struct Upgrades
    {
        public static int RocketFuelTankLvl
        {
            get
            {
                return PlayerPrefs.GetInt("RocketFuelTankLVL", 0);
            }
            set
            {
                PlayerPrefs.SetInt("RocketFuelTankLVL", value);
            }
        }
        public static int RocketShieldDurabilityLvl
        {
            get
            {
                return PlayerPrefs.GetInt("RocketShieldDurabilityLVL", 0);
            }
            set
            {
                PlayerPrefs.SetInt("RocketShieldDurabilityLVL", value);
            }
        }
        public static int RocketShieldRechargeLvl
        {
            get
            {
                return PlayerPrefs.GetInt("RocketShieldRechargeLVL", 0);
            }
            set
            {
                PlayerPrefs.SetInt("RocketShieldRechargeLVL", value);
            }
        }
        public static int ShieldRechargeLvl
        {
            get
            {
                return PlayerPrefs.GetInt("ShieldRechargeLVL", 0);
            }
            set
            {
                PlayerPrefs.SetInt("ShieldRechargeLVL", value);
            }
        }
        public static int ShieldUseLvl
        {
            get
            {
                return PlayerPrefs.GetInt("ShieldUseLVL", 0);
            }
            set
            {
                PlayerPrefs.SetInt("ShieldUseLVL", value);
            }
        }
        public static int CatapultRechargeLvl
        {
            get
            {
                return PlayerPrefs.GetInt("CatapultRechargeLVL", 0);
            }
            set
            {
                PlayerPrefs.SetInt("CatapultRechargeLVL", value);
            }
        }
        public static int ExtrafuelLvl
        {
            get
            {
                return PlayerPrefs.GetInt("ExtrafuelLVL", 0);
            }
            set
            {
                PlayerPrefs.SetInt("ExtrafuelLVL", value);
            }
        }
        public static int ExtrafuelRechargeLvl
        {
            get
            {
                return PlayerPrefs.GetInt("ExtrafuelRechargeLVL", 0);
            }
            set
            {
                PlayerPrefs.SetInt("ExtrafuelRechargeLVL", value);
            }
        }
        public static int MagnetStrenghtLvl
        {
            get
            {
                return PlayerPrefs.GetInt("MagnetStrenghtLVL", 0);
            }
            set
            {
                PlayerPrefs.SetInt("MagnetStrenghtLVL", value);
            }
        }
        public static int ScoreMultiplierLvl
        {
            get
            {
                return PlayerPrefs.GetInt("ScoreMultiplierLVL", 0);
            }
            set
            {
                PlayerPrefs.SetInt("ScoreMultiplierLVL", value);
            }
        }
        public static int ScrewChanceLvl
        {
            get
            {
                return PlayerPrefs.GetInt("ScrewChanceLVL", 0);
            }
            set
            {
                PlayerPrefs.SetInt("ScrewChanceLVL", value);
            }
        }

        public static bool IsShieldUnlocked
        {
            get
            {
                return PlayerPrefs.GetInt("IsPowerupUnlocked_Shield", 0) == 1 ? true : false;
            }
            set
            {
                PlayerPrefs.SetInt("IsPowerupUnlocked_Shield", value ? 1 : 0);
            }
        }
        public static bool IsSpringUnlocked
        {
            get
            {
                return PlayerPrefs.GetInt("IsPowerupUnlocked_Spring", 0) == 1 ? true : false;
            }
            set
            {
                PlayerPrefs.SetInt("IsPowerupUnlocked_Spring", value ? 1 : 0);
            }
        }
        public static bool IsExtrafuelUnlocked
        {
            get
            {
                return PlayerPrefs.GetInt("IsPowerupUnlocked_Fuel", 0) == 1 ? true : false;
            }
            set
            {
                PlayerPrefs.SetInt("IsPowerupUnlocked_Fuel", value ? 1 : 0);
            }
        }

        public static void ResetAll()
        {
            PlayerPrefs.SetInt("RocketFuelTankLVL", 0);
            PlayerPrefs.SetInt("RocketShieldDurabilityLVL", 0);
            PlayerPrefs.SetInt("RocketShieldRechargeLVL", 0);
            PlayerPrefs.SetInt("ShieldRechargeLVL", 0);
            PlayerPrefs.SetInt("ShieldUseLVL", 0);
            PlayerPrefs.SetInt("CatapultRechargeLVL", 0);
            PlayerPrefs.SetInt("ExtrafuelLVL", 0);
            PlayerPrefs.SetInt("ExtrafuelRechargeLVL", 0);
            PlayerPrefs.SetInt("MagnetStrenghtLVL", 0);
            PlayerPrefs.SetInt("ScoreMultiplierLVL", 0);
            PlayerPrefs.SetInt("ScrewChanceLVL", 0);

            PlayerPrefs.SetInt("IsPowerupUnlocked_Shield", 0);
            PlayerPrefs.SetInt("IsPowerupUnlocked_Spring", 0);
            PlayerPrefs.SetInt("IsPowerupUnlocked_Fuel", 0);
        }
    }

    /// <summary>
    /// Returns the currently unlocked value of the upgrades
    /// </summary>
    public struct GetCurrentUpgradeAmount
    {
        public static float RocketFuelTank
        {
            get
            {
                return UpgradeAmount.RocketFuelTank[Upgrades.RocketFuelTankLvl];
            }
        }
        public static float RocketShieldDurability
        {
            get
            {
                return UpgradeAmount.RocketShieldDurability[Upgrades.RocketShieldDurabilityLvl];
            }
        }
        public static float RocketShieldRecharge
        {
            get
            {
                return UpgradeAmount.RocketShieldRecharge[Upgrades.RocketShieldRechargeLvl];
            }
        }
        public static float ShieldRecharge
        {
            get
            {
                return UpgradeAmount.ShieldRecharge[Upgrades.ShieldRechargeLvl];
            }
        }
        public static float ShieldUse
        {
            get
            {
                return UpgradeAmount.ShieldUse[Upgrades.ShieldUseLvl];
            }
        }
        public static float CatapultRecharge
        {
            get
            {
                return UpgradeAmount.CatapultRecharge[Upgrades.CatapultRechargeLvl];
            }
        }
        public static float Extrafuel
        {
            get
            {
                return UpgradeAmount.Extrafuel[Upgrades.ExtrafuelLvl];
            }
        }
        public static float ExtrafuelRecharge
        {
            get
            {
                return UpgradeAmount.ExtrafuelRecharge[Upgrades.ExtrafuelRechargeLvl];
            }
        }
        public static float MagnetStrenght
        {
            get
            {
                return UpgradeAmount.MagnetStrenght[Upgrades.MagnetStrenghtLvl];
            }
        }
        public static float ScoreMultiplier
        {
            get
            {
                return UpgradeAmount.ScoreMultiplier[Upgrades.ScoreMultiplierLvl];
            }
        }
        public static float ScrewChance
        {
            get
            {
                return UpgradeAmount.ScrewChance[Upgrades.ScrewChanceLvl];
            }
        }
    }
    #endregion

    public struct GPGS
    {
        public static int LastRocketLabsScore
        {
            get
            {
                return PlayerPrefs.GetInt("GPGS_LastRocketLabsScore", 0);
            }
            set
            {
                PlayerPrefs.SetInt("GPGS_LastRocketLabsScore", value);
            }
        }
        public static int LastHardcoreScore
        {
            get
            {
                return PlayerPrefs.GetInt("GPGS_LastHardcoreScore", 0);
            }
            set
            {
                PlayerPrefs.SetInt("GPGS_LastHardcoreScore", value);
            }
        }
        public static int LastImpossibleScore
        {
            get
            {
                return PlayerPrefs.GetInt("GPGS_LastImpossibleScore", 0);
            }
            set
            {
                PlayerPrefs.SetInt("GPGS_LastImpossibleScore", value);
            }
        }
    }
}
