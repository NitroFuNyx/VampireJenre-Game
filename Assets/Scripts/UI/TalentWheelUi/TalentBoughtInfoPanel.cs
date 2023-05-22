using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

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
    [SerializeField] private TextMeshProUGUI preveousCharacteristicValueText;
    [SerializeField] private TextMeshProUGUI upgradeCharactersiticValueText;

    private float showInfoPanelDelay = 0.3f;

    private void Start()
    {
        HidePanel();
    }

    public void ShowPanelWithTalentData(TalentDataStruct talentMainData, int newLevel)
    {
        charactersisticNameText.text = $"{talentMainData.talentDescribtion}";
        preveousLevelText.text = $"lvl + {newLevel - 1}";
        newLevelText.text = $"lvl {newLevel}";

        StartCoroutine(ShowInfoPanelCoroutine());
    }

    private IEnumerator ShowInfoPanelCoroutine()
    {
        yield return new WaitForSeconds(showInfoPanelDelay);
        ShowPanel();
    }
}
