using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;

public class CharacterSelectionUI : MainCanvasPanel
{
    [Header("Clases Data")]
    [Space]
    [SerializeField] private PlayerClassesDataSO classesDataSO;
    [Header("Images")]
    [Space]
    [SerializeField] private Image characterImage;
    [Header("Prefabs")]
    [Space]
    [SerializeField] private CharacterDisplayPanel characterDisplayPanelPrefab;
    [Header("Internal References")]
    [Space]
    [SerializeField] private Transform characterSelectionPanel;
    [SerializeField] private PanelActivationManager baseCharacatersInfoPanel;
    [SerializeField] private PanelActivationManager characterUpgradePanel;

    private PlayerCharacteristicsManager _playerCharacteristicsManager;
    private PlayerCharactersManager _playerCharactersManager;

    private ChangeActiveCharacterButton button_ChangeActiveCharacter;

    private List<PlayerClasses> charactersList = new List<PlayerClasses>();
    private Dictionary<PlayerClasses, Sprite> charactersDictionary = new Dictionary<PlayerClasses, Sprite>();

    private PlayerClasses visibleCharacter;

    private void Start()
    {
        FillCharactersListAndDictionary();
        FillCharactersDisplayPanels();
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerCharacteristicsManager playerCharacteristicsManager,
                           ChangeActiveCharacterButton changeActiveCharacterButton, PlayerCharactersManager playerCharactersManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
        button_ChangeActiveCharacter = changeActiveCharacterButton;
        _playerCharactersManager = playerCharactersManager;
    }
    #endregion Zenject

    public override void PanelActivated_ExecuteReaction()
    {
        baseCharacatersInfoPanel.ShowPanel();
        characterUpgradePanel.HidePanel();
    }

    public override void PanelDeactivated_ExecuteReaction()
    {
        baseCharacatersInfoPanel.ShowPanel();
        characterUpgradePanel.HidePanel();
    }

    private void SubscribeOnEvents()
    {
        button_ChangeActiveCharacter.OnChangeActiveCharacterButtonPressed += ChangeActiveCharacterButtonPressed_ExecuteReaction;

        _playerCharactersManager.OnCharacterChanged += PlayerCharactersManager_OnCharacterChanged_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        button_ChangeActiveCharacter.OnChangeActiveCharacterButtonPressed -= ChangeActiveCharacterButtonPressed_ExecuteReaction;

        _playerCharactersManager.OnCharacterChanged -= PlayerCharactersManager_OnCharacterChanged_ExecuteReaction;
    }

    private void FillCharactersListAndDictionary()
    {
        charactersList.Add(PlayerClasses.Knight);
        charactersList.Add(PlayerClasses.Dryad);
        charactersList.Add(PlayerClasses.Wizzard);

        charactersDictionary.Add(PlayerClasses.Knight, GetClassSprite(PlayerClasses.Knight));
        charactersDictionary.Add(PlayerClasses.Dryad, GetClassSprite(PlayerClasses.Dryad));
        charactersDictionary.Add(PlayerClasses.Wizzard, GetClassSprite(PlayerClasses.Wizzard));
    }

    private void UpdateCharacterSprite(PlayerClasses characterType)
    {
        characterImage.sprite = charactersDictionary[characterType];
    }

    private PlayerClasses GetNewVisibleCharacter(SelectionArrowTypes arrowType)
    {
        int newCharacterIndex = charactersList.IndexOf(visibleCharacter);

        if (arrowType == SelectionArrowTypes.Right)
        {
            newCharacterIndex += 1;

            if(newCharacterIndex == charactersList.Count)
            {
                newCharacterIndex = 0;
            }
        }
        else
        {
            newCharacterIndex -= 1;

            if(newCharacterIndex < 0)
            {
                newCharacterIndex = charactersList.Count - 1;
            }
        }

        return charactersList[newCharacterIndex];
    }

    private void UpdateCharacterDisplayData()
    {
        UpdateCharacterSprite(visibleCharacter);

        if(visibleCharacter != _playerCharacteristicsManager.CurrentPlayerData.playerCharacterType)
        {
            button_ChangeActiveCharacter.SetButtonSprite(false);
        }
        else
        {
            button_ChangeActiveCharacter.SetButtonSprite(true);
        }
    }

    private Sprite GetClassSprite(PlayerClasses characterType)
    {
        Sprite sprite = null;

        for(int i = 0; i < classesDataSO.PlayerClassesDataList.Count; i++)
        {
            if(classesDataSO.PlayerClassesDataList[i].playerClass == characterType)
            {
                sprite = classesDataSO.PlayerClassesDataList[i].classSprite;
                break;
            }
        }

        return sprite;
    }

    private int GetCharacterLevel(PlayerClasses characterType)
    {
        int level = 0;

        for (int i = 0; i < _playerCharacteristicsManager.CharactersClasesDataList.Count; i++)
        {
            if (_playerCharacteristicsManager.CharactersClasesDataList[i].playerCharacterType == characterType)
            {
                level = _playerCharacteristicsManager.CharactersClasesDataList[i].characterLevel;
                break;
            }
        }

        return level;
    }

    private void FillCharactersDisplayPanels()
    {
        for (int i = 0; i < classesDataSO.PlayerClassesDataList.Count; i++)
        {
            CharacterDisplayPanel panel = Instantiate(characterDisplayPanelPrefab, Vector3.zero, Quaternion.identity, characterSelectionPanel);
            panel.SetCharacterData(classesDataSO.PlayerClassesDataList[i], GetCharacterLevel(classesDataSO.PlayerClassesDataList[i].playerClass));
        }
    }

    #region Buttons Events
    public void ChangeVisibleCharacterButtonPressed_ExecuteReaction(ShowNextCharacterButtonsTypes buttonType,
                                                                    SelectionArrowTypes arrowType, PlayerClasses playerClass)
    {
        if(buttonType == ShowNextCharacterButtonsTypes.ArrowButton)
        {
            visibleCharacter = GetNewVisibleCharacter(arrowType);
        }
        else
        {
            visibleCharacter = playerClass;
        }
        

        UpdateCharacterDisplayData();
    }

    private void ChangeActiveCharacterButtonPressed_ExecuteReaction()
    {
        _playerCharacteristicsManager.SetCurrentCharacterData(visibleCharacter);
    }
    #endregion Buttons Events

    private void PlayerCharactersManager_OnCharacterChanged_ExecuteReaction(PlayerClasses playerClass)
    {
        visibleCharacter = playerClass;
        UpdateCharacterDisplayData();
    }
}
