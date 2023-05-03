using UnityEngine;

public class EnemyVFXManager : MonoBehaviour
{
    [Header("VFX")]
    [Space]
    [SerializeField] private ParticleSystem characterAppearanceVFX;

    public void PlayVFX_CharacterAppearance()
    {
        if(characterAppearanceVFX != null)
        characterAppearanceVFX.Play();
    }
}
