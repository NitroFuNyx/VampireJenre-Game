using UnityEngine;

public class PlayerExperienceManager : MonoBehaviour
{
    [Header("Experience Data")]
    [Space]
    [SerializeField] private int currentXp = 0;
    [SerializeField] private int upgradeXpValue = 100;

    public void IncreaseXpValue(int deltaXp)
    {
        currentXp += deltaXp;

        if(currentXp >= upgradeXpValue)
        {
            int newLevelStartXp = currentXp - upgradeXpValue;
            currentXp = newLevelStartXp;
            upgradeXpValue = 100; // reset upgradeXpValue
        }
    }
}
