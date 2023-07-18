using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ScenesManager : MonoBehaviour
{
    private Dictionary<LevelMaps, string> scenesDictionary = new Dictionary<LevelMaps, string>();

    private void Awake()
    {
        FillScenesDictionary();
    }

    private void Start()
    {
        LoadLevelScene(LevelMaps.Cementary);
    }

    public void LoadLevelScene(LevelMaps map)
    {
        SceneManager.LoadScene(scenesDictionary[map], LoadSceneMode.Additive);
    }

    private void FillScenesDictionary()
    {
        scenesDictionary.Add(LevelMaps.Cementary, ScenesNames.Cementary);
        scenesDictionary.Add(LevelMaps.Castle, ScenesNames.Castle);
    }
}
