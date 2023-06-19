using UnityEngine;
using Zenject;

public class SkillsInfoButton : ButtonInteractionHandler
{
    [Header("Skills Type")]
    [Space]
    [SerializeField] private SkillBasicTypes skillsType;
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite buttonActivatedSprite;
    [SerializeField] private Sprite buttonDeactivatedSprite;

    private SkillsInfoUI _skillsInfoUI;

    private void Start()
    {
        _skillsInfoUI.OnCurrentSkillsInfoPanelChanged += SkillsInfoPanel_CurrentSkillsPanelChanged_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _skillsInfoUI.OnCurrentSkillsInfoPanelChanged -= SkillsInfoPanel_CurrentSkillsPanelChanged_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(SkillsInfoUI skillsInfoUI)
    {
        _skillsInfoUI = skillsInfoUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        ShowAnimation_ButtonPressed();
        _skillsInfoUI.ShowSkillsInfo(skillsType);
    }

    private void SkillsInfoPanel_CurrentSkillsPanelChanged_ExecuteReaction(SkillBasicTypes skillType)
    {
        if(this.skillsType == skillType)
        {
            buttonImage.sprite = buttonActivatedSprite;
        }
        else
        {
            buttonImage.sprite = buttonDeactivatedSprite;
        }
    }
}
