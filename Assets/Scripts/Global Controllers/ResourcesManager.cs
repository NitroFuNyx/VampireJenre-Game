using UnityEngine;
using System;
using System.Collections.Generic;
using Zenject;

public class ResourcesManager : MonoBehaviour, IDataPersistance
{
    [Header("Resources Data")]
    [Space]
    //[SerializeField] private int coinsAmount;
    //[SerializeField] private int gemsAmount;
    [Header("Percents Data")]
    [Space]
    [SerializeField] private float gemDropDefaultPercentChance = 10f;
    [SerializeField] private float coinDropDefaultPercentChance = 20f;
    [Header("Treasure Chest Resources")]
    [Space]
    [SerializeField] private List<int> gemsTreasureAmountsList = new List<int>();
    [SerializeField] private List<int> coinsTreasureAmountsList = new List<int>();
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite gemsSprite;
    [SerializeField] private Sprite coinsSprite;

    private DataPersistanceManager _dataPersistanceManager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;
    private GameProcessManager _gameProcessManager;
    [SerializeField]private GameData gameData;
    public GameData GameData
    {
        get => gameData;
       private  set => gameData = value;
    }

    private const int GemSurplusForKillingEnemy = 1;

    private const int CoinsSurplusForKillingEnemy_Min = 1;
    private const int CoinsSurplusForKillingEnemy_Max = 6;

    private GameData.Secureint currentLevelCoinsAmount = new GameData.Secureint();
    private GameData.Secureint currentLevelGemsAmount =  new GameData.Secureint();
    private int coinsMultiplyerForAd = 2;

    //public int CoinsAmount { get => coinsAmount; private set => coinsAmount = value; }
    public GameData.Secureint CurrentLevelCoinsAmount { get => currentLevelCoinsAmount; private set => currentLevelCoinsAmount = value; }
    public GameData.Secureint CurrentLevelGemsAmount { get => currentLevelGemsAmount; private set => currentLevelGemsAmount = value; }

    #region Events Declaration
    public event Action<int> OnCoinsAmountChanged;
    public event Action<int> OnGemsAmountChanged;

    public event Action<int> OnCurrentLevelCoinsAmountChanged;
    public event Action<int> OnCurrentLevelGemsAmountChanged;
    #endregion Events Declaration

    private void Awake()
    {
        _dataPersistanceManager.AddObjectToSaveSystemObjectsList(this);
        gameData = new GameData();
    }

    private void Start()
    {
        _gameProcessManager.OnGameStarted += GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnLevelDataReset += GameProcessManager_LevelDataReset_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnGameStarted -= GameProcessManager_GameStarted_ExecuteReaction;
        _gameProcessManager.OnLevelDataReset -= GameProcessManager_LevelDataReset_ExecuteReaction;
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
        gameData.coinsAmount = data.coinsAmount;
        gameData.gemsAmount = data.gemsAmount;
        Debug.Log($"Gems {data.gemsAmount} Coins {data.coinsAmount}");
        OnCoinsAmountChanged?.Invoke(data.coinsAmount.GetValue());
        OnGemsAmountChanged?.Invoke(data.gemsAmount.GetValue());
    }

    public void SaveData(GameData data)
    {
        data.coinsAmount = new GameData.Secureint(gameData.coinsAmount.GetValue());
        data.gemsAmount = new GameData.Secureint(gameData.gemsAmount.GetValue());
    }
    #endregion Save/Load Methods

    #region Basic Resources Methods
    public void IncreaseCoinsAmount(int deltaAmount)
    {
        gameData.coinsAmount += new GameData.Secureint(deltaAmount);
        OnCoinsAmountChanged?.Invoke(gameData.coinsAmount.GetValue());
        Debug.Log($"Coins {deltaAmount}");
    }

    public void IncreaseCurrentLevelCoinsAmount(int deltaAmount)
    {
        currentLevelCoinsAmount += new GameData.Secureint(deltaAmount);
        OnCurrentLevelCoinsAmountChanged?.Invoke(currentLevelCoinsAmount.GetValue());
    }

    public void DecreaseCoinsAmount(int deltaAmount)
    {
        gameData.coinsAmount -= new GameData.Secureint(deltaAmount);

        if( gameData.coinsAmount.GetValue() < 0)
        {
            gameData.coinsAmount =new GameData.Secureint(0);
        }

        OnCoinsAmountChanged?.Invoke(gameData.coinsAmount.GetValue());
    }

    public void IncreaseGemsAmount(int deltaAmount)
    {
        gameData.gemsAmount += new GameData.Secureint(deltaAmount);
        OnGemsAmountChanged?.Invoke(gameData.gemsAmount.GetValue());
        Debug.Log($"Gems {deltaAmount}");
    }

    public void IncreaseCurrentLevelGemsAmount(int deltaAmount)
    {
        currentLevelGemsAmount += new GameData.Secureint(deltaAmount);
        OnCurrentLevelGemsAmountChanged?.Invoke(currentLevelGemsAmount.GetValue());
    }

    public void DecreaseGemsAmount(int deltaAmount)
    {
        gameData.gemsAmount -= new GameData.Secureint(deltaAmount);

        if( gameData.gemsAmount.GetValue() < 0)
        {
            gameData.gemsAmount = new GameData.Secureint(0);
        }

        OnGemsAmountChanged?.Invoke(gameData.gemsAmount.GetValue());
    }
    #endregion Basic Resources Methods

    public ResourceBonusItemStruct GetResourceItemForKillingEnemyData()
    {
        ResourceBonusItemStruct data = new ResourceBonusItemStruct();
        data.canBeCollected = false;

        float gemGrantingIndex = 0;

        if(_gameProcessManager.CurrentGameMode == GameModes.Standart)
        {
            gemGrantingIndex = UnityEngine.Random.Range(0, CommonValues.maxPercentAmount);
        }
        else if(_gameProcessManager.CurrentGameMode == GameModes.Deathmatch)
        {
            gemGrantingIndex = CommonValues.maxPercentAmount + 1; // always not accessible
        }

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

    public void AddTreasureForPickingUpTreasureChest(ResourcesTypes resource, int amount)
    {
        if(resource == ResourcesTypes.Gems)
        {
            IncreaseCurrentLevelGemsAmount(amount);
        }
        else
        {
            IncreaseCurrentLevelCoinsAmount(amount);
        }
    }

    //public void AddResourceForPickingUpTreasureChest()
    //{
    //    float gemGrantingIndex;

    //    gemGrantingIndex = UnityEngine.Random.Range(0, CommonValues.maxPercentAmount);

    //    if (gemGrantingIndex < gemDropDefaultPercentChance)
    //    {
    //        IncreaseCurrentLevelGemsAmount(GemSurplusForKillingEnemy);
    //    }
    //    else
    //    {
    //        int coinsAmount = UnityEngine.Random.Range(CoinsSurplusForKillingEnemy_Min, CoinsSurplusForKillingEnemy_Max);
    //        IncreaseCurrentLevelCoinsAmount(coinsAmount);
    //    }
    //}

    public TreasureChestResourceDataStruct GetResourceDataForPickingUpTreasureChest(TreasureChestItems resource)
    {
        TreasureChestResourceDataStruct resourceData = new TreasureChestResourceDataStruct();

        int index;


        if(resource == TreasureChestItems.Gems)
        {
            index = UnityEngine.Random.Range(0, gemsTreasureAmountsList.Count);

            resourceData.resourceType = ResourcesTypes.Gems;
            resourceData.resourceAmount = gemsTreasureAmountsList[index];
            resourceData.resourceSprite = gemsSprite;
        }
        else if(resource == TreasureChestItems.Coins)
        {
            index = UnityEngine.Random.Range(0, coinsTreasureAmountsList.Count);

            resourceData.resourceType = ResourcesTypes.Coins;
            resourceData.resourceAmount = coinsTreasureAmountsList[index];
            resourceData.resourceSprite = coinsSprite;
        }

        return resourceData;
    }

    public void MultiplyCurrentLevelCoinsAmount()
    {
        currentLevelCoinsAmount *= new GameData.Secureint(coinsMultiplyerForAd);
    }

    public int GetCoinsForLevelAmountWithSkillBonus()
    {
        currentLevelCoinsAmount += new GameData.Secureint(GetBonusCoinsAmountFromSkill());
        return currentLevelCoinsAmount.GetValue();
    }

    // Start Of Test Methods
    [ContextMenu("Increse Resources Amount")]
    public void TestMethod_IncreaseResourcesAmount()
    {
         gameData.coinsAmount += new GameData.Secureint(1000);
         gameData.gemsAmount += new GameData.Secureint(1000);

        OnCoinsAmountChanged?.Invoke(gameData.coinsAmount.GetValue());
        OnGemsAmountChanged?.Invoke(gameData.gemsAmount.GetValue());
    }
    // End Of Test Methods

    private void AddCurrentLevelResourcesToGeneralAmount()
    {
        gameData.coinsAmount += new GameData.Secureint(currentLevelCoinsAmount.GetValue());
        OnCoinsAmountChanged?.Invoke( gameData.coinsAmount.GetValue());

        gameData.gemsAmount += new GameData.Secureint(currentLevelGemsAmount.GetValue());
        OnGemsAmountChanged?.Invoke(gameData.gemsAmount.GetValue());
    }

    private int GetBonusCoinsAmountFromSkill()
    {
        int coinsBonusAmount = (currentLevelCoinsAmount.GetValue() * (int)_playerCharacteristicsManager.CurrentPlayerData.characterCoinsSurplusPercent) / (int)CommonValues.maxPercentAmount;
        return coinsBonusAmount;
    }

    private void GameProcessManager_GameStarted_ExecuteReaction()
    {
        currentLevelCoinsAmount = new GameData.Secureint();
        currentLevelGemsAmount = new GameData.Secureint();

        OnCurrentLevelCoinsAmountChanged?.Invoke(currentLevelCoinsAmount.GetValue());
        OnCurrentLevelGemsAmountChanged?.Invoke(currentLevelGemsAmount.GetValue());
    }

    private void GameProcessManager_LevelDataReset_ExecuteReaction()
    {
        AddCurrentLevelResourcesToGeneralAmount();
        _dataPersistanceManager.SaveGame();
    }
}
