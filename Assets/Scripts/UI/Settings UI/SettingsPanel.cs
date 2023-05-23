using UnityEngine;

public class SettingsPanel : PanelActivationManager
{
    [Header("Panel Data")]
    [Space]
    [SerializeField] private SettingsPanels panelType;

    public SettingsPanels PanelType { get => panelType; }
}
