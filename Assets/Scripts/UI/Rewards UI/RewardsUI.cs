using UnityEngine;

public class RewardsUI : MainCanvasPanel
{
    [Header("Info Windows")]
    [Space]
    [SerializeField] private PanelActivationManager rawardReceivedInfoWindow;

    private void Start()
    {
        rawardReceivedInfoWindow.HidePanel();
    }

    public override void PanelActivated_ExecuteReaction()
    {
        
    }

    public override void PanelDeactivated_ExecuteReaction()
    {
        
    }
}
