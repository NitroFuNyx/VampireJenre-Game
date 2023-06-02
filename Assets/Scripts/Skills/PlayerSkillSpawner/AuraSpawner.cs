using UnityEngine;

public class AuraSpawner : MonoBehaviour
{
    [SerializeField] private GameObject particle;

    private void Start()
    {
        particle.SetActive(true);
    }
}
