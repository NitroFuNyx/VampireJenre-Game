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
    [SerializeField] private protected int projectileCount;

    [SerializeField] private protected Skills skillType;
    [SerializeField]protected Transform _dynamicEnvironment;

    public int ProjectileCount => projectileCount;

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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == Layers.ObstaclesOnMap )//
        {
            Debug.Log($"collided with {obstacles}");

            CollideWithMapObstacle();
        }
    }

    protected abstract void CollideWithMapObstacle();
}