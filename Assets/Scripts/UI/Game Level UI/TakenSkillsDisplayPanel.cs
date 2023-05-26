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
    [Header("Skills")]
    [Space]
    [SerializeField] private List<ActiveSkills> activeSkillsList = new List<ActiveSkills>();
    [SerializeField] private List<PassiveSkills> passiveSkillsList = new List<PassiveSkills>();

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

    public void SkillTaken_ExecuteReaction(int skillCategoryIndex, int skillIndex, Sprite skillSprite)
    {
        Image skillImage;

        if ((SkillBasicTypes)skillCategoryIndex == SkillBasicTypes.Active)
        {
            if(!activeSkillsList.Contains((ActiveSkills)skillIndex))
            {
                activeSkillsList.Add((ActiveSkills)skillIndex);
                skillImage = activeSkillsImagesList[activeSkillsTakenAmount];
                activeSkillsTakenAmount++;
                skillImage.sprite = skillSprite;
                skillImage.DOFade(1f, changeAlphaDuration);
                image = skillImage;
            }
            else
            {

            }
        }
        else
        {
            if (!passiveSkillsList.Contains((PassiveSkills)skillIndex))
            {
                passiveSkillsList.Add((PassiveSkills)skillIndex);
                skillImage = passiveSkillsImagesList[passiveSkillsTakenAmount];
                passiveSkillsTakenAmount++;
                skillImage.sprite = skillSprite;
                skillImage.DOFade(1f, changeAlphaDuration);
                image = skillImage;
            }       
        }
    }

    public void ResetSkillsData()
    {
        ChangeSkillImagesListAlpha(activeSkillsImagesList, 0f);
        ChangeSkillImagesListAlpha(passiveSkillsImagesList, 0f);

        activeSkillsList.Clear();
        passiveSkillsList.Clear();

        activeSkillsTakenAmount = 0;
        passiveSkillsTakenAmount = 0;
    }

    private void ChangeSkillImagesListAlpha(List<Image> imagesList, float alpha)
    {
        for(int i = 0; i < imagesList.Count; i++)
        {
            imagesList[i].DOFade(alpha, changeAlphaDuration);
        }
    }
}
