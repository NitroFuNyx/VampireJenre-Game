using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class MagicNovaSkill : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private ParticleSystem novaVFX;
    [SerializeField] private SphereCollider collider;
    private float _cooldownTimer;
    [Range(0,1)][SerializeField] private float explosionTime;

    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    [Inject]
    private void InjectDependencies(PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
    }
    public void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if (_cooldownTimer > _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.pulseAuraSkillData.cooldown)
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
        novaVFX.Stop(true);
        novaVFX.Play(true);
        collider.enabled = true;
        yield return new WaitForSeconds(explosionTime);
        collider.enabled = false;

    }
}
