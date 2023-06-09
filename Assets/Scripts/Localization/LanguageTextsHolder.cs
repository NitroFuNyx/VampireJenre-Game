using System;

namespace Localization
{
    [Serializable]
    public class LanguageTextsHolder
    {
        public Data data;
    }

    [Serializable]
    public class Data
    {
        public MainScreenUIData mainscreenUITexts;
        public SettingsUIData settingsUITexts;
        public RewardsUIData rewardsUITexts;
        public GameLevelUIData gameLevelUITexts;
        public SkillsCharacteristicsData skillCharacteristicsTexts;
        public TalentsUIData talentsUITexts;
        public RoadmapUIData roadmapUITexts;
        public SkillsInfoUIData skillsInfoUITexts;
        public AdsBlockerPurchasedUIData adsBlockerPurchasedUITexts;
    }

    [Serializable]
    public class MainScreenUIData
    {
        public string playButtonText;
    }

    [Serializable]
    public class SettingsUIData
    {
        public string changeLanguageButtonText;
        public string storyButtonText;
        public string privacyPolicyButtonText;
        public string backButtonText;
    }

    [Serializable]
    public class RewardsUIData
    {
        public string spinButtonText;
    }

    [Serializable]
    public class GameLevelUIData
    {
        public PausePanelData pausePanelTexts;
        public LevelUpgradePanelData levelUpgradePanelTexts;
        public VictoryPanelData victoryPanelTexts;
        public DefeatPanelData defeatPanelTexts;
        public string menuButtonText;
    }

    [Serializable]
    public class PausePanelData
    {
        public string panelTitleText;
        public string panelDescribtionText;
        public string resumeButtonText;
    }

    [Serializable]
    public class LevelUpgradePanelData
    {
        public string panelTitleText;
        public string panelDescribtionText;
    }

    [Serializable]
    public class VictoryPanelData
    {
        public string multiplyButtonText;
    }

    [Serializable]
    public class DefeatPanelData
    {
        public string getUpButtonText;
    }

    [Serializable]
    public class SkillsCharacteristicsData
    {
        public string damageText;
        public string rangeText;
        public string widthText;
        public string cooldownText;
        public string projectilesAmountText;
        public string postEffectDurationText;
        public string jumpsAmountText;
        public string sizeText;
        public string movementSpeedText;
        public string incomeDamageText;
        public string regenerationText;
        public string critChanceText;
        public string critPowerText;
    }

    [Serializable]
    public class TalentsUIData
    {
        public string upgradeButtonText;
        public string notEnoughCoinsText;
        public string allTalentsUpgradedText;
    }

    [Serializable]
    public class RoadmapUIData
    {
        public string titleText;
        public string shopPanelTitleText;
        public string shopPanelDescriptionText;
        public string charactersPanelTitleText;
        public string charactersPanelDescriptionText;
        public string mapsPanelTitleText;
        public string mapsPanelDescriptionText;
    }

    [Serializable]
    public class SkillsInfoUIData
    {
        public string activeSkillsButtonText;
        public string passiveSkillsButtonText;

        public string singleShotText;
        public string chainLightningText;
        public string magicAuraText;
        public string forceWaveText;
        public string fireballsText;
        public string lightningBoltText;
        public string pulseAuraText;
        public string allDirectionsShotsText;
        public string weaponStrikeText;
        public string meteorText;

        public string critPowerText;
        public string movementSpeedText;
        public string itemDropChanceText;
        public string coinsSurplusText;
        public string decreaseIncomeDamageText;
        public string regenerationText;
        public string critChanceText;
        public string increaseRangeText;
        public string increaseDamageText;
        public string increaseProjectilesAmountText;
    }

    [Serializable]
    public class AdsBlockerPurchasedUIData
    {
        public string titleText;
    }
}
