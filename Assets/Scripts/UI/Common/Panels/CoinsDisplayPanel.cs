
public class CoinsDisplayPanel : ResourceDisplayPanel
{
    protected override void SubscribeOnEvents()
    {
        _resourcesManager.OnCoinsAmountChanged += ResourcesManager_ResourceAmountChanged_ExecuteReaction;
    }

    protected override void UnsubscribeFromEvents()
    {
        _resourcesManager.OnCoinsAmountChanged -= ResourcesManager_ResourceAmountChanged_ExecuteReaction;
    }
}
