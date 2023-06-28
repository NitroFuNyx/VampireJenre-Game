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

    private List<PlayerClasses> charactersList = new List<PlayerClasses>();
    private Dictionary<PlayerClasses, Sprite> charactersDictionary = new Dictionary<PlayerClasses, Sprite>();

    private PlayerClasses visibleCharacter;

    private void Start()
    {
        FillCharactersListAndDictionary();
        //currentlySelectedCharacter = PlayersCharactersTypes.Knight;
        //visibleCharacter = PlayersCharactersTypes.Knight;
        //UpdateCharacterSprite(visibleCharacter);
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
        charactersList.Add(PlayerClasses.Knight);
        charactersList.Add(PlayerClasses.Dryad);
        charactersList.Add(PlayerClasses.Wizzard);

        charactersDictionary.Add(PlayerClasses.Knight, knightSprite);
        charactersDictionary.Add(PlayerClasses.Dryad, druidSprite);
        charactersDictionary.Add(PlayerClasses.Wizzard, wizzardSprite);
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
}
