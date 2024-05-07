using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SoldierBase : MonoBehaviour
{
    public States state = States.Idle;
    public enum States
    {
        Idle,
        Moving,
        Seeking,
        Attacking,
        Dead,
    }
    [HideInInspector] public SoldierTeam soldierTeam;
    public enum SoldierTeam
    {
        AlliedTeam,
        EnemyTeam
    }
    public Vector3 groundPosition;
    public GameObject target; //Momentaneo
    public List<GameObject> targetsDetected = new List<GameObject>();
    public LayerMask targetLayer;
    public UnityAction<SoldierBase> OnDealDamage, OnDie; //Analizar si se va a usar

    [HideInInspector] public int health;
    [HideInInspector] public float velocity;
    [HideInInspector] public float attackRange;
    [HideInInspector] public float attackDamage;
    [HideInInspector] public float attackRatio; 
    [HideInInspector] public float detectRange;
    [HideInInspector] public float changeMind;
    [HideInInspector] public bool enemyNear;
    [HideInInspector] public bool inAttackRange;

    public virtual bool TargetInRange(float detectRange)
    {
        if (target != null)
        {
            enemyNear = Vector3.Distance(transform.position, target.transform.position) < detectRange;
            return enemyNear;
        }
        else
            return enemyNear = false;
    }
    public bool TargetInAttackRange(float attackRange)
    {
        if (target != null)
        {
            inAttackRange = Vector3.Distance(transform.position, target.transform.position) < attackRange;
            return inAttackRange;
        }
        else
            return inAttackRange = false;
    }
    public void SetTarget(float detectRadio)
    {
        targetsDetected.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadio, targetLayer);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject) 
            {
                targetsDetected.Add(collider.gameObject);
            }
        }
        if (targetsDetected.Count > 0) 
        {
            if (target == null || !targetsDetected.Contains(target))
            {
                target = targetsDetected[0];
            }
        }
        else
        {
            target = null; 
        }
    }
    public virtual void Move(Vector3 direction, float desiredSpeed, NavMeshAgent agent)
    {
        Vector3 movement = direction * desiredSpeed * Time.deltaTime;
        agent.Move(movement);
    }
    public virtual void Idle()
    {
        state = States.Idle;
    }
    public virtual void Seek()
    {
        state = States.Seeking;

    }
    public virtual void Moving()
    {
        state = States.Moving;
    }
    public virtual void Attack()
    {
        state = States.Attacking; 
    }
    public virtual void Stop()
    {
        state = States.Idle;
    }
    protected virtual void Die()
    {
        state = States.Dead;
    }
}
