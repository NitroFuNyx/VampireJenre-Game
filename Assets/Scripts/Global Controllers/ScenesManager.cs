using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Zenject;

public class ScenesManager : MonoBehaviour
{
    private MapsManager _mapsManager;

    private Dictionary<LevelMaps, string> scenesDictionary = new Dictionary<LevelMaps, string>();

    private Scene currentlyActiveAdditiveScene;

    public Scene CurrentlyActiveAdditiveScene { get => currentlyActiveAdditiveScene; }

    private void Awake()
    {
        FillScenesDictionary();
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
        currentlyActiveAdditiveScene = SceneManager.GetSceneByName(scenesDictionary[map]);
    }

    private void FillScenesDictionary()
    {
        scenesDictionary.Add(LevelMaps.Cementary, ScenesNames.Cementary);
        scenesDictionary.Add(LevelMaps.Castle, ScenesNames.Castle);
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
        if(currentlyActiveAdditiveScene.name != scenesDictionary[map])
        {
            SceneManager.UnloadScene(currentlyActiveAdditiveScene);
            LoadLevelScene(map);
        }
    }
}
