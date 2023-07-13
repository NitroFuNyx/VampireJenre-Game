using UnityEngine;
using DG.Tweening;
using Zenject;

public class PlayDeathmatchButton : ButtonInteractionHandler
{
    [Header("Process Canceled Animation Data")]
    [Space]
    [SerializeField] private Vector3 scaleAnimationPunchVector = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private int scaleAnimationFrequency = 2;
    [SerializeField] private float scaleAnimationDuration = 0.5f;
    [SerializeField] private Color disabledColor;

    private DeathmatchAccessManager _deathmatchAccessManager;

    private bool buttonActivated = false;

    #region Zenject
    [Inject]
    private void Construct(DeathmatchAccessManager deathmatchAccessManager)
    {
        _deathmatchAccessManager = deathmatchAccessManager;
    }
    #endregion Zenject

    public override void ButtonActivated()
    {
        if(!buttonActivated)
        {
            buttonActivated = true;
            _deathmatchAccessManager.PlayDeathmatchButtonPressed_ExecuteReaction(ShowSuccessfullBuyingResult, ShowFailedBuyingResult);
        }
    }

    private void ShowFailedBuyingResult()
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

    private void ShowSuccessfullBuyingResult()
    {
        buttonActivated = false;
        ShowAnimation_ButtonPressed();
    }
}
