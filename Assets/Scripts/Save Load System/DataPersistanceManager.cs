using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DataPersistanceManager : MonoBehaviour
{
    private float loadStartDataDelay = 0.5f;

    private List<IDataPersistance> saveSystemDataObjectsList = new List<IDataPersistance>();

    private GameData gameData;

    private TalentsManager _talentsManager;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    private void Start()
    {
        StartCoroutine(LoadStartDataCoroutine());
    }

    #region Zenject
    [Inject]
    private void Construct(TalentsManager talentsManager, PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _talentsManager = talentsManager;
        _playerCharacteristicsManager = playerCharacteristicsManager;
    }
    #endregion Zenject
    
    public void NewGame()
    {
        gameData = new GameData(); // make class with default data
        
        FileDataHandler.Write(gameData); // create json file and write default data

        _talentsManager.InitializeTalentsLevelsData(gameData);
        InitializeDayOfPlayingData(gameData);
        InitializeCharacterClasesData(gameData);

        SaveGame(); // save actual Unity data set in json file
    }

    public void SaveGame()
    {
        for (int i = 0; i < saveSystemDataObjectsList.Count; i++)
        {
            saveSystemDataObjectsList[i].SaveData(gameData);
        }

        FileDataHandler.Write(gameData);
    }

    public void LoadGame()
    {
        gameData = FileDataHandler.Read();

        if(gameData == null)
        {
            Debug.Log($"No Save Files Found. Creating New Save File");
            NewGame();
        }

        for (int i = 0; i < saveSystemDataObjectsList.Count; i++)
        {
            saveSystemDataObjectsList[i].LoadData(gameData);
        }
    }

    public void AddObjectToSaveSystemObjectsList(IDataPersistance saveSystemObject)
    {
        saveSystemDataObjectsList.Add(saveSystemObject);
    }

    private void InitializeDayOfPlayingData(GameData gameData)
    {
        gameData.lastDayPlaying = DateConstants.newGameIndexForData;
    }

    private void InitializeCharacterClasesData(GameData gameData)
    {
        for(int i = 0; i <_playerCharacteristicsManager.PlayerCharactersClasesDataSO.PlayerBasicDataLists.Count; i++)
        {
            //gameData.playerClasesDataList.Add(_playerCharacteristicsManager.PlayerCharactersClasesDataSO.PlayerBasicDataLists[i]);
            gameData.playerClasesDataList.Add(_playerCharacteristicsManager.GetCharacterClassStartValues(i));
        }
    }

    private IEnumerator LoadStartDataCoroutine()
    {
        yield return new WaitForSeconds(loadStartDataDelay);
        LoadGame();
    }
}
