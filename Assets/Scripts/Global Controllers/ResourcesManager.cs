using UnityEngine;
using System;
using Zenject;

public class ResourcesManager : MonoBehaviour, IDataPersistance
{
    [Header("Resources Data")]
    [Space]
    [SerializeField] private int coinsAmount;
    [SerializeField] private int gemsAmount;
    [Header("Percents Data")]
    [Space]
    [SerializeField] private float gemDropDefaultPercentChance = 10f;
    [SerializeField] private float coinDropDefaultPercentChance = 20f;

    private DataPersistanceManager _dataPersistanceManager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;
    private GameProcessManager _gameProcessManager;

    private const int GemSurplusForKillingEnemy = 1;

    private const int CoinsSurplusForKillingEnemy_Min = 1;
    private const int CoinsSurplusForKillingEnemy_Max = 6;

    private int currentLevelCoinsAmount = 0;
    private int currentLevelGemsAmount = 0;

    public int CoinsAmount { get => coinsAmount; private set => coinsAmount = value; }

    #region Events Declaration
    public event Action<int> OnCoinsAmountChanged;
    public event Action<int> OnGemsAmountChanged;

    public event Action<int> OnCurrentLevelCoinsAmountChanged;
    public event Action<int> OnCurrentLevelGemsAmountChanged;
    #endregion Events Declaration

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
    }

    private void Start()
    {
        _gameProcessManager.OnGameStarted += GameProcessManager_GameStarted_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_GameStarted_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(DataPersistanceManager dataPersistanceManager, PlayerCharacteristicsManager playerCharacteristicsManager, 
                           GameProcessManager gameProcessManager)
    {
        _dataPersistanceManager = dataPersistanceManager;
        _playerCharacteristicsManager = playerCharacteristicsManager;
        _gameProcessManager = gameProcessManager;
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

    public void IncreaseCurrentLevelCoinsAmount(int deltaAmount)
    {
        currentLevelCoinsAmount += deltaAmount;
        OnCurrentLevelCoinsAmountChanged?.Invoke(currentLevelCoinsAmount);
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

    public void IncreaseCurrentLevelGemsAmount(int deltaAmount)
    {
        currentLevelGemsAmount += deltaAmount;
        OnCurrentLevelGemsAmountChanged?.Invoke(currentLevelGemsAmount);
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

    public ResourceBonusItemStruct GetResourceItemForKillingEnemyData()
    {
        ResourceBonusItemStruct data = new ResourceBonusItemStruct();
        data.canBeCollected = false;

        float gemGrantingIndex;

        gemGrantingIndex = UnityEngine.Random.Range(0, CommonValues.maxPercentAmount);

        if (gemGrantingIndex < gemDropDefaultPercentChance + _playerCharacteristicsManager.CurrentPlayerData.characterItemDropChancePercent)
        {
            data.resourceType = ResourcesTypes.Gems;
            data.ResourceAmount = GemSurplusForKillingEnemy;
            data.canBeCollected = true;
        }
        else
        {
            float coinsGrantingIndex;

            coinsGrantingIndex = UnityEngine.Random.Range(0, CommonValues.maxPercentAmount);

            if (coinsGrantingIndex < coinDropDefaultPercentChance + _playerCharacteristicsManager.CurrentPlayerData.characterItemDropChancePercent)
            {
                int coins = UnityEngine.Random.Range(CoinsSurplusForKillingEnemy_Min, CoinsSurplusForKillingEnemy_Max);
                data.resourceType = ResourcesTypes.Coins;
                data.ResourceAmount = coins;
                data.canBeCollected = true;
            }
        }

        return data;
    }

    public void AddResourceForKillingEnemy(ResourceBonusItemStruct data)
    {
        if(data.resourceType == (ResourcesTypes)ResourcesTypes.Gems)
        {
            IncreaseCurrentLevelGemsAmount(data.ResourceAmount);
        }
        else
        {
            IncreaseCurrentLevelCoinsAmount(data.ResourceAmount);
        }
    }

    public void AddResourceForPickingUpTreasureChest()
    {
        float gemGrantingIndex;

        gemGrantingIndex = UnityEngine.Random.Range(0, CommonValues.maxPercentAmount);

        if (gemGrantingIndex < gemDropDefaultPercentChance)
        {
            IncreaseCurrentLevelGemsAmount(GemSurplusForKillingEnemy);
        }
        else
        {
            int coinsAmount = UnityEngine.Random.Range(CoinsSurplusForKillingEnemy_Min, CoinsSurplusForKillingEnemy_Max);
            IncreaseCurrentLevelCoinsAmount(coinsAmount);
        }
    }

    private void GameProcessManager_GameStarted_ExecuteReaction()
    {
        currentLevelCoinsAmount = 0;
        currentLevelGemsAmount = 0;

        OnCurrentLevelCoinsAmountChanged?.Invoke(currentLevelCoinsAmount);
        OnCurrentLevelGemsAmountChanged?.Invoke(currentLevelGemsAmount);
    }

    private void AddCurrentLevelResourcesToGeneralAmount()
    {
        coinsAmount += currentLevelCoinsAmount;
        OnCoinsAmountChanged?.Invoke(coinsAmount);

        gemsAmount += currentLevelGemsAmount;
        OnGemsAmountChanged?.Invoke(gemsAmount);
    }
}
