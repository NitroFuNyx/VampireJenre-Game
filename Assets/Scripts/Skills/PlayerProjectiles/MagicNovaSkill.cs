using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class MagicNovaSkill : MonoBehaviour
{
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

    private void Start()
    {
        if (_playerCharacteristicsManager != null)
            transform.localScale =
                new Vector3(_playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.pulseAuraSkillData.radius,
                    0, _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.pulseAuraSkillData.radius);
        else
        {
            Debug.Log("Zenject couldn't load");
        }
    }

    public void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if (_cooldownTimer < _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.pulseAuraSkillData.cooldown)
        {
            StartCoroutine(ExplodingSphere());
            _cooldownTimer = 0;
        }
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