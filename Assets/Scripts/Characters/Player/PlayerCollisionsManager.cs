using UnityEngine;
using System;
using System.Collections;
using Zenject;

public class PlayerCollisionsManager : MonoBehaviour
{
    [Header("Hp Data")]
    [Space]
    [SerializeField] private float currentHp = 100f;
    [Header("Delays")]
    [Space]
    [SerializeField] private float regenerationDelay = 5f;

    private PlayerCharacteristicsManager _playerCharacteristicsManager;
    private EnemiesCharacteristicsManager _enemiesCharacteristicsManager;

    private bool canCheckCollisions = true;

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
            DecreaseHp(_enemiesCharacteristicsManager.CurrentEnemiesData.damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.BossProjectile)
            DecreaseHp(20);
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerCharacteristicsManager playerCharacteristicsManager, EnemiesCharacteristicsManager enemiesCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
        _enemiesCharacteristicsManager = enemiesCharacteristicsManager;
    }
    #endregion Zenject

    public void ResetComponent()
    {
        SetStartSettings();
    }

    public void StartRegeneration()
    {
        StartCoroutine(RegenerateHpCoroutine());
    }

    public void StopRegeneration()
    {
        StopAllCoroutines();
    }

    private void SetStartSettings()
    {
        currentHp = _playerCharacteristicsManager.CurrentPlayerData.characterHp;
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

    private void IncreaseHp(float amount)
    {
        currentHp += amount;
        if (currentHp > _playerCharacteristicsManager.CurrentPlayerData.characterHp)
        {
            currentHp = _playerCharacteristicsManager.CurrentPlayerData.characterHp;
        }

        OnHpAmountChanged?.Invoke(currentHp, _playerCharacteristicsManager.CurrentPlayerData.characterHp);
    }

    private float GetReducedDamageAmount(float damage)
    {
        float reducedDamage = damage - (damage * _playerCharacteristicsManager.CurrentPlayerData.characterDamageReductionPercent) / CommonValues.maxPercentAmount;

        return reducedDamage;
    }

    private float GetHpAmountToRestore()
    {
        float regenerationAmount = (_playerCharacteristicsManager.CurrentPlayerData.characterRegenerationPercent *
                                    _playerCharacteristicsManager.CurrentPlayerData.characterHp) / CommonValues.maxPercentAmount;
        return regenerationAmount;
    }

    private IEnumerator RegenerateHpCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(regenerationDelay);
            IncreaseHp(GetHpAmountToRestore());
            Debug.Log($"Restore {GetHpAmountToRestore()} hp");
        }
    }
}
