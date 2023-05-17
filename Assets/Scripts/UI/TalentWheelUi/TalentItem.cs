using UnityEngine;

public class TalentItem : MonoBehaviour
{
    [Header("Talent Type")]
    [Space]
    [SerializeField] private TalentsIndexes talentType;

    public TalentsIndexes TalentType { get => talentType; }
}
