using UnityEngine;
using System;
using Zenject;

public class TalentsManager : MonoBehaviour
{
    [Header("Cost Data")]
    [Space]
    [SerializeField] private int talentCostAmount = 100;

    private ResourcesManager _resourcesManager;
    private TalentWheel _talentWheel;

    private Action OnBuyingProcessFinishedCallback;

    private void Start()
    {
        _talentWheel.OnTalentToUpgradeDefined += TalentToUpgradeDefined_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _talentWheel.OnTalentToUpgradeDefined -= TalentToUpgradeDefined_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager, TalentWheel talentWheel)
    {
        _resourcesManager = resourcesManager;
        _talentWheel = talentWheel;
    }
    #endregion Zenject

    public void BuyTalent(Action OnBuyingProcessFinished, Action OnBuyingProcessCanceled)
    {
        if(_resourcesManager.CoinsAmount >= talentCostAmount)
        {
            OnBuyingProcessFinishedCallback = OnBuyingProcessFinished;
            _resourcesManager.DecreaseCoinsAmount(talentCostAmount);
            _talentWheel.StartWheel();
            OnBuyingProcessFinished?.Invoke();
        }
        else
        {
            OnBuyingProcessCanceled?.Invoke();
        }
    }

    private void TalentToUpgradeDefined_ExecuteReaction(TalentItem talentItem)
    {
        Debug.Log($"Talent {talentItem.TalentType}");
        OnBuyingProcessFinishedCallback?.Invoke();
    }
}
