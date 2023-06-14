using UnityEngine;
using System.Collections;

public class BloodPuddleVFX : MonoBehaviour
{
    [Header("Delays")]
    [Space]
    [SerializeField] private float hideDelay = 5f;

    private ParticleSystem vfx;
    private PoolItem poolItem;

    private void Awake()
    {
        vfx = GetComponent<ParticleSystem>();
        poolItem = GetComponent<PoolItem>();

        poolItem.OnObjectAwakeStateSet += PlayVFX;
    }

    private void OnDestroy()
    {
        poolItem.OnObjectAwakeStateSet -= PlayVFX;
    }

    private void PlayVFX()
    {
        vfx.Play();
        StartCoroutine(HideVFXCoroutine());
    }

    private IEnumerator HideVFXCoroutine()
    {
        yield return new WaitForSeconds(hideDelay);
        poolItem.PoolItemsManager.ReturnItemToPool(poolItem);
    }
}
