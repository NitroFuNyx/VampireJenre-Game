using System;
using System.Collections.Generic;
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
        coinsAmount = new Secureint();
        gemsAmount = new Secureint();
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
   
    public struct Secureint
    {
      private int valueOffset;
        private int valueAmount;

        public Secureint(int coinsValue)
        {
            valueOffset = Random.Range(-1000, 1000);
            valueAmount = coinsValue + valueOffset;
        }

        public int GetValue()
        {
            return valueAmount - valueOffset;
        }

        public override string ToString()
        {
            return GetValue().ToString();
        }

        public static Secureint operator +(Secureint i1, Secureint i2)
        {
            return new Secureint(i1.GetValue() + i2.GetValue());
        }
        public static Secureint operator -(Secureint i1, Secureint i2)
        {
            return new Secureint(i1.GetValue() - i2.GetValue());
        }
        public static Secureint operator *(Secureint i1, Secureint i2)
        {
            return new Secureint(i1.GetValue() * i2.GetValue());
        }
        public static Secureint operator /(Secureint i1, Secureint i2)
        {
            return new Secureint(i1.GetValue() / i2.GetValue());
        }
        
        
    }
}
