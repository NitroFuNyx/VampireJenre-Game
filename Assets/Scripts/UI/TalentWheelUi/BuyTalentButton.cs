using UnityEngine;
using DG.Tweening;
using Zenject;

public class BuyTalentButton : ButtonInteractionHandler
{
    [Header("Process Canceled Animation Data")]
    [Space]
    [SerializeField] private Vector3 scaleAnimationPunchVector = new Vector3(1.2f, 1.2f, 1.2f);
    [SerializeField] private int scaleAnimationFrequency = 2;
    [SerializeField] private float scaleAnimationDuration = 1f;

    private TalentsManager _talentsManager;

    [SerializeField] private bool buttonActivated = false;

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
            _talentsManager.BuyTalent(BuyingProcessLaunced_ExecuteReaction, ResetButton, BuyingProcessCanceled_ExecuteReaction);
        }
    }

    public void ResetButton()
    {
        buttonActivated = false;
    }

    public void BuyingProcessCanceled_ExecuteReaction()
    {
        buttonImage.DOColor(Color.red, scaleAnimationDuration / 2).OnComplete(() =>
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
}
