using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int languageIndex;
    public int coinsAmount;
    public int gemsAmount;
    public int finishedChaptersCounter;
    public int lastDayPlaying;
    public bool soundMuted;
    public bool canVibrate;
    public bool blockAdsOptionPurchased;
    public bool freeRewardSpinUsed;
    public bool rewardForAdSpinUsed;
    public PlayerBasicCharacteristicsStruct playerCharacteristcsData;
    public List<TalentLevelStruct> skillsLevelsList;

    public GameData()
    {
        languageIndex = 0;
        coinsAmount = 0;
        gemsAmount = 0;
        finishedChaptersCounter = 0;
        lastDayPlaying = 0;
        soundMuted = false;
        canVibrate = true;
        blockAdsOptionPurchased = false;
        freeRewardSpinUsed = false;
        rewardForAdSpinUsed = false;

        playerCharacteristcsData = new PlayerBasicCharacteristicsStruct();
        skillsLevelsList = new List<TalentLevelStruct>();
    }
}
