using UnityEngine;
using System;
using System.Collections;

public class MainLoaderUI : MainCanvasPanel
{
    [Header("Duration")]
    [Space]
    [SerializeField] private float loaderAnimationDuration = 1f;
    [Header("Delays")]
    [Space]
    [SerializeField] private float loadEndDelay = 0.2f;

    #region Events Declaration
    public event Action<float, float> OnLoadProgressChanged;
    #endregion Events Declaration

    public void ShowLoading(Action OnLoaderAnimationFinished)
    {
        StartCoroutine(FinishLoaderCoroutine(OnLoaderAnimationFinished));
    }

    public override void PanelActivated_ExecuteReaction()
    {

    }

    public override void PanelDeactivated_ExecuteReaction()
    {

    }

    private IEnumerator FinishLoaderCoroutine(Action OnLoaderAnimationFinished)
    {
        float currentWaitTime = loaderAnimationDuration;

        while(currentWaitTime > 0f)
        {
            currentWaitTime -= Time.deltaTime;
            OnLoadProgressChanged?.Invoke(loaderAnimationDuration - currentWaitTime, loaderAnimationDuration);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(loadEndDelay);
        OnLoaderAnimationFinished?.Invoke();
    }
}
