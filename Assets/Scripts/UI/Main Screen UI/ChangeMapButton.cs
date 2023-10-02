using UnityEngine;
using Zenject;

public class ChangeMapButton : ButtonInteractionHandler
{
    [Header("Button Type")]
    [Space]
    [SerializeField] private SelectionArrowTypes buttonArrowType;

    private MapsManager _mapsManager;

    #region Zenject
    [Inject]
    private void Construct(MapsManager mapsManager)
    {
        _mapsManager = mapsManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        _mapsManager.PressButton_ChangeMap(buttonArrowType);
    }
}
