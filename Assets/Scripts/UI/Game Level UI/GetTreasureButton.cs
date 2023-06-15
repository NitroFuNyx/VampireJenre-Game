using UnityEngine;
using Zenject;

public class GetTreasureButton : ButtonInteractionHandler
{
    [Header("Button Type")]
    [Space]
    [SerializeField] private bool getAllTreasures = false;

    private TreasureChestInfoPanel _treasureChestInfoPanel;

    #region Zenject
    [Inject]
    private void Construct(TreasureChestInfoPanel treasureChestInfoPanel)
    {
        _treasureChestInfoPanel = treasureChestInfoPanel;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _treasureChestInfoPanel.CollectTreasures(getAllTreasures);
    }
}
