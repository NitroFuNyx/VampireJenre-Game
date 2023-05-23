using UnityEngine;

public class TalentWheelUi : MainCanvasPanel
{
    [Header("Info Windows")]
    [Space]
    [SerializeField] private PanelActivationManager talentBougtInfoWindow;

    private void Start()
    {
        talentBougtInfoWindow.HidePanel();
    }

    public override void PanelActivated_ExecuteReaction()
    {

    }

    public override void PanelDeactivated_ExecuteReaction()
    {

    }
}
