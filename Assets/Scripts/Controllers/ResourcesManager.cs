using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    [Header("Resources Data")]
    [Space]
    [SerializeField] private int coinsAmount;
    [SerializeField] private int gemsAmount;

    public void IncreaseCoinsAmount(int deltaAmount)
    {
        coinsAmount += deltaAmount;
    }

    public void DecreaseCoinsAmount(int deltaAmount)
    {
        coinsAmount -= deltaAmount;

        if(coinsAmount < 0)
        {
            coinsAmount = 0;
        }
    }

    public void IncreaseGemsAmount(int deltaAmount)
    {
        gemsAmount += deltaAmount;
    }

    public void DecreaseGemsAmount(int deltaAmount)
    {
        gemsAmount -= deltaAmount;

        if(gemsAmount < 0)
        {
            gemsAmount = 0;
        }
    }
}
