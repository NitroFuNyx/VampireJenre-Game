using UnityEngine;
using System;
using System.Collections.Generic;
using Zenject;

public class TreasureChestInfoPanel : GameLevelSubPanel
{
    [Header("Items Panels")]
    [Space]
    [SerializeField] private TreasureItemInfoPanel firstTreasurePanel;
    [SerializeField] private TreasureItemInfoPanel secondTreasurePanel;

    private List<UpgradeSkillData> treasureSkillsForUpgradeList = new List<UpgradeSkillData>();
    private List<TreasureChestResourceDataStruct> treasureResourcesList = new List<TreasureChestResourceDataStruct>();
    private List<TreasureChestItems> treasureItemsList = new List<TreasureChestItems>();

    private PickableItemsManager _pickableItemsManager;
    private SkillsManager _skillsManager;
    private ResourcesManager _resourcesManager;
    private AdsManager _adsManager;
    private AdsController _adsController;

    private bool rewardRequested = false;

    public event Action OnTreasureChestItemsCollected;

    private void Start()
    {
        _pickableItemsManager.OnTreasureChestCollected += PickableItemsManager_OnTreasureChestCollected_ExecuteReaction;

        _adsController.OnRewardAdViewed += AdsController_RewardGranted_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _pickableItemsManager.OnTreasureChestCollected -= PickableItemsManager_OnTreasureChestCollected_ExecuteReaction;

        _adsController.OnRewardAdViewed -= AdsController_RewardGranted_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(PickableItemsManager pickableItemsManager, SkillsManager skillsManager, ResourcesManager resourcesManager,
                           AdsManager adsManager, AdsController adsController)
    {
        _pickableItemsManager = pickableItemsManager;
        _skillsManager = skillsManager;
        _resourcesManager = resourcesManager;
        _adsManager = adsManager;
        _adsController = adsController;
    }
    #endregion Zenject

    public void CollectTreasures(bool getAllTreasures)
    {
        GetFirstTreasure();

        if(getAllTreasures)
        {
            if(!_adsManager.BlockAdsOptionPurchased)
            {
                rewardRequested = true;
                _adsController.LoadRewarded();
            }
            //GetSecondTreasure();
        }
        else
        {
            OnTreasureChestItemsCollected?.Invoke();
            HidePanel();
        }
    }

    private void GetFirstTreasure()
    {
        if (treasureItemsList[0] == TreasureChestItems.Skill)
        {
            int skillndex;

            if (treasureSkillsForUpgradeList[0].SkillType == SkillBasicTypes.Active)
            {
                skillndex = (int)treasureSkillsForUpgradeList[0].ActiveSkill;
            }
            else
            {
                skillndex = (int)treasureSkillsForUpgradeList[0].PassiveSkill;
            }

            _skillsManager.DefineSkillToUpgrade((int)treasureSkillsForUpgradeList[0].SkillType, skillndex);
        }
        else
        {
            _resourcesManager.AddTreasureForPickingUpTreasureChest(treasureResourcesList[0].resourceType, treasureResourcesList[0].resourceAmount);
        }
    }

    private void GetSecondTreasure()
    {
        if (treasureItemsList[1] == TreasureChestItems.Skill)
        {
            int skillndex;

            if (treasureSkillsForUpgradeList[1].SkillType == SkillBasicTypes.Active)
            {
                skillndex = (int)treasureSkillsForUpgradeList[1].ActiveSkill;
            }
            else
            {
                skillndex = (int)treasureSkillsForUpgradeList[1].PassiveSkill;
            }

            _skillsManager.DefineSkillToUpgrade((int)treasureSkillsForUpgradeList[1].SkillType, skillndex);
        }
        else
        {
            if (treasureResourcesList.Count == 1)
            {
                _resourcesManager.AddTreasureForPickingUpTreasureChest(treasureResourcesList[0].resourceType, treasureResourcesList[0].resourceAmount);
            }
            else if (treasureResourcesList.Count > 1)
            {
                _resourcesManager.AddTreasureForPickingUpTreasureChest(treasureResourcesList[1].resourceType, treasureResourcesList[1].resourceAmount);
            }
        }
    }

    private void PickableItemsManager_OnTreasureChestCollected_ExecuteReaction(TreasureChestItems firstItem, TreasureChestItems secondItem)
    {
        ShowPanel();

        treasureSkillsForUpgradeList.Clear();
        treasureResourcesList.Clear();
        treasureItemsList.Clear();

        treasureItemsList.Add(firstItem);
        treasureItemsList.Add(secondItem);

        if(firstItem == TreasureChestItems.Skill || secondItem == TreasureChestItems.Skill)
        {
            treasureSkillsForUpgradeList = _skillsManager.GetRandomSkillsToUpgradeList(2);
        }

        if(firstItem == TreasureChestItems.Skill)
        {
            firstTreasurePanel.UpdateSkillPanelData(treasureSkillsForUpgradeList[0]);
        }
        else
        {
            TreasureChestResourceDataStruct resource = _resourcesManager.GetResourceDataForPickingUpTreasureChest(firstItem);
            firstTreasurePanel.UpdateResourcePanelData(resource);
            treasureResourcesList.Add(resource);
        }

        if (secondItem == TreasureChestItems.Skill)
        {
            secondTreasurePanel.UpdateSkillPanelData(treasureSkillsForUpgradeList[1]);
        }
        else
        {
            TreasureChestResourceDataStruct resource = _resourcesManager.GetResourceDataForPickingUpTreasureChest(secondItem);
            secondTreasurePanel.UpdateResourcePanelData(resource);
            treasureResourcesList.Add(resource);
        }
    }

    private void AdsController_RewardGranted_ExecuteReaction()
    {
        if(rewardRequested)
        {
            rewardRequested = false;
            Debug.Log($"Reward for Chest");
            GetSecondTreasure();

            OnTreasureChestItemsCollected?.Invoke();
            HidePanel();
        }
    }
}
