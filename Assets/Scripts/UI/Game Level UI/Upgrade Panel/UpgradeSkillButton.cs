using Zenject;

public class UpgradeSkillButton : ButtonInteractionHandler
{
    private SkillsManager _skillsManager;

    private SkillUpgradeDisplayPanel skillUpgradeDisplayPanel;

    private void Start()
    {
        if(transform.parent.TryGetComponent(out SkillUpgradeDisplayPanel panel))
        {
            skillUpgradeDisplayPanel = panel;
        }
    }

    #region Zenject
    [Inject]
    private void Construct(SkillsManager skillsManager)
    {
        _skillsManager = skillsManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        int skillndex;

        if(skillUpgradeDisplayPanel.SkillType == SkillBasicTypes.Active)
        {
            skillndex = (int)skillUpgradeDisplayPanel.ActiveSkill;
        }
        else
        {
            skillndex = (int)skillUpgradeDisplayPanel.PassiveSkill;
        }

        _skillsManager.DefineSkillToUpgrade((int)skillUpgradeDisplayPanel.SkillType, skillndex);
    }
}
