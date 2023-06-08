using UnityEngine;
using Zenject;

public class SkillsInfoButton : ButtonInteractionHandler
{
    [Header("Skills Type")]
    [Space]
    [SerializeField] private SkillBasicTypes skillsType;

    private SkillsInfoUI _skillsInfoUI;

    #region Zenject
    [Inject]
    private void Construct(SkillsInfoUI skillsInfoUI)
    {
        _skillsInfoUI = skillsInfoUI;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        _skillsInfoUI.ShowSkillsInfo(skillsType);
    }
}
