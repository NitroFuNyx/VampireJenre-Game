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
    private CharacterDetailsButton button_detailsCharacter;
    private BuyCharacterButton button_buyCharacter;

    private CharacterCostPanel _characterCostPanel;


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
    private void Construct(PlayerCharacteristicsManager playerCharacteristicsManager, CharacterCostPanel characterCostPanel,
                           ChangeActiveCharacterButton changeActiveCharacterButton, PlayerCharactersManager playerCharactersManager,
                           CharacterDetailsButton characterDetailsButton, BuyCharacterButton buyCharacterButton)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
        button_ChangeActiveCharacter = changeActiveCharacterButton;
        _playerCharactersManager = playerCharactersManager;
        button_detailsCharacter = characterDetailsButton;
        button_buyCharacter = buyCharacterButton;
        _characterCostPanel = characterCostPanel;
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

        button_buyCharacter.OnNewCharacterBought += CharacterBought_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        button_ChangeActiveCharacter.OnChangeActiveCharacterButtonPressed -= ChangeActiveCharacterButtonPressed_ExecuteReaction;

        button_buyCharacter.OnNewCharacterBought -= CharacterBought_ExecuteReaction;
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

        if(_playerCharacteristicsManager.GetCharacterCharacteristicsData(visibleCharacter).locked)
        {
            button_ChangeActiveCharacter.gameObject.SetActive(false);
            button_detailsCharacter.gameObject.SetActive(false);
            button_buyCharacter.gameObject.SetActive(true);
            _characterCostPanel.gameObject.SetActive(true);

            _characterCostPanel.UpdateCharacterCostPanel(GetPlayerClassData(visibleCharacter));
            button_buyCharacter.UpdateButtonSprite(GetPlayerClassData(visibleCharacter));
        }
        else
        {
            button_ChangeActiveCharacter.gameObject.SetActive(true);
            button_detailsCharacter.gameObject.SetActive(true);
            button_buyCharacter.gameObject.SetActive(false);
            _characterCostPanel.gameObject.SetActive(false);

            if (visibleCharacter != _playerCharacteristicsManager.CurrentPlayerData.playerCharacterType)
            {
                button_ChangeActiveCharacter.SetButtonSprite(false);
            }
            else
            {
                button_ChangeActiveCharacter.SetButtonSprite(true);
            }
        }
    }

    private PlayerClassDataStruct GetPlayerClassData(PlayerClasses characterClass)
    {
        PlayerClassDataStruct playerClassData = new PlayerClassDataStruct();

        for (int i = 0; i < classesDataSO.PlayerClassesDataList.Count; i++)
        {
            if (characterClass == classesDataSO.PlayerClassesDataList[i].playerClass)
            {
                playerClassData = classesDataSO.PlayerClassesDataList[i];
                break;
            }
        }

        return playerClassData;
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
        _playerCharactersManager.SetPlayCharacterModel(visibleCharacter);
    }

    private void CharacterBought_ExecuteReaction(PlayerClasses playerClass)
    {
        UpdateCharacterDisplayData();
    }
    #endregion Buttons Events
}
