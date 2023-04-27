using UnityEngine;
using DG.Tweening;

public class PanelActivationManager : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public CanvasGroup _CanvasGroup { get => canvasGroup; private set => canvasGroup = value; }

    private void Awake()
    {
        if(TryGetComponent(out CanvasGroup canvas))
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        else
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void ShowPanel()
    {
        SetCanvasActivationState(true);
    }

    public void HidePanel()
    {
        SetCanvasActivationState(false);
    }

    public void HidePanelSlowly(float duration)
    {
        canvasGroup.DOFade(0f, duration);
    }

    public void SetCanvasActivationState(bool isActive)
    {
        if(canvasGroup)
        {
            if (isActive)
            {
                canvasGroup.alpha = 1f;
            }
            else
            {
                canvasGroup.alpha = 0f;
            }

            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }
        else
        {
            Debug.LogError($"There is no canvas group component attached to {gameObject}", gameObject);
        }
    }
}
