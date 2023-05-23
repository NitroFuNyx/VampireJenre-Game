using System.Collections;
using UnityEngine;

public class MagicNovaSkill : MonoBehaviour
{
    [SerializeField] private ParticleSystem novaVFX;
    [SerializeField] private SphereCollider collider;
    [Range(0,10)][SerializeField] private float cooldown;
    private float _cooldownTimer;
    [Range(0,1)][SerializeField] private float explosionTime;

    public void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if (cooldown < _cooldownTimer)
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
