using UnityEngine;
using System;
using Zenject;

public class AdsManager : MonoBehaviour, IDataPersistance
{
    private DataPersistanceManager _dataPersistanceManager;

    private bool blockAdsOptionPurchased = false;

    public bool BlockAdsOptionPurchased { get => blockAdsOptionPurchased; }

    public Action OnSuccessfullAdsBlockerPurchase;
    public Action OnAdsDataLoaded;

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
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
}
