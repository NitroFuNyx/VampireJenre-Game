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

    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    private List<PlayerClasses> charactersList = new List<PlayerClasses>();
    private Dictionary<PlayerClasses, Sprite> charactersDictionary = new Dictionary<PlayerClasses, Sprite>();

    private PlayerClasses visibleCharacter;

    private void Start()
    {
        FillCharactersListAndDictionary();
        FillCharactersDisplayPanels();
        //currentlySelectedCharacter = PlayersCharactersTypes.Knight;
        //visibleCharacter = PlayersCharactersTypes.Knight;
        //UpdateCharacterSprite(visibleCharacter);
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
    }
    #endregion Zenject

    public override void PanelActivated_ExecuteReaction()
    {
        
    }

    public override void PanelDeactivated_ExecuteReaction()
    {
       
    }

    public void ChangeVisibleCharacterButtonPressed_ExecuteReaction(SelectionArrowTypes arrowType)
    {
        visibleCharacter = GetNewVisibleCharacter(arrowType);

        UpdateCharacterDisplayData();
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
}
