using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SoldierBase : MonoBehaviour
{
    public States state = States.Idle;
    public enum States
    {
        Idle,
        Move,
        Seeking,
        Attacking,
        Dead,
    }
    public LayerMask targetLayer;
    [HideInInspector] public Vector3 groundPosition;
    [HideInInspector] public GameObject target; //Momentaneo
    [HideInInspector] public List<GameObject> targetsDetected = new List<GameObject>();
    [HideInInspector] public int health;
    [HideInInspector] public float velocity;
    [HideInInspector] public float attackRange;
    [HideInInspector] public float attackDamage;
    [HideInInspector] public float attackRatio; 
    [HideInInspector] public float detectRange;
    [HideInInspector] public float changeMind;
    [HideInInspector] public bool enemyNear;
    [HideInInspector] public bool inAttackRange;
    [HideInInspector] public float rotationSpeed;
    [HideInInspector] public float attackTimer;

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
    public void Set()
    {
        if (target == null)
        {
            SetTarget(detectRange);
        }
        else
        {
            LookAt(target.transform.position, rotationSpeed);
        }
    }
    public void Move(Vector3 direction, float desiredSpeed, NavMeshAgent agent)
    {
        Vector3 movement = direction * desiredSpeed * Time.deltaTime;
        agent.Move(movement);
    }
    public void ClickMove(Vector3 position, NavMeshAgent agent)
    {
        agent.autoBraking = true;
        agent.SetDestination(position);
    }
    public void LookAt(Vector3 targetPosition, float rotationSpeed)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
    public IEnumerator Updater(float detectRange, float attackRange)
    {
        while (true)
        {
            TargetInAttackRange(attackRange);
            TargetInRange(detectRange);
            yield return new WaitForSeconds(1);
        }
    }
    public virtual void Idle()
    {
        state = States.Idle;
    }
    public virtual void Seek()
    {
        state = States.Seeking;

    }
    public virtual void Move()
    {
        state = States.Move;
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
