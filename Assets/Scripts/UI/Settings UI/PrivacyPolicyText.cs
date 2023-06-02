using UnityEngine;

public class PrivacyPolicyText : MonoBehaviour
{
    [Header("Language")]
    [Space]
    [SerializeField] private Languages language;

    public Languages Language { get => language; }
}
