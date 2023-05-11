using System.Collections;
using UnityEngine;

public class SlashSkill : MonoBehaviour
{
    [SerializeField] private ParticleSystem slashVFX;
    [Range(0.1f, 5)] [SerializeField] private float coolDown;
    [SerializeField] private MeshCollider collider;
    [Range(0.1f, 5)] [SerializeField] private float timeToDisable;
    [Range(0.1f, 5)] [SerializeField] private float colliderRadius;
    
    private float timeCounter;

    

    private void Update()
    {
        if (timeCounter > coolDown)
        {
                slashVFX.Play();
                timeCounter = 0;
                StartCoroutine(DisablingSlash());
        }

        timeCounter += Time.fixedDeltaTime;
    }

    private IEnumerator DisablingSlash()
    {
        collider.convex = true;
        collider.isTrigger = true;
        yield return new WaitForSeconds(timeToDisable);
        collider.isTrigger = false;

        collider.convex = false;
    }

    
}