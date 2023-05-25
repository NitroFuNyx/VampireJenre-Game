using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TakenSkillsDisplayPanel : MonoBehaviour
{
    [Header("Skills Images")]
    [Space]
    [SerializeField] private List<Image> activeSkillsImagesList = new List<Image>();
    [SerializeField] private List<Image> passiveSkillsImagesList = new List<Image>();

    private SkillsManager _skillsManager;
    private Image image;

    private float changeAlphaDuration = 0.1f;

    private int activeSkillsTakenAmount = 0;
    private int passiveSkillsTakenAmount = 0;

    private void Start()
    {
        ChangeSkillImagesListAlpha(activeSkillsImagesList, 0f);
        ChangeSkillImagesListAlpha(passiveSkillsImagesList, 0f);
    }

    private void OnDestroy()
    {
        
    }

    public void SkillTaken_ExecuteReaction(int skillTypeIndex, Sprite skillSprite)
    {
        Image skillImage;

        if ((SkillBasicTypes)skillTypeIndex == SkillBasicTypes.Active)
        {
            skillImage = activeSkillsImagesList[activeSkillsTakenAmount];
            activeSkillsTakenAmount++;
        }
        else
        {
            skillImage = passiveSkillsImagesList[passiveSkillsTakenAmount];
            passiveSkillsTakenAmount++;
        }

        skillImage.sprite = skillSprite;
        skillImage.DOFade(1f, changeAlphaDuration);
        image = skillImage;
    }

    private void ChangeSkillImagesListAlpha(List<Image> imagesList, float alpha)
    {
        for(int i = 0; i < imagesList.Count; i++)
        {
            imagesList[i].DOFade(alpha, changeAlphaDuration);
        }
    }
}
