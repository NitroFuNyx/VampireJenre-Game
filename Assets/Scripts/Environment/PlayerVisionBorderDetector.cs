using UnityEngine;
using System;

public class PlayerVisionBorderDetector : MonoBehaviour
{
    public event Action OnObjectBecomeVisibleForPlayer;
    public event Action OnObjectStoppedBeingVisibleForPlayer;

    private BoxCollider boxCollider;

    private Collider player;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        player = FindObjectOfType<PlayerVisionBorder>().BoxCollider;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == Layers.PlayerVisionBorder)
        {
            OnObjectBecomeVisibleForPlayer?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == Layers.PlayerVisionBorder)
        {
            OnObjectStoppedBeingVisibleForPlayer?.Invoke();
        }
    }

    //public void ObjectBecomeVisibleForPlayer()
    //{
    //    OnObjectBecomeVisibleForPlayer?.Invoke();
    //}

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
