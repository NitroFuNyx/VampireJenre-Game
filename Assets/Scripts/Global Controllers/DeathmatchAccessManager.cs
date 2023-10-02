using UnityEngine;
using System;
using System.Collections;
using Zenject;

public class DeathmatchAccessManager : MonoBehaviour, IDataPersistance
{
    [Header("Deathmathc Buying Data")]
    [Space]
    [SerializeField] private ResourcesTypes buyingCurrency = ResourcesTypes.Gems;
    [SerializeField] private int deatmatchPurchaseCost = 500;

    private DataPersistanceManager _dataPersistanceManager;
    private SystemTimeManager _systemTimeManager;
    private MainUI _mainUI;
    private ResourcesManager _resourcesManager;

    private bool deathMatchModeUsedAtCurrentDay = false;

    public int DeatmatchPurchaseCost { get => deatmatchPurchaseCost; }

    public event Action OnDeathmatchUiUpdateRequired;

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager, SystemTimeManager systemTimeManager, MainUI mainUI,
                           ResourcesManager resourcesManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
        _systemTimeManager = systemTimeManager;
        _mainUI = mainUI;
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    public void LoadData(GameData data)
    {
        deathMatchModeUsedAtCurrentDay = data.deathMatchModeUsedAtCurrentDay;
        StartCoroutine(SetDeathmatchDataCoroutine());
    }

    public void SaveData(GameData data)
    {
        data.deathMatchModeUsedAtCurrentDay = deathMatchModeUsedAtCurrentDay;
    }

    public void PlayDeathmatchButtonPressed_ExecuteReaction(System.Action OnBuyingProcessSuccessfull, System.Action OnBuyingProccessFailed)
    {
        if(!deathMatchModeUsedAtCurrentDay && _resourcesManager.CheckIfEnoughResources(buyingCurrency, deatmatchPurchaseCost))
        {
            deathMatchModeUsedAtCurrentDay = true;
            _resourcesManager.SpentResource(buyingCurrency, deatmatchPurchaseCost);
            _dataPersistanceManager.SaveGame();
            OnBuyingProcessSuccessfull?.Invoke();
            OnDeathmatchUiUpdateRequired?.Invoke();
            _mainUI.DeathmatchButtonPressed_ExecuteReaction();
        }
        else
        {
            OnBuyingProccessFailed?.Invoke();
        }
    }

    private IEnumerator SetDeathmatchDataCoroutine()
    {
        yield return null;
        yield return null;

        if (_systemTimeManager.NewDay)
        {
            deathMatchModeUsedAtCurrentDay = false;
            _dataPersistanceManager.SaveGame();
        }
        else
        {
            if (deathMatchModeUsedAtCurrentDay)
            {
                OnDeathmatchUiUpdateRequired?.Invoke();
            }
        }
    }
}
