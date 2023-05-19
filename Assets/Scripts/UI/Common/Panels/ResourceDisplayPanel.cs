using UnityEngine;
using TMPro;
using Zenject;

public abstract class ResourceDisplayPanel : MonoBehaviour
{
    [Header("Resource Texts")]
    [Space]
    [SerializeField] protected TextMeshProUGUI resourceAmountText;

    protected ResourcesManager _resourcesManager;

    private void Start()
    {
        SubscribeOnEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    #region Zenject
    [Inject]
    private void Construct(ResourcesManager resourcesManager)
    {
        _resourcesManager = resourcesManager;
    }
    #endregion Zenject

    protected abstract void SubscribeOnEvents();

    protected abstract void UnsubscribeFromEvents();

    protected void ResourcesManager_ResourceAmountChanged_ExecuteReaction(int amount)
    {
        resourceAmountText.text = $"{amount}";
    }
}
