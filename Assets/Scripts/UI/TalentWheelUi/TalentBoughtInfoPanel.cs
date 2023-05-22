using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class TalentBoughtInfoPanel : PanelActivationManager
{
    [Header("Images")]
    [Space]
    [SerializeField] private Image talentImage;
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI preveousLevelText;
    [SerializeField] private TextMeshProUGUI newLevelText;
    [SerializeField] private TextMeshProUGUI preveousCharacteristicValueText;
    [SerializeField] private TextMeshProUGUI upgradeCharactersiticValueText;

    private void Start()
    {
        HidePanel();
    }

    public void ShowPanelWithTalentData()
    {

    }
}
