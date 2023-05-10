using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class RewardWheelSpinner : MonoBehaviour
{
    [Header("Spin Data")]
    [Space]
    [SerializeField] private float minSpinSpeed = 10f;
    [SerializeField] private float maxSpinSpeed = 10f;
    [Header("Rewards")]
    [Space]
    [SerializeField] private List<int> rewardsList = new List<int>();
    [Header("Internal References")]
    [Space]
    [SerializeField] private Transform wheelImage;

    private MenuButtonsUI _menuButtonsUI;

    private float spinSpeed;

    private const float CircleAngle = 360f;

    private float rotation = 0f;

    private int rewardNumber = -1;

    private bool canSpin = true;
    private bool isSpinning = false;

    public bool CanSpin { get => canSpin; private set => canSpin = value; }

    #region Zenject
    [Inject]
    private void Construct(MenuButtonsUI menuButtonsUI)
    {
        _menuButtonsUI = menuButtonsUI;
    }
    #endregion Zenject

    private void Update()
    {
        if(isSpinning)
        {
            if(spinSpeed > 2f)
            {
                spinSpeed -= 2 * Time.deltaTime;
            }
            else
            {
                spinSpeed -= 0.3f * Time.deltaTime;
            }

            rotation += 100 * Time.deltaTime * spinSpeed;
            wheelImage.localRotation = Quaternion.Euler(0f, 0f, -rotation);

            if(spinSpeed < 0f)
            {
                spinSpeed = 0f;
                isSpinning = false;
                rewardNumber = (int)((rotation % CircleAngle) / (CircleAngle / rewardsList.Count));
                DefineRewardIndex();
                canSpin = true;
                _menuButtonsUI.ChangeScreenBlockingState(false);
                //Debug.Log($"Reward {rewardNumber}");
            }
        }
    }

    [ContextMenu("Spin")]
    public void Spin()
    {
        if(canSpin)
        {
            canSpin = false;
            _menuButtonsUI.ChangeScreenBlockingState(true);
            spinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);
            rotation = 0f;
            rewardNumber = -1;
            isSpinning = true;
        }
    }

    private void DefineRewardIndex()
    {
        float lastAngle;
        if (wheelImage.eulerAngles.z <= 180f)
        {
            lastAngle = wheelImage.eulerAngles.z;
        }
        else
        {
            lastAngle = wheelImage.eulerAngles.z - 360f;
        }
        //Debug.Log($"{lastAngle}");

        float newAngle = wheelImage.eulerAngles.z;


        //if (newAngle > -27 && newAngle <= 18)
        //{
        //    rewardNumber = 0;
        //}
        //else if (newAngle > 18 && newAngle <= 63)
        //{
        //    rewardNumber = 1;
        //}
        //else if (newAngle > 63 && newAngle <= 108)
        //{
        //    rewardNumber = 2;
        //}
        //else if (newAngle > 108 && newAngle <= 153)
        //{
        //    rewardNumber = 3;
        //}
        //else if (newAngle > 153 && newAngle <= 198)
        //{
        //    rewardNumber = 4;
        //}
        //else if (newAngle > 198 && newAngle <= 243)
        //{
        //    rewardNumber = 5;
        //}
        //else if (newAngle > 243 && newAngle <= 288)
        //{
        //    rewardNumber = 6;
        //}
        //else if (newAngle > 288 && newAngle <= 333)
        //{
        //    rewardNumber = 7;
        //}
    }
}
