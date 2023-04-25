using UnityEngine;
using System;

public class PoolItem : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private PoolItemsTypes poolItemType;

    public PoolItemsTypes PoolItemType { get => poolItemType;}

    #region Events Declaration
    public event Action OnItemResetRequired;
    #endregion Events Declaration

    public void ResetPoolItem()
    {
        OnItemResetRequired?.Invoke();
    }
}