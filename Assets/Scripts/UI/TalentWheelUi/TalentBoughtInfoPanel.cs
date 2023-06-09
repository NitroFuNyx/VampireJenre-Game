using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Zenject;

public class TalentBoughtInfoPanel : PanelActivationManager
{
    [Header("Images")]
    [Space]
    [SerializeField] private Image talentImage;
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI charactersisticNameText;
    [SerializeField] private TextMeshProUGUI preveousLevelText;
    [SerializeField] private TextMeshProUGUI newLevelText;
    [SerializeField] private TextMeshProUGUI upgradeCharactersiticValueText;

    private LanguageManager _languageManager;

    private float showInfoPanelDelay = 0.3f;

    private void Start()
    {
        HidePanel();
    }

    #region Zenject
    [Inject]
    private void Construct(LanguageManager languageManager)
    {
        _languageManager = languageManager;
    }
    #endregion Zenject

    public void ShowPanelWithTalentData(TalentDataStruct talentMainData, int newLevel, float upgradePercent)
    {
        talentImage.sprite = talentMainData.talentSprite;

        //charactersisticNameText.text = $"{talentMainData.talentDescribtion}";
        charactersisticNameText.text = $"{_languageManager.SkillsTranslationHandler.GetTranslatedSkillText(talentMainData.talentNameForTranslation)}";
        preveousLevelText.text = $"lvl {newLevel - 1} -";
        newLevelText.text = $"lvl {newLevel}";
        upgradeCharactersiticValueText.text = $"{upgradePercent}%";

        StartCoroutine(ShowInfoPanelCoroutine());
    }

    private IEnumerator ShowInfoPanelCoroutine()
    {
        yield return new WaitForSeconds(showInfoPanelDelay);
        ShowPanel();
    }
}
