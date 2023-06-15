using UnityEngine;
using System.Collections;
using Zenject;

public class UpgradeSkillButton : ButtonInteractionHandler
{
    [Header("Upgrade Type")]
    [Space]
    [SerializeField] private bool doubleUpgrade = false;
    [Header("Panels")]
    [Space]
    [SerializeField] private SkillUpgradeDisplayPanel skillUpgradeDisplayPanel;

    private SkillsManager _skillsManager;

    //private void Start()
    //{
    //    if(transform.parent.TryGetComponent(out SkillUpgradeDisplayPanel panel))
    //    {
    //        skillUpgradeDisplayPanel = panel;
    //    }
    //}

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

        if (skillUpgradeDisplayPanel.SkillType == SkillBasicTypes.Active)
        {
            skillndex = (int)skillUpgradeDisplayPanel.ActiveSkill;
        }
        else
        {
            skillndex = (int)skillUpgradeDisplayPanel.PassiveSkill;
        }

        _skillsManager.DefineSkillToUpgrade((int)skillUpgradeDisplayPanel.SkillType, skillndex);

        if (doubleUpgrade)
        {
            StartCoroutine(UpgradeAdditionalSkillLevelCoroutine(skillndex));
        }
    }

    private IEnumerator UpgradeAdditionalSkillLevelCoroutine(int skillIndex)
    {
        yield return null;
        _skillsManager.DefineSkillToUpgrade((int)skillUpgradeDisplayPanel.SkillType, skillIndex);
    }
}
