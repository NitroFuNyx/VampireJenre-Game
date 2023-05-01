using System;
using UnityEngine;


public abstract class SkillParameterBase : MonoBehaviour
{
    [SerializeField]private protected float speed;
    [SerializeField]private protected int projectileCount;
    
    [SerializeField]private protected Skills skillType;

    public int ProjectileCount => projectileCount;

    
}
