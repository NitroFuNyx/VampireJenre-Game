using UnityEngine;

public class ExplosionCollision : MonoBehaviour
{
    public void Explode(float explosionColliderRadius)
    { 
    var colliders = Physics.OverlapSphere(transform.position, explosionColliderRadius);

    foreach (var VARIABLE in colliders)
    {
       if(VARIABLE.TryGetComponent(out EnemyCollisionsManager enemyCollider))
       {
           enemyCollider.SendMessage("ApplyExplosion");
       }
    }
    }

    
}
