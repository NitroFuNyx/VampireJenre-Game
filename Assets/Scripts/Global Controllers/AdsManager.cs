using UnityEngine;
using System;
using Zenject;

public class AdsManager : MonoBehaviour, IDataPersistance
{
    [Header("Ad Possibilities")]
    [Space]
    [SerializeField] private float levelUpAdChance = 25f;

    private DataPersistanceManager _dataPersistanceManager;
    private PlayerExperienceManager _playerExperienceManager;

    private bool blockAdsOptionPurchased = false;

    public bool BlockAdsOptionPurchased { get => blockAdsOptionPurchased; }

    public Action OnSuccessfullAdsBlockerPurchase;
    public Action OnAdsDataLoaded;

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    private void Start()
    {
        _playerExperienceManager.OnPlayerGotNewLevel += PlayerExperienceManager_OnPlayerGottNewLevel_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _playerExperienceManager.OnPlayerGotNewLevel -= PlayerExperienceManager_OnPlayerGottNewLevel_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager, PlayerExperienceManager playerExperienceManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
        _playerExperienceManager = playerExperienceManager;
    }
    #endregion Zenject

    #region Save/Load Methods
    public void LoadData(GameData data)
    {
        this.blockAdsOptionPurchased = data.blockAdsOptionPurchased;
        OnAdsDataLoaded?.Invoke();
    }

    public void SaveData(GameData data)
    {
        data.blockAdsOptionPurchased = this.blockAdsOptionPurchased;
    }
    #endregion Save/Load Methods

    public void PurchaseAdsBlocker()
    {
        if(!blockAdsOptionPurchased)
        {
            blockAdsOptionPurchased = true;
            OnSuccessfullAdsBlockerPurchase?.Invoke();
            _dataPersistanceManager.SaveGame();
        }
    }

    public bool CheckIfNetworkConnectionIsActive()
    {
        bool activeConnection;

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            activeConnection = false;
        }
        else
        {
            activeConnection = true;
        }

        return activeConnection;
    }

    private void PlayerExperienceManager_OnPlayerGottNewLevel_ExecuteReaction()
    {
        float adIndex = UnityEngine.Random.Range(0, CommonValues.maxPercentAmount);

        if(adIndex < levelUpAdChance)
        {
            // show ad
        }
    }
}
