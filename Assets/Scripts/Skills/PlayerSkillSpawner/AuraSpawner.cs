using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AuraSpawner : MonoBehaviour
{
    [Header("References")]
    [Space]
    [SerializeField] private GameObject particle;
    [SerializeField] private SphereCollider collider;
    [Header("Parameters")]
    [Space]
    [Range(0,1)][SerializeField] private float explosionTime;

    private float _cooldownTimer;

    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    #region Zenject

    [Inject]
    private void InjectDependencies(PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
    }

    #endregion
    
    
    public void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if (_cooldownTimer > _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.magicAuraSkillData.cooldown)
        {
            StartCoroutine(ExplodingSphere());
            _cooldownTimer = 0;
        }
    }

    private void OnEnable()
    {
        particle.SetActive(true);

    }

    private void OnDisable()
    {
        particle.SetActive(false);
    }

    private IEnumerator ExplodingSphere()
    {
        collider.enabled = true;
        yield return new WaitForSeconds(explosionTime);
        collider.enabled = false;

    }
}
