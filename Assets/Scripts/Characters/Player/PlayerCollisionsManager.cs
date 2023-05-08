using UnityEngine;
using System;

public class PlayerCollisionsManager : MonoBehaviour
{
    [Header("Hp Data")]
    [Space]
    [SerializeField] private int startHp = 100;
    [SerializeField] private int currentHp = 100;

    private bool canCheckCollisions = true;

    #region Events Declaration
    public event Action OnPlayerOutOfHp;
    public event Action OnDamageReceived;
    #endregion Events Declaration

    private void Start()
    {
        SetStartSettings();
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
