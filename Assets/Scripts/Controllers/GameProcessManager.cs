using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameProcessManager : MonoBehaviour
{
    [Header("Player")]
    [Space]
    [SerializeField] private GameObject skillObject;

    private SpawnEnemiesManager _spawnEnemiesManager;

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

    private IEnumerator StartGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        skillObject.SetActive(true);
        _spawnEnemiesManager.SpawnEnemies();
    }
}
