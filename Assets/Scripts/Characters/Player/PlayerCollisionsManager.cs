using UnityEngine;
using System;
using Zenject;

public class PlayerCollisionsManager : MonoBehaviour
{
    [Header("Hp Data")]
    [Space]
    //[SerializeField] private float startHp = 100f;
    [SerializeField] private float currentHp = 100f;

    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    private bool canCheckCollisions = true;

    private float maxPercentAmount = 100f;

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
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
    }
    #endregion Zenject

    public void ResetComponent()
    {
        SetStartSettings();
    }

    private void SetStartSettings()
    {
        //startHp = _playerCharacteristicsManager.CurrentPlayerData.characterHp;
        currentHp = _playerCharacteristicsManager.CurrentPlayerData.characterHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == Layers.BossProjectile)
            DecreaseHp(20);
    }

    private void DecreaseHp(float amount)
    {
        currentHp -= GetReducedDamageAmount(amount);
        if (currentHp <= 0)
        {
            canCheckCollisions = false;
            OnPlayerOutOfHp?.Invoke();
        }
        else
        {
            OnDamageReceived?.Invoke();
        }

        OnHpAmountChanged?.Invoke(currentHp, _playerCharacteristicsManager.CurrentPlayerData.characterHp);
    }

    private float GetReducedDamageAmount(float damage)
    {
        float reducedDamage = damage - (damage * _playerCharacteristicsManager.CurrentPlayerData.characterDamageReductionPercent) / maxPercentAmount;

        return reducedDamage;
    }
}
