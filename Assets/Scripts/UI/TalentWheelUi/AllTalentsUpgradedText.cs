using UnityEngine;
using DG.Tweening;
using TMPro;
using Zenject;

public class AllTalentsUpgradedText : MonoBehaviour
{
    [Header("Durations")]
    [Space]
    [SerializeField] private float changeAlphaDuration = 1f;

    private TalentsManager _talentsManager;

    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.DOFade(0f, changeAlphaDuration);
    }

    private void Start()
    {
        //_talentsManager.OnTalentBuyingProcessCanceled += BuyingProcessCanceled_ExecuteReaction;
    }

    private void OnDestroy()
    {
        //_talentsManager.OnTalentBuyingProcessCanceled -= BuyingProcessCanceled_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(TalentsManager talentsManager)
    {
        _talentsManager = talentsManager;
    }
    #endregion Zenject

    public void AllTalentsUpgraded_ExecuteReaction()
    {
        textComponent.DOFade(1f, changeAlphaDuration);
    }
}
