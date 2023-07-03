using System;

public class ChangeActiveCharacterButton : ButtonInteractionHandler
{
    public event Action OnChangeActiveCharacterButtonPressed;

    public override void ButtonActivated()
    {
        OnChangeActiveCharacterButtonPressed?.Invoke();
    }
}
