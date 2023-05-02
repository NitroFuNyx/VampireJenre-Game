
using System;
using UnityEngine;

public class MeteorSkillProjectile : SkillParameterBase , ISkillProjectile
{
    [SerializeField] private Rigidbody projectileRigidBody;
    private void Start()
    {
        throw new NotImplementedException();
    }

    public void Move()
    {
        projectileRigidBody.AddForce(transform.forward * speed,ForceMode.Acceleration);
    }
}
