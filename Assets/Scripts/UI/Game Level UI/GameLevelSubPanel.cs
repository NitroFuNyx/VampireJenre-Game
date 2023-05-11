using UnityEngine;

public class GameLevelSubPanel : PanelActivationManager
{
    [Header("Panel Data")]
    [Space]
    [SerializeField] private GameLevelPanels panelType;

    public GameLevelPanels PanelType { get => panelType; }
}
