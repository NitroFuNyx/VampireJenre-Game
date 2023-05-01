using UnityEngine;

public class MissilesSkillProjectile : SkillParameterBase, ISkillProjectile
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

    public void SetObjectDirection()
    {
        throw new System.NotImplementedException();
    }
}
