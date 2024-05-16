using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public class Soldier : SoldierBase
{
    [SerializeField] SoldierStats stats;
    private StateMachine soldierState;
    private Animator animator;
    private NavMeshAgent agent;
    private float attackTimer = 0;
    public bool canMove = false; //Variable para mover al soldado sin que haya detectado a un enemigo
    public bool groundTarget = false; //Variable para diferenciar si el objetivo es un soldado o marca en el piso
    
    private void Start()
    {
        soldierState = GetComponent<StateMachine>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Activate(stats);
    }

    public void Activate(SoldierStats sData) //Metodo para llamarlo desde un manager, sin esto no se mueve padre santo
    {
        health = sData.health;
        velocity = sData.velocity;
        attackRange = sData.attackRange;
        attackDamage = sData.attackDamage;
        attackRatio = sData.attackRatio;
        enemyNear = sData.enemyNear;
        inAttackRange = sData.inAttackRange;
        changeMind = sData.changeMind;
        detectRange = sData.detectRange;
        rotationSpeed = 10;
        agent.speed = velocity;
        agent.enabled = true;
        soldierState.PushState(Idle, OnIdleEnter, OnIdleExit);
    }
    public void OnDead()
    {
        soldierState.PushState(Die, OnDieEnter, null);
    }
    private void Update()
    {
        TargetInRange(detectRange);
        TargetInAttackRange(attackRange);
        Set();
    }
    private void OnDrawGizmosSelected() //Solo para debug, se puede borrar sin problemas
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnIdleEnter()
    {
        agent.ResetPath();
        //animator.SetBool("IsIdle", true); Incorporar una vez teniendo las animaciones
    }
    public override void Idle()
    {
        base.Idle();
        if (enemyNear || canMove && target != null)
        {
            soldierState.PushState(Seek, OnSeekEnter, OnSeekExit);
        }
    }
    private void OnIdleExit()
    {
        animator.SetBool("IsIdle", false); 
    }

    private void OnSeekEnter()
    {
        animator.SetBool("IsMoving", true); 
    }
    public override void Seek()
    {
        base.Seek();
        Vector3 direction = (target.transform.position - transform.position).normalized;    
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= attackRange && !groundTarget)
        {
            soldierState.PushState(Attack, OnAttackEnter, null);
            canMove = false;
        }

        else if (distanceToTarget > detectRange + 0.5f && !canMove)
        {
            soldierState.PopState();
            soldierState.PushState(Idle, OnIdleEnter, OnIdleExit);
        }
        else
        {
            float desiredSpeed = Mathf.Clamp(distanceToTarget, 0, velocity);
            Move(direction, desiredSpeed, agent);
            if (distanceToTarget <= attackRange && groundTarget)
            {
                groundTarget = false;
                canMove = false;
                target = null;
                soldierState.PopState();
                soldierState.PushState(Idle, OnIdleEnter, OnIdleExit);
            }
        }
    }
    private void OnSeekExit()
    {
        animator.SetBool("IsMoving", false); 
    }

    private void OnAttackEnter()
    {
        agent.ResetPath();
    }
    public override void Attack()
    {
        base.Attack();
        attackTimer -= Time.deltaTime;

        if (!inAttackRange)
        {
            soldierState.PopState();
            soldierState.PushState(Idle, OnIdleEnter, OnIdleExit);
        }
        else if (inAttackRange && attackTimer <= 0)
        {
            if (target == null)
            {
                return;
            }
            SoldierHealth targetHealth = target.GetComponent<SoldierHealth>();
            if (targetHealth != null && targetHealth.gameObject != null && targetHealth.Health > 0)
            {
                animator.SetTrigger("IsAttacking"); 
                targetHealth.StartCoroutine(targetHealth.ReceiveDamage(attackDamage,1));
            }
            else if (targetHealth != null && targetHealth.Health <= 0) 
            {
                target = null;
                targetHealth = null;
            }
            attackTimer = attackRatio;
        }
    }
    private void OnDieEnter()
    {
        animator.SetTrigger("IsDead"); 
        agent.isStopped = true;
    }
    protected override void Die()
    {
        base.Die();
        Destroy(gameObject, 5);
        //StartCoroutine(OffSelf());
    }

    IEnumerator OffSelf()
    {
        target = null;
        targetsDetected.Clear();
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
