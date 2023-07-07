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
    private AdsManager _adsManager;
    private AdsController _adsController;

    private bool rewardRequested = false;
    private bool firstSkillLearned = false;

    private int currentSkillIndex;
    private int skillndex;

    private void Start()
    {
        _adsController.OnRewardAdViewed += AdsController_RewardGranted_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _adsController.OnRewardAdViewed -= AdsController_RewardGranted_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(SkillsManager skillsManager, AdsManager adsManager, AdsController adsController)
    {
        _skillsManager = skillsManager;
        _adsManager = adsManager;
        _adsController = adsController;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if (doubleUpgrade)
        {
            if (!_adsManager.BlockAdsOptionPurchased)
            {
                rewardRequested = true;
                currentSkillIndex = skillndex;
                _adsController.LoadRewarded();
                //StartCoroutine(UpgradeAdditionalSkillLevelCoroutine(skillndex));
            }
        }
        else
        {
            firstSkillLearned = true;

            //int skillndex;

            if (skillUpgradeDisplayPanel.SkillType == SkillBasicTypes.Active)
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

    private void AdsController_RewardGranted_ExecuteReaction()
    {
        if(rewardRequested)
        {
            if (!firstSkillLearned)
            {
                firstSkillLearned = true;

                //int skillndex;

                if (skillUpgradeDisplayPanel.SkillType == SkillBasicTypes.Active)
                {
                    skillndex = (int)skillUpgradeDisplayPanel.ActiveSkill;
                }
                else
                {
                    skillndex = (int)skillUpgradeDisplayPanel.PassiveSkill;
                }

                _skillsManager.DefineSkillToUpgrade((int)skillUpgradeDisplayPanel.SkillType, skillndex);
            }

            Debug.Log($"Ad Reward For Scroll");
            rewardRequested = false;
            firstSkillLearned = false;
            StartCoroutine(UpgradeAdditionalSkillLevelCoroutine(currentSkillIndex));
        }
    }

    private IEnumerator UpgradeAdditionalSkillLevelCoroutine(int skillIndex)
    {
        yield return null;
        _skillsManager.DefineSkillToUpgrade((int)skillUpgradeDisplayPanel.SkillType, skillIndex);
    }
}
