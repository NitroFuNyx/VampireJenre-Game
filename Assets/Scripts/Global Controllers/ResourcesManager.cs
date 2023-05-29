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

    private const int GemSurplusForKillingEnemy = 1;

    private const int CoinsSurplusForKillingEnemy_Min = 1;
    private const int CoinsSurplusForKillingEnemy_Max = 6;

    public int CoinsAmount { get => coinsAmount; private set => coinsAmount = value; }

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

    #region Basic Resources Methods
    public void IncreaseCoinsAmount(int deltaAmount)
    {
        coinsAmount += deltaAmount;
        OnCoinsAmountChanged?.Invoke(coinsAmount);
        Debug.Log($"Coins {deltaAmount}");
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
        OnGemsAmountChanged?.Invoke(gemsAmount);
        Debug.Log($"Gems {deltaAmount}");
    }

    public void DecreaseGemsAmount(int deltaAmount)
    {
        gemsAmount -= deltaAmount;

        if(gemsAmount < 0)
        {
            gemsAmount = 0;
        }

        OnGemsAmountChanged?.Invoke(gemsAmount);
    }
    #endregion Basic Resources Methods

    public void AddResourceForKillingEnemy()
    {
        int gemGrantingIndex;

        gemGrantingIndex = UnityEngine.Random.Range(0, 10);

        if(gemGrantingIndex == 0)
        {
            IncreaseGemsAmount(GemSurplusForKillingEnemy);
        }
        else
        {
            int coinsGrantingIndex;

            coinsGrantingIndex = UnityEngine.Random.Range(0, 5);

            if(coinsGrantingIndex == 0)
            {
                int coinsAmount = UnityEngine.Random.Range(CoinsSurplusForKillingEnemy_Min, CoinsSurplusForKillingEnemy_Max);
                IncreaseCoinsAmount(coinsAmount);
            }
        }
    }

    public void AddResourceForPickingUpTreasureChest()
    {
        int gemGrantingIndex;

        gemGrantingIndex = UnityEngine.Random.Range(0, 10);

        if (gemGrantingIndex == 0)
        {
            IncreaseGemsAmount(GemSurplusForKillingEnemy);
        }
        else
        {
            int coinsAmount = UnityEngine.Random.Range(CoinsSurplusForKillingEnemy_Min, CoinsSurplusForKillingEnemy_Max);
            IncreaseCoinsAmount(coinsAmount);
        }
    }
}
