
public class GemsDisplayPanel : ResourceDisplayPanel
{
    protected override void SubscribeOnEvents()
    {
        _resourcesManager.OnGemsAmountChanged += ResourcesManager_ResourceAmountChanged_ExecuteReaction;
    }

    protected override void UnsubscribeFromEvents()
    {
        _resourcesManager.OnGemsAmountChanged -= ResourcesManager_ResourceAmountChanged_ExecuteReaction;
    }
}
