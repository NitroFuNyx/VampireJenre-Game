using UnityEngine;

public class SettingsUI : MainCanvasPanel
{
    [Header("Panels")]
    [Space]
    [SerializeField] private PanelActivationManager mainSettingsPanel;

    public override void PanelActivated_ExecuteReaction()
    {
        mainSettingsPanel.ShowPanel();
    }

    public override void PanelDeactivated_ExecuteReaction()
    {
        
    }
}
