using UnityEngine;
using System.Collections;
using Zenject;

public class UIDisolveHandler : MonoBehaviour
{
    [Header("Materials")]
    [Space]
    [SerializeField] private Material disolveMaterial;
    [Header("Disolve Data")]
    [Space]
    [SerializeField] private float fadeDelta = 0.006f;

    private GameProcessManager _gameProcessManager;

    private void Start()
    {
        ResetMaterial();

        _gameProcessManager.OnLevelDataReset += ResetMaterial;
    }

    private void OnDestroy()
    {
        _gameProcessManager.OnLevelDataReset -= ResetMaterial;
    }

    #region Zenject
    [Inject]
    private void Construct(GameProcessManager gameProcessManager)
    {
        _gameProcessManager = gameProcessManager;
    }
    #endregion Zenject

    public void DisolveImage(System.Action OnDisolveFinished)
    {
        StartCoroutine(DisolveCoroutine(OnDisolveFinished));
    }

    private void ResetMaterial()
    {
        disolveMaterial.SetFloat(DisolveMaterialReferences.FadeValue, 1f);
    }

    private IEnumerator DisolveCoroutine(System.Action OnDisolveFinished)
    {
        while(disolveMaterial.GetFloat(DisolveMaterialReferences.FadeValue) > 0f)
        {
            float currentDisolveValue = disolveMaterial.GetFloat(DisolveMaterialReferences.FadeValue);
            disolveMaterial.SetFloat(DisolveMaterialReferences.FadeValue, currentDisolveValue - fadeDelta);
            yield return new WaitForEndOfFrame();
        }

        OnDisolveFinished?.Invoke();
    }
}
