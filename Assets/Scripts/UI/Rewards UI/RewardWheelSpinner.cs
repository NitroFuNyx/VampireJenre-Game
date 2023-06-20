using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class RewardWheelSpinner : MonoBehaviour
{
    [Header("Spin Data")]
    [Space]
    [SerializeField] private float minSpinSpeed = 10f;
    [SerializeField] private float maxSpinSpeed = 10f;
    [Header("Rewards")]
    [Space]
    [SerializeField] private List<RewardObject> rewardsList = new List<RewardObject>();
    [Header("Internal References")]
    [Space]
    [SerializeField] private Transform wheelImage;
    [SerializeField] private Transform pointer;
    [Header("Buttons")]
    [Space]
    [SerializeField] private SpinButton spinButton;

    private MenuButtonsUI _menuButtonsUI;
    private RewardsManager _rewardsManager;
    private AdsController _adsController;
    private AdsManager _adsManager;

    private List<RewardObject> updatedRewardsList = new List<RewardObject>();

    private RewardObject currentReward;

    private const float CircleAngle = 360f;

    private float spinSpeed;
    private float rotation = 0f;
    private float speedTreshold = 2f;
    private float maxSpeedSlowdownDelta = 2f;
    private float minSpeedSlowdownDelta = 0.3f;
    private float rotationCoefficient = 100f;

    private bool canSpin = true;
    private bool isSpinning = false;

    private bool rewardRequested = false;

    #region Events Declartation
    public event System.Action<RewardObject> OnRewardDefined;
    #endregion Events Declaration

    public bool CanSpin { get => canSpin; private set => canSpin = value; }

    private void Start()
    {
        for (int i = 0; i < rewardsList.Count; i++)
        {
            updatedRewardsList.Add(rewardsList[i]);
        }

        _rewardsManager.OnSpinButtonUpdateRequired += RewardsManager_OnSpinButtonUpdateRequired_ExecuteReaction;

        _adsController.OnRewardAdViewed += AdsController_RewardGranted_ExecuteReaction;
    }

    private void Update()
    {
        if (isSpinning)
        {
            if (spinSpeed > speedTreshold)
            {
                spinSpeed -= maxSpeedSlowdownDelta * Time.deltaTime;
            }
            else
            {
                spinSpeed -= minSpeedSlowdownDelta * Time.deltaTime;
            }

            rotation += rotationCoefficient * Time.deltaTime * spinSpeed;
            wheelImage.localRotation = Quaternion.Euler(0f, 0f, -rotation);

            if (spinSpeed < 0f)
            {
                spinSpeed = 0f;
                isSpinning = false;
                DefineRewardIndex();
                canSpin = true;
                _menuButtonsUI.ChangeScreenBlockingState(false);
            }
        }
    }

    private void OnDestroy()
    {
        _rewardsManager.OnSpinButtonUpdateRequired -= RewardsManager_OnSpinButtonUpdateRequired_ExecuteReaction;

        _adsController.OnRewardAdViewed -= AdsController_RewardGranted_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(MenuButtonsUI menuButtonsUI, RewardsManager rewardsManager, AdsController adsController,
                           AdsManager adsManager)
    {
        _menuButtonsUI = menuButtonsUI;
        _rewardsManager = rewardsManager;
        _adsController = adsController;
        _adsManager = adsManager;
    }
    #endregion Zenject

    public void PressSpinButton()
    {
        if (!_rewardsManager.FreeRewardSpinUsed)
        {
            _rewardsManager.UseFreeRewardSpin();
            spinButton.SetState_FreeSpinUsed();
            Spin();
        }
        else if (_rewardsManager.FreeRewardSpinUsed && !_rewardsManager.RewardForAdSpinUsed)
        {
            if (!_adsManager.BlockAdsOptionPurchased)
            {
                rewardRequested = true;
                _adsController.LoadRewarded();
            }
            else
            {
                _rewardsManager.UseRewardSpinForAd();
                spinButton.SetState_AllSpinsUsed();
                Spin();
            }
        }
    }

    public void Spin()
    {
        if (canSpin)
        {
            canSpin = false;
            _menuButtonsUI.ChangeScreenBlockingState(true);
            spinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);
            rotation = 0f;
            isSpinning = true;
        }
    }

    private void DefineRewardIndex()
    {
        updatedRewardsList = updatedRewardsList.OrderBy(s => Vector3.Distance(s.transform.position, pointer.position)).ToList();
        currentReward = updatedRewardsList[0];
        OnRewardDefined?.Invoke(currentReward);
    }

    private void RewardsManager_OnSpinButtonUpdateRequired_ExecuteReaction()
    {
        if (_rewardsManager.FreeRewardSpinUsed && _rewardsManager.RewardForAdSpinUsed)
        {
            spinButton.SetState_AllSpinsUsed();
        }
        else if (_rewardsManager.FreeRewardSpinUsed)
        {
            spinButton.SetState_FreeSpinUsed();
        }
    }

    private void AdsController_RewardGranted_ExecuteReaction()
    {
        if(rewardRequested)
        {
            Debug.Log($"Reward For Ad Granted");
            rewardRequested = false;
            _rewardsManager.UseRewardSpinForAd();
            spinButton.SetState_AllSpinsUsed();
            Spin();
        }
    }
}
