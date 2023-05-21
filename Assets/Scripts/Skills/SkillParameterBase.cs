using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;


public abstract class SkillParameterBase : MonoBehaviour
{
    protected PoolItem poolItemComponent;
    [SerializeField] protected List<Layers> obstacles;

    [SerializeField] private protected float speed;

    [SerializeField]protected Transform _dynamicEnvironment;


    [Inject]
    private void InjectDependencies(Transform dynamicEnvironment)
    {
        _dynamicEnvironment = dynamicEnvironment;
    }


    private void Awake()
    {
        CashComponents();
    }

    private void CashComponents()
    {
        if (TryGetComponent(out PoolItem poolObject))
        {
            poolItemComponent = poolObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.MapBoundBox)
        {
            Debug.Log($"collided with {obstacles}");
            poolItemComponent.PoolItemsManager.ReturnItemToPool(poolItemComponent);
        }
    }

  

}