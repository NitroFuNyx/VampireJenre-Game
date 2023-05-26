using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetsHolder : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayers;

    private List<Collider> enemies;

    public List<Collider> Enemies => enemies;
    private int targetCounter;
    private float timer;
    [Range(0,5)]
    [SerializeField]private float timerGoal;
    private bool WriteNewTargets()
    {
       var _enemyTargets = Physics.OverlapSphere(transform.position, 22, targetLayers);
       enemies=  _enemyTargets.ToList();
       if (enemies.Count == 0) return false;
       return true;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer>timerGoal)
        {
            WriteNewTargets();
            timer = 0;
        }
    }

    public void RemoveTarget(Collider target)
    {
        enemies.Remove(target);
    }

    public Collider GetTarget()
    {
            if (WriteNewTargets())
            {
                if (targetCounter >= enemies.Count)
                {
                    targetCounter = 0;

                }
            }
            else return null;
        var target = enemies[targetCounter];
        targetCounter++;
        
        return target;
    }
}
