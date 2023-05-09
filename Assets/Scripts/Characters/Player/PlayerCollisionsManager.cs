using UnityEngine;
using System;

public class PlayerCollisionsManager : MonoBehaviour
{
    [Header("Hp Data")]
    [Space]
    [SerializeField] private int startHp = 100;
    [SerializeField] private int currentHp = 100;

    private bool canCheckCollisions = true;

    private int damage = 10;

    #region Events Declaration
    public event Action OnPlayerOutOfHp;
    public event Action OnDamageReceived;
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
    }

    public void ResetComponent()
    {
        currentHp = startHp;
    }

    private void SetStartSettings()
    {
        currentHp = startHp;
    }

    private void DecreaseHp(int amount)
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
    }
}
