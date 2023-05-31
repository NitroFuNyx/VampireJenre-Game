using UnityEngine;
using System;

public class PlayerVisionBorderDetector : MonoBehaviour
{
    public event Action OnObjectBecomeVisibleForPlayer;

    private BoxCollider boxCollider;

    private Collider player;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        player = FindObjectOfType<PlayerVisionBorder>().BoxCollider;
    }

    public void ObjectBecomeVisibleForPlayer()
    {
        OnObjectBecomeVisibleForPlayer?.Invoke();
    }

    public bool CheckIfVisibleForPlayer()
    {
        bool isVisible = false;

        if(boxCollider.bounds.Intersects(player.bounds))
        {
            Debug.Log($"{gameObject}");
            isVisible = true;
        }

        return isVisible;
    }
}
