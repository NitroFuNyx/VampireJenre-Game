using System;
using System.Collections.Generic;
using Save_Load_System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class GameData
{
    public int languageIndex;
    public Secureint coinsAmount;
    public Secureint gemsAmount;
    public int finishedChaptersCounter;
    public int lastDayPlaying;
    public int daysInARow;
    public bool soundMuted;
    public bool canVibrate;
    public bool blockAdsOptionPurchased;
    public bool freeRewardSpinUsed;
    public bool rewardForAdSpinUsed;
    public bool deathMatchModeUsedAtCurrentDay;
    public PlayerClasses lastPlayedClass;
    public List<PlayerBasicCharacteristicsStruct> playerClasesDataList;
    public List<TalentLevelStruct> skillsLevelsList;
    public PlayerStatsData playerStatsData;

    public GameData()
    {
        languageIndex = 0;
        coinsAmount = new Secureint();
        gemsAmount = new Secureint();
        finishedChaptersCounter = 0;
        lastDayPlaying = 0;
        soundMuted = false;
        canVibrate = true;
        blockAdsOptionPurchased = false;
        freeRewardSpinUsed = false;
        rewardForAdSpinUsed = false;
        deathMatchModeUsedAtCurrentDay = false;
        lastPlayedClass = PlayerClasses.Knight;

        playerClasesDataList = new List<PlayerBasicCharacteristicsStruct>();
        skillsLevelsList = new List<TalentLevelStruct>();
        playerStatsData = new PlayerStatsData();
    }
   
    
}
