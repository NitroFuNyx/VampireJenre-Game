using UnityEngine;
using System.Collections;
using DG.Tweening;

public class EnemyMovementManager : MonoBehaviour
{
    [Header("Move Data")]
    [Space]
    [SerializeField] private float moveVelocity = 10f;
    [SerializeField] private float rotationVelocity = 10f;

    private PlayerController player;

    private bool canMove = false;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (canMove)
        {
            Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPos, moveVelocity * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), rotationVelocity * Time.deltaTime);
        }
    }

    public void MoveToPlayer()
    {
        canMove = true;
        //Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        //transform.DOMove(playerPos, 10f);
    }
}
