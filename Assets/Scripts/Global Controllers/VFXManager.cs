using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class VFXManager : MonoBehaviour
{
    [Header("Holders")]
    [Space]
    [SerializeField] private Transform bloodPudleVfxXHolder;

    private GameProcessManager _gameProcessManager;

    public Transform BloodPudleVfxXHolder { get => bloodPudleVfxXHolder; }

    private void Start()
    {
        _gameProcessManager.OnLevelDataReset += GameProcessManager_OnLevelDataReset_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnLevelDataReset -= GameProcessManager_OnLevelDataReset_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(GameProcessManager gameProcessManager)
    {
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    private void ReturnHolderItemsToPool(Transform holder)
    {
        List<PoolItem> itemsList = new List<PoolItem>();

        for(int i = 0; i < holder.childCount; i++)
        {
            if(holder.GetChild(i).TryGetComponent(out PoolItem item))
            {
                itemsList.Add(item);
            }
        }

        for(int i = 0; i < itemsList.Count; i++)
        {
            itemsList[i].PoolItemsManager.ReturnItemToPool(itemsList[i]);
            Debug.Log($"{itemsList[i]} returns");
        }
    }

    private void GameProcessManager_OnLevelDataReset_ExecuteReaction()
    {
        ReturnHolderItemsToPool(bloodPudleVfxXHolder);
    }
}
