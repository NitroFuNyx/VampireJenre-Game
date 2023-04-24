using System.Collections.Generic;
using UnityEngine;

public class MapPillarLightHandler : MonoBehaviour
{
    [Header("Light")]
    [Space]
    [SerializeField] private List<Light> lightComponentsList = new List<Light>();
    [Space]
    [SerializeField] private bool activateLight = true;

    private void OnValidate()
    {
        ChangeLightState(activateLight);
    }

    private void Start()
    {
        ChangeLightState(activateLight);
    }

    private void ChangeLightState(bool isActive)
    {
        for(int i = 0; i < lightComponentsList.Count; i++)
        {
            lightComponentsList[i].enabled = isActive;
        }
    }
}
