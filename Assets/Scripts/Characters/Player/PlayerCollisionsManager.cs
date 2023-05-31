using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;

public class PlayerCollisionsManager : MonoBehaviour
{
    [Header("Hp Data")]
    [Space]
    [SerializeField] private float startHp = 100f;
    [SerializeField] private float currentHp = 100f;

    private bool canCheckCollisions = true;

    private float damage = 10f;

    #region Events Declaration
    public event Action OnPlayerOutOfHp;
    public event Action OnDamageReceived;
    public event Action<float, float> OnHpAmountChanged;
    #endregion Events Declaration

    private void Start()
    {
        SetStartSettings();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == Layers.EnemySkeleton || collision.gameObject.layer == Layers.EnemyGhost || collision.gameObject.layer == Layers.EnemyZombie)
        {
            DecreaseHp(damage);
        }
        if(collision.gameObject.layer == 29||collision.gameObject.layer == 30||collision.gameObject.layer == 31)
            Debug.Log($"Collided with {collision.gameObject.layer} GO: {collision.gameObject}",gameObject);
    }

    public void ResetComponent()
    {
        currentHp = startHp;
    }

    private void SetStartSettings()
    {
        currentHp = startHp;
    }

    private void DecreaseHp(float amount)
    {
        currentHp -= amount;
        if (currentHp <= 0)
        {
            canCheckCollisions = false;
            OnPlayerOutOfHp?.Invoke();
        }
        else
        {
            OnDamageReceived?.Invoke();
        }

        OnHpAmountChanged?.Invoke(currentHp, startHp);
    }
}
