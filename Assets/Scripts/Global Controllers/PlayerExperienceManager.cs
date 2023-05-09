using UnityEngine;
using System;

public class PlayerExperienceManager : MonoBehaviour
{
    [Header("Experience Data")]
    [Space]
    [SerializeField] private float currentXp = 0;
    [SerializeField] private float upgradeXpValue = 100;

    #region Events Declaration
    public event Action<float, float> OnPlayerXpAmountChanged;
    #endregion Events Declaration

    public void IncreaseXpValue(float deltaXp)
    {
        currentXp += deltaXp;

        if(currentXp >= upgradeXpValue)
        {
            float newLevelStartXp = currentXp - upgradeXpValue;
            currentXp = newLevelStartXp;
            upgradeXpValue = 100; // reset upgradeXpValue
        }

        OnPlayerXpAmountChanged?.Invoke(currentXp, upgradeXpValue);
    }
}
