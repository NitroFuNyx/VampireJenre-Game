
public class CharacterClassProgressBar : ProgressBar
{
    protected override void SubscribeOnEvents()
    {
        
    }

    protected override void UnsubscribeFromEvents()
    {
        
    }

    public void UpdateValue(float currentAmount, float maxAmount)
    {
        ProgressBarValueChanged_ExecuteReaction(currentAmount, maxAmount);
    }

    protected override void SetProgressBarStartValue()
    {
        progressBarImage.fillAmount = 0f;
    }
}
