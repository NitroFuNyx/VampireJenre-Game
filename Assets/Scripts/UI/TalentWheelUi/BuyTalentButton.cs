using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

public class BuyTalentButton : ButtonInteractionHandler
{
    [Header("Process Canceled Animation Data")]
    [Space]
    [SerializeField] private Vector3 scaleAnimationPunchVector = new Vector3(1.2f, 1.2f, 1.2f);
    [SerializeField] private int scaleAnimationFrequency = 2;
    [SerializeField] private float scaleAnimationDuration = 1f;
    [Header("Lock Data")]
    [Space]
    [SerializeField] private Image lockImage;
    [Header("Durations")]
    [Space]
    [SerializeField] private float changeAlphaDuration = 0.05f;
    [Header("Colors")]
    [Space]
    [SerializeField] private Color disabledColor;

    private TalentsManager _talentsManager;

    private bool buttonActivated = false;

    private void Start()
    {
        lockImage.DOFade(0f, changeAlphaDuration);

        _talentsManager.OnTalentBuyingProcessCanceled += BuyingProcessCanceled_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _talentsManager.OnTalentBuyingProcessCanceled -= BuyingProcessCanceled_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(TalentsManager talentsManager)
    {
        _talentsManager = talentsManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if(!buttonActivated)
        {
            buttonActivated = true;
            _talentsManager.BuyTalent(BuyingProcessLaunced_ExecuteReaction, ResetButton);
        }
    }

    public void ResetButton()
    {
        buttonActivated = false;
    }

    public void BuyingProcessCanceled_ExecuteReaction()
    {
        buttonImage.DOColor(disabledColor, scaleAnimationDuration / 2).OnComplete(() =>
        {
            buttonImage.DOColor(Color.white, scaleAnimationDuration / 2);
        });
        buttonImage.transform.DOPunchScale(scaleAnimationPunchVector, scaleAnimationDuration, scaleAnimationFrequency).OnComplete(() =>
        {
            buttonActivated = false;
        });
    }

    public void BuyingProcessLaunced_ExecuteReaction()
    {
        ShowAnimation_ButtonPressed();
    }

    private void AllTalentsUpgraded_ExecuteReaction()
    {
        ButtonComponent.interactable = false;
        lockImage.DOFade(1f, changeAlphaDuration);
    }
}
