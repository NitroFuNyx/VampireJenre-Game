using System;

public class CharacterDetailsButton : ButtonInteractionHandler
{
    public event Action OnCharacterDetailsButtonPressed;

    public override void ButtonActivated()
    {
        OnCharacterDetailsButtonPressed?.Invoke();
    }
}
