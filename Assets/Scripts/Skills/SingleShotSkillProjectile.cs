using System;
using UnityEngine;

public class SingleShotSkillProjectile : SkillParameterBase, ISkillProjectile
{
    [SerializeField] private Rigidbody projectileRigidBody;
    
    private void Start()
    {
        Move();
    }

    public void Move()
    {
        projectileRigidBody.AddForce(transform.forward * speed,ForceMode.Acceleration);
    }


    protected override void CollideWithMapObstacle()
    {
        throw new NotImplementedException();
    }
}
