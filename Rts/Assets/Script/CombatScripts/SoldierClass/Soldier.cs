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

    private void Start()
    {
        soldierState = GetComponent<StateMachine>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        Activate(stats);
    }
    private void Update()
    {
        Set();
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
        attackTimer = 0;
        agent.speed = velocity;
        agent.enabled = true;
        
        soldierState.PushState(Idle, OnIdleEnter, null);
        StartCoroutine(Updater(detectRange,attackRange));
    }
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void OnIdleEnter()
    {
        agent.ResetPath();
    }
    public override void Idle()
    {
        base.Idle();
        if (enemyNear && target != null)
        {
            soldierState.PushState(Seek, OnSeekEnter, OnSeekExit);
            animator.SetBool("IsIdle", false);
        }
    }

    private void OnSeekEnter()
    {
        agent.ResetPath();
        agent.speed = velocity;
        animator.SetBool("IsMoving", true);
    }

    public override void Seek()
    {
        base.Seek();
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= attackRange)
        {
            soldierState.PushState(Attack, null, null);
        }

        //else if (distanceToTarget > detectRange + 0.5f)
        //{
        //    soldierState.PopState();
        //    soldierState.PushState(Idle, OnIdleEnter, null);
        //}
        else
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            float desiredSpeed = Mathf.Clamp(distanceToTarget, 0, velocity);
            Move(direction, desiredSpeed, agent);
        }
    }

    private void OnSeekExit()
    {
        animator.SetBool("IsMoving", false);
    }

    private void OnMoveEnter()
    {
        agent.speed = velocity;
        animator.SetBool("IsMoving", true);
    }

    public override void Move()
    {
        base.Move();
        float distanceToTarget = Vector3.Distance(transform.position, groundPosition);
        if (enemyNear && target != null)
        {
            soldierState.PopState();
            soldierState.PushState(Seek, OnSeekEnter, OnSeekExit);
        }
        else if (distanceToTarget < 20)
        {
            agent.isStopped = true;
            agent.speed = 0;
            soldierState.PopState();
            soldierState.PushState(Idle, OnIdleEnter, null);
        }
        else
        {
            ClickMove(groundPosition, agent);
        }
    }

    private void OnMoveExit()
    {
        animator.SetBool("IsMoving", false);
    }

    public override void Attack()
    {
        base.Attack();
        agent.ResetPath();
        attackTimer -= Time.deltaTime;

        if (!inAttackRange)
        {
            soldierState.PopState();
            soldierState.PushState(Idle, OnIdleEnter, null);
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
                targetHealth.StartCoroutine(targetHealth.ReceiveDamage(attackDamage, 1));
            }
            else if (targetHealth != null && targetHealth.Health <= 0)
            {
                target = null;
                targetHealth = null;
            }
            attackTimer = attackRatio;
        }
    }

    public void OnMove()
    {
        soldierState.PushState(Move, OnMoveEnter, OnMoveExit);
    }
    public void OnSeek()
    {
        soldierState.PushState(Seek, OnSeekEnter, OnSeekExit);
    }
    public void OnDead()
    {
        soldierState.PushState(Die, null, null);
    }

    protected override void Die()
    {
        base.Die();
        agent.isStopped = true;
        animator.SetTrigger("IsDead");
        StopCoroutine(Updater(detectRange,attackRange));
        Destroy(gameObject, 5);
    }

}
