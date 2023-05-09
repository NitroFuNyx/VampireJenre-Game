using UnityEngine;
using System;
using Zenject;

public class ResourcesManager : MonoBehaviour, IDataPersistance
{
    [Header("Resources Data")]
    [Space]
    [SerializeField] private int coinsAmount;
    [SerializeField] private int gemsAmount;

    private DataPersistanceManager _dataPersistanceManager;

    private int coinsSurplus = 5;

    #region Events Declaration
    public event Action<int> OnCoinsAmountChanged;
    public event Action<int> OnGemsAmountChanged;
    #endregion Events Declaration

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
        coinsAmount = data.coinsAmount;
        gemsAmount = data.gemsAmount;

        OnCoinsAmountChanged?.Invoke(coinsAmount);
        OnGemsAmountChanged?.Invoke(gemsAmount);
    }

    public void SaveData(GameData data)
    {
        data.coinsAmount = coinsAmount;
        data.gemsAmount = gemsAmount;
    }
    #endregion Save/Load Methods

    public void IncreaseCoinsAmount()
    {
        int randomIndex = UnityEngine.Random.Range(0, 5);
        if(randomIndex == 0)
        {
            coinsAmount += coinsSurplus;
            OnCoinsAmountChanged?.Invoke(coinsAmount);
        }
    }

    public void DecreaseCoinsAmount(int deltaAmount)
    {
        coinsAmount -= deltaAmount;

        if(coinsAmount < 0)
        {
            coinsAmount = 0;
        }

        OnCoinsAmountChanged?.Invoke(coinsAmount);
    }

    public void IncreaseGemsAmount(int deltaAmount)
    {
        gemsAmount += deltaAmount;
    }

    public void DecreaseGemsAmount(int deltaAmount)
    {
        gemsAmount -= deltaAmount;

        if(gemsAmount < 0)
        {
            gemsAmount = 0;
        }
    }
}
