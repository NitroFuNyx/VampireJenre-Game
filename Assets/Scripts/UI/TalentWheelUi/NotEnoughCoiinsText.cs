using UnityEngine;
using DG.Tweening;
using TMPro;
using Zenject;

public class NotEnoughCoiinsText : MonoBehaviour
{
    [Header("Durations")]
    [Space]
    [SerializeField] private float changeColorDuration = 1f;
    [Header("Colors")]
    [Space]
    [SerializeField] private Color disabledColor;
    [Header("Text Data")]
    [Space]
    [SerializeField] private bool coinsCostText = false;

    private TalentsManager _talentsManager;

    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        if(!coinsCostText)
        {
            textComponent.DOFade(0f, changeColorDuration);
        }
    }

    private void Start()
    {
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

    public void BuyingProcessCanceled_ExecuteReaction()
    {
        if (!coinsCostText)
        {
            textComponent.DOFade(1f, changeColorDuration).OnComplete(() =>
            {
                textComponent.DOFade(0f, changeColorDuration);
            });
        }
        else
        {
            textComponent.DOColor(disabledColor, changeColorDuration).OnComplete(() =>
            {
                textComponent.DOColor(Color.white, changeColorDuration);
            });
        }
    }
}
