using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Zenject;

public class ScenesManager : MonoBehaviour
{
    private MapsManager _mapsManager;

    private Dictionary<LevelMaps, string> scenesDictionary = new Dictionary<LevelMaps, string>();
    private List<LevelMaps> scenesList = new List<LevelMaps>();

    private void Awake()
    {
        FillScenesDictionaryAndList();
    }

    private void Start()
    {
        LoadLevelScene(LevelMaps.Cementary);

        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(MapsManager mapsManager)
    {
        _mapsManager = mapsManager;
    }
    #endregion Zenject

    public void LoadLevelScene(LevelMaps map)
    {
        SceneManager.LoadScene(scenesDictionary[map], LoadSceneMode.Additive);
    }

    private void FillScenesDictionaryAndList()
    {
        scenesDictionary.Add(LevelMaps.Cementary, ScenesNames.Cementary);
        scenesDictionary.Add(LevelMaps.Castle, ScenesNames.Castle);

        scenesList.Add(LevelMaps.Cementary);
        scenesList.Add(LevelMaps.Castle);
    }

    private void SubscribeOnEvents()
    {
        _mapsManager.OnMapChanged += MapsManager_OnMapChanged_ExecuteReaction;
    }

    private void UnsubscribeFromEvents()
    {
        _mapsManager.OnMapChanged -= MapsManager_OnMapChanged_ExecuteReaction;
    }

    private void MapsManager_OnMapChanged_ExecuteReaction(LevelMaps map)
    {
        for(int i = 0; i < scenesList.Count; i++)
        {
            if(scenesList[i] != map)
            {
                SceneManager.UnloadSceneAsync(scenesDictionary[map]);
            }
            else
            {
                LoadLevelScene(map);
            }
        }
    }
}
