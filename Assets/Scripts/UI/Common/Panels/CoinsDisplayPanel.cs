
public class CoinsDisplayPanel : ResourceDisplayPanel
{
    protected override void SubscribeOnEvents()
    {
        if(resourceSaveType == ResourcesSaveTypes.GeneralAmount)
        {
            _resourcesManager.OnCoinsAmountChanged += ResourcesManager_ResourceAmountChanged_ExecuteReaction;
        }
        else if(resourceSaveType == ResourcesSaveTypes.CurrentLevelAmount)
        {
            _resourcesManager.OnCurrentLevelCoinsAmountChanged += ResourcesManager_ResourceAmountChanged_ExecuteReaction;
        }
    }

    protected override void UnsubscribeFromEvents()
    {
        if (resourceSaveType == ResourcesSaveTypes.GeneralAmount)
        {
            _resourcesManager.OnCoinsAmountChanged -= ResourcesManager_ResourceAmountChanged_ExecuteReaction;
        }
        else if (resourceSaveType == ResourcesSaveTypes.CurrentLevelAmount)
        {
            _resourcesManager.OnCurrentLevelCoinsAmountChanged -= ResourcesManager_ResourceAmountChanged_ExecuteReaction;
        }
    }
}
