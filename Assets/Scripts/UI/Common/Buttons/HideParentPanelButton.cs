using UnityEngine;

public class HideParentPanelButton : ButtonInteractionHandler
{
    [Header("External References")]
    [Space]
    [SerializeField] private PanelActivationManager parentPanel;

    public override void ButtonActivated()
    {
        parentPanel.HidePanel();
    }
}
