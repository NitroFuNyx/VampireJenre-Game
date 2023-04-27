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

    public void MoveToPlayer()
    {
        canMove = true;
        //Vector3 playerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        //transform.DOMove(playerPos, 10f);
        StartCoroutine(MoveToPlayerCoroutine());
    }

    private IEnumerator MoveToPlayerCoroutine()
    {
        if (canMove)
        {
            Vector3 startPos = transform.position;
            Vector3 originalPlayerPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            float travelPercent = 0f;

            while (Vector3.Distance(transform.position, originalPlayerPos) > 0.1f)
            {
                Vector3 targetPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

                travelPercent += moveVelocity * Time.deltaTime;
                transform.position = Vector3.Lerp(startPos, targetPos, travelPercent);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), rotationVelocity * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
