using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int languageIndex;
    public int coinsAmount;
    public int gemsAmount;
    public bool soundMuted;
    public bool canVibrate;
    public PlayerBasicCharacteristicsStruct playerCharacteristcsData;

    public GameData()
    {
        languageIndex = 0;
        coinsAmount = 0;
        gemsAmount = 0;
        soundMuted = false;
        canVibrate = true;

        playerCharacteristcsData = new PlayerBasicCharacteristicsStruct();
    }
}
