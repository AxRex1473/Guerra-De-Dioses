using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoldierBase : MonoBehaviour
{
    public States state = States.Idle;
    public enum States
    {
        Idle,
        Seeking,
        Attacking,
        Dead,
    }
    [HideInInspector] public AttackType attackType;
    public enum AttackType
    {
        Melee,
        Ranged
    }
    public GameObject target; //Momentaneo
    public UnityAction<SoldierBase> OnDealDamage, OnDie;

    [HideInInspector] public int health;
    [HideInInspector] public float velocity;
    [HideInInspector] public float attackRange;
    [HideInInspector] public float attackDamage;
    [HideInInspector] public float attackRatio; 
    [HideInInspector] public float detectRange;
    [HideInInspector] public float changeMind;
    [HideInInspector] public bool enemyNear;
    [HideInInspector] public bool inAttackRange;

    public virtual bool SetTarget(float detectRange)
    {
        if (target != null)
        {
            enemyNear = Vector3.Distance(transform.position, target.transform.position) < detectRange;
            return enemyNear;
        }
        else
            return false;
    }
    public bool TargetInRange(float attackRange)
    {
        if (target != null)
        {
            inAttackRange = Vector3.Distance(transform.position, target.transform.position) < attackRange;
            return inAttackRange;
        }
        else
            return false;
    }
    public virtual void Seek()
    {
        state = States.Seeking;
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
