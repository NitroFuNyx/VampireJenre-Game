using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterSelectionUI : MainCanvasPanel
{
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite knightSprite;
    [SerializeField] private Sprite druidSprite;
    [SerializeField] private Sprite wizzardSprite;
    [Header("Images")]
    [Space]
    [SerializeField] private Image characterImage;

    private List<PlayersCharactersTypes> charactersList = new List<PlayersCharactersTypes>();
    private Dictionary<PlayersCharactersTypes, Sprite> charactersDictionary = new Dictionary<PlayersCharactersTypes, Sprite>();

    private PlayersCharactersTypes currentlySelectedCharacter;
    private PlayersCharactersTypes visibleCharacter;

    private void Start()
    {
        FillCharactersListAndDictionary();
        currentlySelectedCharacter = PlayersCharactersTypes.Knight;
        visibleCharacter = PlayersCharactersTypes.Knight;
        UpdateCharacterSprite(visibleCharacter);
    }

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
        charactersList.Add(PlayersCharactersTypes.Knight);
        charactersList.Add(PlayersCharactersTypes.Druid);
        charactersList.Add(PlayersCharactersTypes.Wizzard);

        charactersDictionary.Add(PlayersCharactersTypes.Knight, knightSprite);
        charactersDictionary.Add(PlayersCharactersTypes.Druid, druidSprite);
        charactersDictionary.Add(PlayersCharactersTypes.Wizzard, wizzardSprite);
    }

    private void UpdateCharacterSprite(PlayersCharactersTypes characterType)
    {
        characterImage.sprite = charactersDictionary[characterType];
    }

    private PlayersCharactersTypes GetNewVisibleCharacter(SelectionArrowTypes arrowType)
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
}
