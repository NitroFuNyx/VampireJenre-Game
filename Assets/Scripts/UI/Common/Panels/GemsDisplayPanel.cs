
public class GemsDisplayPanel : ResourceDisplayPanel
{
    protected override void SubscribeOnEvents()
    {
        if (resourceSaveType == ResourcesSaveTypes.GeneralAmount)
        {
            _resourcesManager.OnGemsAmountChanged += ResourcesManager_ResourceAmountChanged_ExecuteReaction;
        }
        else if (resourceSaveType == ResourcesSaveTypes.CurrentLevelAmount)
        {
            _resourcesManager.OnCurrentLevelGemsAmountChanged += ResourcesManager_ResourceAmountChanged_ExecuteReaction;
        }
    }

    protected override void UnsubscribeFromEvents()
    {
        if (resourceSaveType == ResourcesSaveTypes.GeneralAmount)
        {
            _resourcesManager.OnGemsAmountChanged -= ResourcesManager_ResourceAmountChanged_ExecuteReaction;
        }
        else if (resourceSaveType == ResourcesSaveTypes.CurrentLevelAmount)
        {
            _resourcesManager.OnCurrentLevelGemsAmountChanged -= ResourcesManager_ResourceAmountChanged_ExecuteReaction;
        }
    }
}
