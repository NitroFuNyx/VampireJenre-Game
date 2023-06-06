using System.Collections.Generic;
using UnityEngine;
using System;
using Localization;
using Zenject;
using TMPro;

public class LanguageManager : MonoBehaviour, IDataPersistance
{
    [Header("Texts JSON")]
    [Space]
    [SerializeField] private TextAsset englishTextsJSON;
    [SerializeField] private TextAsset ukrainianTextsJSON;
    [Header("Current Language")]
    [Space]
    [SerializeField] private Languages currentLanguage;
    [Header("Translation Handlers")]
    [Space]
    [SerializeField] private SkillsTranslationHandler skillsTranslationHandler;

    private LanguageTextsHolder englishTextsHolder = new LanguageTextsHolder();
    private LanguageTextsHolder ukrainianTextsHolder = new LanguageTextsHolder();

    private Dictionary<Languages, LanguageTextsHolder> languagesHoldersDictionary = new Dictionary<Languages, LanguageTextsHolder>();

    private DataPersistanceManager _dataPersistanceManager;

    private int currentLanguageIndex;

    public Languages CurrentLanguage { get => currentLanguage; private set => currentLanguage = value; }
    public LanguageTextsHolder EnglishTextsHolder { get => englishTextsHolder; private set => englishTextsHolder = value; }
    public LanguageTextsHolder UkrainianTextsHolder { get => ukrainianTextsHolder; private set => ukrainianTextsHolder = value; }
    public SkillsTranslationHandler SkillsTranslationHandler { get => skillsTranslationHandler; }

    #region Events Declaration
    public event Action<LanguageTextsHolder> OnLanguageChanged;
    #endregion Events Declaration

    private void Awake()
    {
        FillLanguageTextHolders();
        FillLanguagesHoldersDictionary();
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
    }
    #endregion Zenject

    public void ChangeLanguage()
    {
        currentLanguageIndex = (int)currentLanguage;
        currentLanguageIndex++;
        if(currentLanguageIndex == Enum.GetValues(typeof(Languages)).Length)
        {
            currentLanguageIndex = 0;
        }
        Languages newLanguage = (Languages)currentLanguageIndex;

        if(languagesHoldersDictionary.ContainsKey(newLanguage))
        {
            currentLanguage = newLanguage;
            _dataPersistanceManager.SaveGame();
            OnLanguageChanged?.Invoke(languagesHoldersDictionary[newLanguage]);
        }
    }

    private LanguageTextsHolder SetLanguageTextHolder(TextAsset json)
    {
        LanguageTextsHolder holder = new LanguageTextsHolder();
        holder = JsonUtility.FromJson<LanguageTextsHolder>(json.text);

        return holder;
    }

    private void FillLanguageTextHolders()
    {
        englishTextsHolder = SetLanguageTextHolder(englishTextsJSON);
        ukrainianTextsHolder = SetLanguageTextHolder(ukrainianTextsJSON);
    }

    private void FillLanguagesHoldersDictionary()
    {
        languagesHoldersDictionary.Add(Languages.English, englishTextsHolder);
        languagesHoldersDictionary.Add(Languages.Ukrainian, ukrainianTextsHolder);
    }

    public void LoadData(GameData data)
    {
        if (languagesHoldersDictionary.ContainsKey((Languages)data.languageIndex))
        {
            currentLanguage = (Languages)data.languageIndex;
            OnLanguageChanged?.Invoke(languagesHoldersDictionary[currentLanguage]); 
        }
    }

    public void SaveData(GameData data)
    {
        data.languageIndex = (int)currentLanguage;
    }
}
