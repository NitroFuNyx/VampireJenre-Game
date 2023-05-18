using UnityEngine;
using Zenject;

public class PlayerCharacteristicsManager : MonoBehaviour, IDataPersistance
{
    [Header("Start Data")]
    [Space]
    [SerializeField] private PlayerCharacteristicsSO playerCharacteristicsSO;

    private DataPersistanceManager _dataPersistanceManager;

    private PlayerBasicCharacteristicsStruct currentPlayerData;

    private void Awake()
    {
        currentPlayerData = playerCharacteristicsSO.PlayerBasicDataLists[0];
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
        currentPlayerData = data.playerCharacteristcsData;
    }

    public void SaveData(GameData data)
    {
        data.playerCharacteristcsData = currentPlayerData;
    }
    #endregion Save/Load Methods
}
