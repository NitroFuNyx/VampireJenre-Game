using UnityEngine;
using System;
using System.Collections;

public class MainLoaderUI : MainCanvasPanel
{
    [Header("Duration")]
    [Space]
    [SerializeField] private float loaderAnimationDuration = 1f;

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
        yield return new WaitForSeconds(loaderAnimationDuration);
        OnLoaderAnimationFinished?.Invoke();
    }
}
