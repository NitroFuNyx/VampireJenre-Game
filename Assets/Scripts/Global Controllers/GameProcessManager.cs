using System.Collections;
using UnityEngine;
using Zenject;

public class GameProcessManager : MonoBehaviour
{
    [Header("Player")]
    [Space]
    [SerializeField] private GameObject skillObject;
    [Header("Map Progreess Data")]
    [Space]
    [SerializeField] private int currentMapProgress = 0;
    [SerializeField] private int upgradeProgressValue = 100;

    private SpawnEnemiesManager _spawnEnemiesManager;

    private int mapProgressDelta = 1;

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

        if(currentMapProgress >= upgradeProgressValue)
        {
            // spawn boss
        }
    }

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        skillObject.SetActive(true);
        //_spawnEnemiesManager.SpawnEnemies();
    }
}
