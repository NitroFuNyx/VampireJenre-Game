using UnityEngine;
using System.Collections.Generic;
using Localization;
using TMPro;

public class GameLevelUITextsLanguageHandler : TextsLanguageUpdateHandler
{
    [Header("Pause Panel Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI pausePanelTitleText;
    [SerializeField] private TextMeshProUGUI pausePanelDescribtionText;
    [SerializeField] private TextMeshProUGUI resumeButtonText;
    [Header("Level Upgrade Panel Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI levelUpgradePanelTitleText;
    [SerializeField] private TextMeshProUGUI levelUpgradePanelDescribtionText;
    [Header("Victory Panel Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI multiplyButtonText;
    [Header("Defeat Panel Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI getUpButtonText;
    [Header("Skill Scroll Panel Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI skillScrollPanelTitleText;
    [SerializeField] private TextMeshProUGUI skillScrollPanelContinueText;
    [SerializeField] private TextMeshProUGUI skillScrollPanelMultiplySkillText;
    [Header("Treasure Chest Panel Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI treasurePanelTitleText;
    [SerializeField] private TextMeshProUGUI treasurePanelGetOneTreasureText;
    [SerializeField] private TextMeshProUGUI treasurePanelGetAllTreasuresText;
    [SerializeField] private List<TextMeshProUGUI> treasurePanelFirstTreasureTextsList = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> treasurePanelSecondTreasureTextsList = new List<TextMeshProUGUI>();
    [Header("Deathmatch Panel Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI deathmatchPanelTitleText;
    [SerializeField] private TextMeshProUGUI bestResultsPanelTitleText;
    [SerializeField] private TextMeshProUGUI roundResultsPanelTitleText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [Header("Common Texts")]
    [Space]
    [SerializeField] private List<TextMeshProUGUI> menuButtonsTextsList = new List<TextMeshProUGUI>();


    public override void OnLanguageChange_ExecuteReaction(LanguageTextsHolder languageHolder)
    {
        pausePanelTitleText.text = languageHolder.data.gameLevelUITexts.pausePanelTexts.panelTitleText;
        pausePanelDescribtionText.text = languageHolder.data.gameLevelUITexts.pausePanelTexts.panelDescribtionText;
        resumeButtonText.text = languageHolder.data.gameLevelUITexts.pausePanelTexts.resumeButtonText;

        levelUpgradePanelTitleText.text = languageHolder.data.gameLevelUITexts.levelUpgradePanelTexts.panelTitleText;
        levelUpgradePanelDescribtionText.text = languageHolder.data.gameLevelUITexts.levelUpgradePanelTexts.panelDescribtionText;

        multiplyButtonText.text = languageHolder.data.gameLevelUITexts.victoryPanelTexts.multiplyButtonText;

        getUpButtonText.text = languageHolder.data.gameLevelUITexts.defeatPanelTexts.getUpButtonText;

        skillScrollPanelTitleText.text = languageHolder.data.gameLevelUITexts.skillScrollPanelTexts.panelTitleText;
        skillScrollPanelContinueText.text = languageHolder.data.gameLevelUITexts.skillScrollPanelTexts.continueButtonText;
        skillScrollPanelMultiplySkillText.text = languageHolder.data.gameLevelUITexts.skillScrollPanelTexts.multiplyLevelButtonText;

        treasurePanelTitleText.text = languageHolder.data.gameLevelUITexts.treasureChestPanelTexts.panelTitleText;
        treasurePanelGetOneTreasureText.text = languageHolder.data.gameLevelUITexts.treasureChestPanelTexts.getOneTreasureButtonText;
        treasurePanelGetAllTreasuresText.text = languageHolder.data.gameLevelUITexts.treasureChestPanelTexts.getAllTreasuresButtonText;

        deathmatchPanelTitleText.text = languageHolder.data.gameLevelUITexts.deathmatchPanelTexts.panelTitleText;
        bestResultsPanelTitleText.text = languageHolder.data.gameLevelUITexts.deathmatchPanelTexts.bestResultsPanelTitleText;
        roundResultsPanelTitleText.text = languageHolder.data.gameLevelUITexts.deathmatchPanelTexts.roundResultsPanelTitleText;
        timeText.text = languageHolder.data.gameLevelUITexts.deathmatchPanelTexts.timeText;
        enemiesText.text = languageHolder.data.gameLevelUITexts.deathmatchPanelTexts.enemiesText;

        for (int i = 0; i < treasurePanelFirstTreasureTextsList.Count; i++)
        {
            treasurePanelFirstTreasureTextsList[i].text = languageHolder.data.gameLevelUITexts.treasureChestPanelTexts.firstTreasureText;
        }

        for (int i = 0; i < treasurePanelSecondTreasureTextsList.Count; i++)
        {
            treasurePanelSecondTreasureTextsList[i].text = languageHolder.data.gameLevelUITexts.treasureChestPanelTexts.secondTreasureText;
        }

        for (int i = 0; i < menuButtonsTextsList.Count; i++)
        {
            menuButtonsTextsList[i].text = languageHolder.data.gameLevelUITexts.menuButtonText;
        }
    }
}
