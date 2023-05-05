using UnityEngine;

public abstract class MainCanvasPanel : PanelActivationManager
{
    [Header("Panel Data")]
    [Space]
    [SerializeField] private UIPanels panelType;

    public UIPanels PanelType { get => panelType; set => panelType = value; }

    public abstract void PanelActivated_ExecuteReaction();

    public abstract void PanelDeactivated_ExecuteReaction();
}
