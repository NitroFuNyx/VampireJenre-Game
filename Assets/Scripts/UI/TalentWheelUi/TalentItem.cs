using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TalentItem : MonoBehaviour
{
    [Header("Talent Type")]
    [Space]
    [SerializeField] private TalentsIndexes talentIndex;

    private Image talentImage;
    private Image frameImage;

    private float changeFrameAlhaDuration = 0.08f;

    public TalentsIndexes TalentIndex { get => talentIndex; }

    private void Awake()
    {
        talentImage = GetComponent<Image>();
        frameImage = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        frameImage.DOFade(0f, changeFrameAlhaDuration);
    }

    public void ChangeTalentImageColor(Color targetColor)
    {
        talentImage.color = targetColor;
    }

    public void ChangeTalentSelectionState(bool selected)
    {
        if(selected)
        {
            frameImage.DOFade(1f, changeFrameAlhaDuration);
        }
        else
        {
            frameImage.DOFade(0f, changeFrameAlhaDuration);
        }
    }
}
