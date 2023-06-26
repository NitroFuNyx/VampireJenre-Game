using UnityEngine;
using Zenject;

public class ChangeVisibleCharacterButton : ButtonInteractionHandler
{
    [Header("Button Type")]
    [Space]
    [SerializeField] private SelectionArrowTypes buttonType;

    private CharacterSelectionUI _characterSelectionUI;

    #region Zenject
    [Inject]
    private void Construct(CharacterSelectionUI characterSelectionUI)
    {
        _characterSelectionUI = characterSelectionUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _characterSelectionUI.ChangeVisibleCharacterButtonPressed_ExecuteReaction(buttonType);
    }
}
