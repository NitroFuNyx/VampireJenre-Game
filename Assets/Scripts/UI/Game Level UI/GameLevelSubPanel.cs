using UnityEngine;

public class GameLevelSubPanel : PanelActivationManager
{
    [Header("Panel Data")]
    [Space]
    [SerializeField] protected GameLevelPanels panelType;

    public GameLevelPanels PanelType { get => panelType; }
}
