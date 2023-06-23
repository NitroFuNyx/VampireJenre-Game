using TMPro;
using DG.Tweening;
using UnityEngine;

public class TextScaleHandler : MonoBehaviour
{
    [Header("Scale Data")]
    [Space]
    [SerializeField] private Vector3 scaleVector = new Vector3();
    [SerializeField] private float scaleDuration;
    [SerializeField] private int scaleFreequency;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Scale();
    }

    private void Scale()
    {
        text.transform.DOPunchScale(scaleVector, scaleDuration, scaleFreequency).SetLoops(-1);
    }
}
