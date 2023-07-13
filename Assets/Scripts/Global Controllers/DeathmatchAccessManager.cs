using UnityEngine;
using System.Collections;
using Zenject;

public class DeathmatchAccessManager : MonoBehaviour, IDataPersistance
{
    [Header("Deathmathc Buying Data")]
    [Space]
    [SerializeField] private ResourcesTypes buyingCurrency = ResourcesTypes.Gems;
    [SerializeField] private int cost = 4;

    private DataPersistanceManager _dataPersistanceManager;
    private SystemTimeManager _systemTimeManager;
    private MainUI _mainUI;
    private ResourcesManager _resourcesManager;

    private bool deathMatchModeUsedAtCurrentDay = false;

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
        if(!deathMatchModeUsedAtCurrentDay && _resourcesManager.CheckIfEnoughResources(buyingCurrency, cost))
        {
            deathMatchModeUsedAtCurrentDay = true;
            _resourcesManager.BuyDeathmatchGame(buyingCurrency, cost);
            _dataPersistanceManager.SaveGame();
            OnBuyingProcessSuccessfull?.Invoke();
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
                //OnSpinButtonUpdateRequired?.Invoke();
                // update button
            }
        }
    }
}
