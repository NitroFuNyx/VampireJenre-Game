using System;
using System.Collections.Generic;

namespace Save_Load_System
{
    [Serializable]
    public class PlayerStatsData
    {
        public TimeHolder averageTimeSurviving; //common
        public TimeHolder allTimePlayedStandardGamemode; //common
        public TimeHolder allTimePlayed; //general
        public TimeHolder longestTimePlayed; //deathmatch
       
        public int matchesPlayed; //general
        public int killedMonstersAmount; //general
        public int killedMonstersAverage; //general
        
        public List<int> pickedSkillsKeys;
        public List<int> pickedSkillsValues;
        
        public ActiveSkills mostPickableSkill; //common

        public PlayerStatsData()
        {
            averageTimeSurviving = new TimeHolder();
            allTimePlayedStandardGamemode = new TimeHolder();
            allTimePlayed = new TimeHolder();
            matchesPlayed = 0;
            longestTimePlayed = new TimeHolder();
            killedMonstersAmount = 0;
            killedMonstersAverage = 0;
            pickedSkillsKeys = new List<int>();
            pickedSkillsValues = new List<int>();
        }
    }
}