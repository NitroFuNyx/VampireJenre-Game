using UnityEngine;

public class TreasureItemInfoPanel : MonoBehaviour
{
    [Header("Panels Canvas Groups")]
    [Space]
    [SerializeField] private CanvasGroup skillsPanelCanvasGroup;
    [SerializeField] private CanvasGroup resourcePanelCanvasGroup;
    [Header("Panels")]
    [Space]
    [SerializeField] private SkillUpgradeDisplayPanel skillPanel;
    [SerializeField] private TreasureResourceInfoPanel resourcePanel;

    private void Start()
    {
        ChangePanelActivationState(skillsPanelCanvasGroup, false);
        ChangePanelActivationState(resourcePanelCanvasGroup, false);
    }

    public void UpdateSkillPanelData(UpgradeSkillData upgradeSkillData)
    {
        ChangePanelActivationState(skillsPanelCanvasGroup, true);
        ChangePanelActivationState(resourcePanelCanvasGroup, false);

        skillPanel.UpdateUI(upgradeSkillData);
    }

    public void UpdateResourcePanelData(TreasureChestResourceDataStruct resource)
    {
        ChangePanelActivationState(skillsPanelCanvasGroup, false);
        ChangePanelActivationState(resourcePanelCanvasGroup, true);

        resourcePanel.UpdateResourceData(resource);
    }

    private void ChangePanelActivationState(CanvasGroup canvasGroup, bool enabled)
    {
        if(enabled)
        {
            canvasGroup.alpha = 1f;
        }
        else
        {
            canvasGroup.alpha = 0f;
        }

        canvasGroup.interactable = false;
        canvasGroup.interactable = false;
    }
}
