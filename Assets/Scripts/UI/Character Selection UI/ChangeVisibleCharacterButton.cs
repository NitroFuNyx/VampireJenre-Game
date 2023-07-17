using UnityEngine;
using Zenject;

public class ChangeVisibleCharacterButton : ButtonInteractionHandler
{
    [Header("Button Type")]
    [Space]
    [SerializeField] private ShowNextCharacterButtonsTypes buttonType;
    [Header("Arrow Button Data")]
    [Space]
    [SerializeField] private SelectionArrowTypes arrowType;
    [Header("Fixed Character Button Data")]
    [Space]
    [SerializeField] private PlayerClasses playerClass;

    private CharacterSelectionUI _characterSelectionUI;

    public PlayerClasses PlayerClass { get => playerClass; set => playerClass = value; }

    private void Start()
    {
        if(buttonType == ShowNextCharacterButtonsTypes.FixedCharacterButton)
        {
            _characterSelectionUI = FindObjectOfType<CharacterSelectionUI>();
        }
    }

    #region Zenject
    [Inject]
    private void Construct(CharacterSelectionUI characterSelectionUI)
    {
        if(buttonType == ShowNextCharacterButtonsTypes.ArrowButton)
        _characterSelectionUI = characterSelectionUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _characterSelectionUI.ChangeVisibleCharacterButtonPressed_ExecuteReaction(buttonType, arrowType, playerClass);
    }
}
