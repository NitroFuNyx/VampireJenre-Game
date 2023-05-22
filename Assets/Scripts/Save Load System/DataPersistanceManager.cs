using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    private float loadStartDataDelay = 0.5f;

    private List<IDataPersistance> saveSystemDataObjectsList = new List<IDataPersistance>();

    private GameData gameData;

    private void Start()
    {
        StartCoroutine(LoadStartDataCoroutine());
    }

    public void NewGame()
    {
        gameData = new GameData(); // make class with default data

        FileDataHandler.Write(gameData); // create json file and write default data

        for(int i = 0; i < 9; i++)
        {
            gameData.skillsLevelsList.Add(0);
        }

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

    private IEnumerator LoadStartDataCoroutine()
    {
        yield return new WaitForSeconds(loadStartDataDelay);
        LoadGame();
    }
}
