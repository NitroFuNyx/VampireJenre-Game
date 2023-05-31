using System.Collections.Generic;
using UnityEngine;

public class MapPillarLightHandler : MonoBehaviour
{
    [Header("Light")]
    [Space]
    [SerializeField] private List<Light> lightComponentsList = new List<Light>();
    [Space]
    [SerializeField] private bool activateLight = true;
    [Header("VFX")]
    [Space]
    [SerializeField] private List<ParticleSystem> vfxList = new List<ParticleSystem>();
    [Header("Player Vision Detector")]
    [Space]
    [SerializeField] private PlayerVisionBorderDetector visionBorderDetector;

    private void OnValidate()
    {
        ChangeLightState(activateLight);
    }

    private void Start()
    {
        ChangeLightState(activateLight);

        visionBorderDetector.OnObjectBecomeVisibleForPlayer += PlayerVisionBorderDetector_ObjectBecomeVisibleForPlayer_ExecuteReaction;
        visionBorderDetector.OnObjectStoppedBeingVisibleForPlayer += PlayerVisionBorderDetector_ObjectStoppedBeingVisibleForPlayer_ExecuteReaction;
    }

    private void OnDestroy()
    {
        visionBorderDetector.OnObjectBecomeVisibleForPlayer -= PlayerVisionBorderDetector_ObjectBecomeVisibleForPlayer_ExecuteReaction;
        visionBorderDetector.OnObjectStoppedBeingVisibleForPlayer -= PlayerVisionBorderDetector_ObjectStoppedBeingVisibleForPlayer_ExecuteReaction;
    }

    private void ChangeLightState(bool isActive)
    {
        for(int i = 0; i < lightComponentsList.Count; i++)
        {
            lightComponentsList[i].enabled = isActive;
        }
    }

    private void PlayerVisionBorderDetector_ObjectBecomeVisibleForPlayer_ExecuteReaction()
    {
        ChangeVfxActivationState(true);
    }

    private void PlayerVisionBorderDetector_ObjectStoppedBeingVisibleForPlayer_ExecuteReaction()
    {
        ChangeVfxActivationState(false);
    }

    private void ChangeVfxActivationState(bool enabled)
    {
        for(int i = 0; i < vfxList.Count; i++)
        {
            if(enabled)
            {
                vfxList[i].Play();
            }
            else
            {
                vfxList[i].Stop();
            }
        }
    }
}
