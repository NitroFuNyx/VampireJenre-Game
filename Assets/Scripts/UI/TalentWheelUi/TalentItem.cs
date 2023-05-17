using UnityEngine;

public class TalentItem : MonoBehaviour
{
    [Header("Talent Type")]
    [Space]
    [SerializeField] private TalentsTypes talentType;

    public TalentsTypes TalentType { get => talentType; }
}
