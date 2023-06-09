using UnityEngine;
using TMPro;
using Zenject;

public class PlayerLevelText : MonoBehaviour
{
    private PlayerExperienceManager _playerExperienceManager;

    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _playerExperienceManager.OnPlayerLevelDataUpdated += PlayerExperienceManager_OnPlayerLevelDataUpdated_ExecuteReaction;
    }

    private void OnDestroy()
    {
        _playerExperienceManager.OnPlayerLevelDataUpdated -= PlayerExperienceManager_OnPlayerLevelDataUpdated_ExecuteReaction;
    }

    #region Zenject
    [Inject]
    private void Construct(PlayerExperienceManager playerExperienceManager)
    {
        _playerExperienceManager = playerExperienceManager;
    }
    #endregion Zenject

    private void PlayerExperienceManager_OnPlayerLevelDataUpdated_ExecuteReaction(int currentLevel)
    {
        textComponent.text = $"{currentLevel}";
    }
}
