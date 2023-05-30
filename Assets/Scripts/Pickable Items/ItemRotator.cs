using UnityEngine;
using System.Collections;

public class ItemRotator : MonoBehaviour
{
    [Header("Rotation Data")]
    [Space]
    [SerializeField] private float rotationValue = 100f;

    private void OnEnable()
    {
        StartCoroutine(RotateCoroutine());
    }

    protected IEnumerator RotateCoroutine()
    {
        yield return null;
        while (true)
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * rotationValue, 0f));
            yield return null;
        }
    }
}
