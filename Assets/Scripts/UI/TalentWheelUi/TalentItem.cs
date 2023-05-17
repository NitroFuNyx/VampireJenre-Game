using UnityEngine;
using UnityEngine.UI;

public class TalentItem : MonoBehaviour
{
    [Header("Talent Type")]
    [Space]
    [SerializeField] private TalentsIndexes talentType;

    private Image talentImage;

    public TalentsIndexes TalentType { get => talentType; }

    private void Awake()
    {
        talentImage = GetComponent<Image>();
    }

    public void ChangeTalentImageColor(Color targetColor)
    {
        talentImage.color = targetColor;
    }
}
