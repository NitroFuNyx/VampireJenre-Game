using System;

[Serializable]
public class GameData
{
    public int languageIndex;
    public int coinsAmount;
    public bool soundMuted;

    public GameData()
    {
        languageIndex = 0;
        coinsAmount = 0;
        soundMuted = false;
    }
}
