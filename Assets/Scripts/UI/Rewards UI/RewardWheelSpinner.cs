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

    private MenuButtonsUI _menuButtonsUI;

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

    public bool CanSpin { get => canSpin; private set => canSpin = value; }

    private void Start()
    {
        for(int i = 0; i < rewardsList.Count; i++)
        {
            updatedRewardsList.Add(rewardsList[i]);
        }
    }

    private void Update()
    {
        if(isSpinning)
        {
            if(spinSpeed > speedTreshold)
            {
                spinSpeed -= maxSpeedSlowdownDelta * Time.deltaTime;
            }
            else
            {
                spinSpeed -= minSpeedSlowdownDelta * Time.deltaTime;
            }

            rotation += rotationCoefficient * Time.deltaTime * spinSpeed;
            wheelImage.localRotation = Quaternion.Euler(0f, 0f, -rotation);

            if(spinSpeed < 0f)
            {
                spinSpeed = 0f;
                isSpinning = false;
                DefineRewardIndex();
                canSpin = true;
                _menuButtonsUI.ChangeScreenBlockingState(false);
            }
        }
    }

    #region Zenject
    [Inject]
    private void Construct(MenuButtonsUI menuButtonsUI)
    {
        _menuButtonsUI = menuButtonsUI;
    }
    #endregion Zenject

    public void Spin()
    {
        if(canSpin)
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
    }
}
