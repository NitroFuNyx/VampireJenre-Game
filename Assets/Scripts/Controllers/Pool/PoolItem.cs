using UnityEngine;

public class PoolItem : MonoBehaviour
{
    [Header("Item Data")]
    [Space]
    [SerializeField] private PoolItemsTypes poolItemType;

    public PoolItemsTypes PoolItemType { get => poolItemType;}

    public void ResetPoolItem()
    {
        
    }
}