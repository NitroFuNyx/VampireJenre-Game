using UnityEngine;
using UnityEngine.UI;

public abstract class ProgressBar : MonoBehaviour
{
    [Header("Progress Bars")]
    [Space]
    [SerializeField] protected Image progressBarImage;

    private void Start()
    {
        SubscribeOnEvents();

        SetProgressBarStartValue();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    protected abstract void SubscribeOnEvents();

    protected abstract void UnsubscribeFromEvents();

    protected abstract void SetProgressBarStartValue();

    protected void ProgressBarValueChanged_ExecuteReaction(float currentAmount, float maxAmount)
    {
        progressBarImage.fillAmount = currentAmount.Remap(0, maxAmount, 0f, 1f);
    }
}
