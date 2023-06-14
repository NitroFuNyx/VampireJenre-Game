using UnityEngine;
using TMPro;
using Zenject;

public class SpinButton : ButtonInteractionHandler
{
    [Header("Sprites")]
    [Space]
    [SerializeField] private Sprite standartButtonSprite;
    [SerializeField] private Sprite spinUsedButtonSprite;
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI spinText;
    [SerializeField] private TextMeshProUGUI spinCounterText;
    [Header("Images")]
    [Space]
    [SerializeField] private GameObject videoImage;

    private RewardWheelSpinner _rewardWheelSpinner;

    private void Start()
    {
        UpdateSpinCounterTextFontSize();
        videoImage.SetActive(false);
    }

    #region Zenject
    [Inject]
    private void Construct(RewardWheelSpinner rewardWheelSpinner)
    {
        _rewardWheelSpinner = rewardWheelSpinner;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if(_rewardWheelSpinner.CanSpin)
        {
            ShowAnimation_ButtonPressed();
            _rewardWheelSpinner.PressSpinButton();
        }
    }

    public void SetState_FreeSpinUsed()
    {
        buttonImage.sprite = spinUsedButtonSprite;
        spinCounterText.text = $"1/2";
        UpdateSpinCounterTextFontSize();
        videoImage.SetActive(true);
    }

    public void SetState_AllSpinsUsed()
    {
        buttonImage.sprite = spinUsedButtonSprite;
        spinCounterText.text = $"2/2";
        UpdateSpinCounterTextFontSize();
        videoImage.SetActive(true);
    }

    public void UpdateSpinCounterTextFontSize()
    {
        spinCounterText.fontSize = spinText.fontSize;
    }
}
