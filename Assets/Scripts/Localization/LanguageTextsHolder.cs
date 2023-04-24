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
    }

    [Serializable]
    public class MainScreenUIData
    {
        public string titleText;
    }
}
