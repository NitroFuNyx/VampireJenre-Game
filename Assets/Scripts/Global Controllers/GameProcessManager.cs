using System.Collections;
using UnityEngine;
using System;
using Zenject;

public class GameProcessManager : MonoBehaviour
{
    [Header("Player")]
    [Space]
    [SerializeField] private GameObject skillObject;
    [Header("Map Progreess Data")]
    [Space]
    [SerializeField] private float currentMapProgress = 0;
    [SerializeField] private float upgradeProgressValue = 100;

    private SpawnEnemiesManager _spawnEnemiesManager;

    private int mapProgressDelta = 1;

    #region Events Declaration
    public event Action<float, float> OnMapProgressChanged;
    #endregion Events Declaration

    private void Start()
    {
        Input.multiTouchEnabled = false;
    }

    #region Zenject
    [Inject]
    private void Construct(SpawnEnemiesManager spawnEnemiesManager)
    {
        _spawnEnemiesManager = spawnEnemiesManager;
    }
    #endregion Zenject

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void IncreaseCurrentProgressValue()
    {
        currentMapProgress += mapProgressDelta;
        OnMapProgressChanged?.Invoke(currentMapProgress, upgradeProgressValue);

        if(currentMapProgress >= upgradeProgressValue)
        {
            // spawn boss
        }
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        skillObject.SetActive(true);
        _spawnEnemiesManager.SpawnEnemies();
    }
}
