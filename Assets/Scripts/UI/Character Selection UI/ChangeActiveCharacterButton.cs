using UnityEngine;
using System;

public class ChangeActiveCharacterButton : ButtonInteractionHandler
{
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite activeCharacterSprite;
    [SerializeField] private Sprite inActiveCharacterSprite;

    public event Action OnChangeActiveCharacterButtonPressed;

    public override void ButtonActivated()
    {
        OnChangeActiveCharacterButtonPressed?.Invoke();
    }

    public void SetButtonSprite(bool activeCharacter)
    {
        if(activeCharacter)
        {
            buttonImage.sprite = activeCharacterSprite;
        }
        else
        {
            buttonImage.sprite = inActiveCharacterSprite;
        }
    }
}
