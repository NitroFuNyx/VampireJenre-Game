using UnityEngine;
using DG.Tweening;

public class EnemyMovementManager : MonoBehaviour
{
    private PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void MoveToPlayer()
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.DOMove(playerPos, 10f);
    }
}
