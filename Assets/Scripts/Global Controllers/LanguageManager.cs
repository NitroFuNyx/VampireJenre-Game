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
    //[Header("Privacy Policy Texts")]
    //[Space]
    //[SerializeField] private List<PrivacyPolicyText> ppTextsList = new List<PrivacyPolicyText>();

    private LanguageTextsHolder englishTextsHolder = new LanguageTextsHolder();
    private LanguageTextsHolder ukrainianTextsHolder = new LanguageTextsHolder();

    private Dictionary<Languages, LanguageTextsHolder> languagesHoldersDictionary = new Dictionary<Languages, LanguageTextsHolder>();

    private DataPersistanceManager _dataPersistanceManager;

    private int currentLanguageIndex;

    public Languages CurrentLanguage { get => currentLanguage; private set => currentLanguage = value; }

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
            //ChangePrivacyPolicyTexts(language);
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
            //ChangePrivacyPolicyTexts(currentLanguage);
            OnLanguageChanged?.Invoke(languagesHoldersDictionary[currentLanguage]); 
        }
    }

    public void SaveData(GameData data)
    {
        data.languageIndex = (int)currentLanguage;
    }

    //private void ChangePrivacyPolicyTexts(Languages language)
    //{
    //    for(int i = 0; i < ppTextsList.Count; i++)
    //    {
    //        if(ppTextsList[i].Language != language)
    //        {
    //            ppTextsList[i].gameObject.SetActive(false);
    //        }
    //        else
    //        {
    //            ppTextsList[i].gameObject.SetActive(true);
    //        }
    //    }
    //}
}
