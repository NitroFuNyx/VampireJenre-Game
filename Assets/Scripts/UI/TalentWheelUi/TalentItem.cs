using UnityEngine;
using UnityEngine.UI;

public class TalentItem : MonoBehaviour
{
    [Header("Talent Type")]
    [Space]
    [SerializeField] private TalentsIndexes talentIndex;

    private Image talentImage;

    public TalentsIndexes TalentIndex { get => talentIndex; }

    private void Awake()
    {
        talentImage = GetComponent<Image>();
    }

    public void ChangeTalentImageColor(Color targetColor)
    {
        talentImage.color = targetColor;
    }
}
