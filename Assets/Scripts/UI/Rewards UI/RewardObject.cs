using UnityEngine;

public class RewardObject : MonoBehaviour
{
    [Header("Reward Data")]
    [Space]
    [SerializeField] private int rewardIndex;

    public int RewardIndex { get => rewardIndex; }
}
