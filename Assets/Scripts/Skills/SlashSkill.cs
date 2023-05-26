using System.Collections;
using UnityEngine;
using Zenject;

public class SlashSkill : MonoBehaviour
{
    [SerializeField] private ParticleSystem slashVFX;
    [Range(0.1f, 5)] [SerializeField] private float coolDown;
    [SerializeField] private MeshCollider collider;
    [Range(0.1f, 5)] [SerializeField] private float timeToDisable;
    [Range(0.1f, 5)] [SerializeField] private float colliderRadius;
    private PlayerCharacteristicsManager _playerCharacteristicsManager;

    private float timeCounter;

    [Inject]
    private void InjectDependencies(PlayerCharacteristicsManager playerCharacteristicsManager)
    {
        _playerCharacteristicsManager = playerCharacteristicsManager;
    }

    private void Update()
    {
        if (timeCounter > _playerCharacteristicsManager.CurrentPlayerData.playerSkillsData.weaponStrikeSkillData.cooldown)
        {
                slashVFX.Play();
                timeCounter = 0;
                StartCoroutine(DisablingSlash());
        }

        timeCounter += Time.fixedDeltaTime;
    }

    private IEnumerator DisablingSlash()
    {
        collider.enabled = true;
        yield return new WaitForSeconds(timeToDisable);
        collider.enabled = false;


    }

    
}