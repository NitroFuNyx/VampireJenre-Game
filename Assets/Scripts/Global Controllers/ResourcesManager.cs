using UnityEngine;
using System;

public class ResourcesManager : MonoBehaviour
{
    [Header("Resources Data")]
    [Space]
    [SerializeField] private int coinsAmount;
    [SerializeField] private int gemsAmount;

    private int coinsSurplus = 5;

    #region Events Declaration
    public event Action<int> OnCoinsAmountChanged;
    public event Action<int> OnGemsAmountChanged;
    #endregion Events Declaration

    public void IncreaseCoinsAmount()
    {
        int randomIndex = UnityEngine.Random.Range(0, 5);
        if(randomIndex == 0)
        {
            coinsAmount += coinsSurplus;
            OnCoinsAmountChanged?.Invoke(coinsAmount);
        }
    }

    public void DecreaseCoinsAmount(int deltaAmount)
    {
        coinsAmount -= deltaAmount;

        if(coinsAmount < 0)
        {
            coinsAmount = 0;
        }

        OnCoinsAmountChanged?.Invoke(coinsAmount);
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
