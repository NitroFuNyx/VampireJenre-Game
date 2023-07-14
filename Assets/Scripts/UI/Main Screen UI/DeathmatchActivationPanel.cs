using UnityEngine;
using TMPro;
using Zenject;

public class DeathmatchActivationPanel : MonoBehaviour
{
    [Header("Internal Referenses")]
    [Space]
    [SerializeField] private TextMeshProUGUI deathmatchUsedText;
    [SerializeField] private TextMeshProUGUI deathmatchcostText;
    [SerializeField] private CanvasGroup costPanel;

    private DeathmatchAccessManager _deathmatchAccessManager;

    private void Start()
    {
        costPanel.alpha = 1f;
        deathmatchUsedText.gameObject.SetActive(false);
        deathmatchcostText.text = $"{_deathmatchAccessManager.DeatmatchPurchaseCost}";

        _deathmatchAccessManager.OnDeathmatchUiUpdateRequired += DeathmatchUiUpdateRequired_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _deathmatchAccessManager.OnDeathmatchUiUpdateRequired -= DeathmatchUiUpdateRequired_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(DeathmatchAccessManager deathmatchAccessManager)
    {
        _deathmatchAccessManager = deathmatchAccessManager;
    }
    #endregion Zenject

    private void DeathmatchUiUpdateRequired_ExecuteReaction()
    {
        costPanel.alpha = 0f;
        deathmatchUsedText.gameObject.SetActive(true);
    }
}
