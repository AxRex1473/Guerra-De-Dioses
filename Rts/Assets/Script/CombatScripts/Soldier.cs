using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class Soldier : SoldierBase
{
    [SerializeField] SoldierStats stats; 
    [SerializeField] StateMachine soldierState;
    [SerializeField] SoldierHealth targetHealth;
    private Animator animator;
    private NavMeshAgent agent;
    private float attackTimer = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        targetHealth.OnHurt += OnDead; //PORHACER buscar otro metodo para manejar la vida, manejarlo asi causa que todos los soldados mueran al mismo tiempo 
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
        agent.speed = velocity;
        agent.enabled = true;
        soldierState.PushState(Idle, OnIdleEnter, OnIdleExit);
    }
    private void OnDead()
    {
        soldierState.PushState(Die, OnDieEnter, OnDieExit);
    }
    private void Update()
    {
        SetTarget(detectRange);
        TargetInRange(attackRange);
    }
    private void OnDrawGizmosSelected() //Solo para debug
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
    private void Idle()
    {
        changeMind -= Time.deltaTime;
        Debug.Log("Esta idle");

        if (enemyNear)
        {
            soldierState.PushState(Seek,OnSeekEnter,OnSeekExit);
        }
        else if (changeMind <= 0)
        {

        }
    }
    private void OnIdleExit()
    {
        //animator.SetBool("IsIdle", false); Incorporar una vez teniendo las animaciones
    }
    private void OnSeekEnter()
    {
        //animator.SetBool("IsMoving", true); Incorporar una vez teniendo las animaciones
    }
    public override void Seek()
    {
        if(target == null)
        {
            return;
        }
        base.Seek();
        Debug.Log("Esta buscando");
        agent.SetDestination(target.transform.position);
        if(Vector3.Distance(transform.position,target.transform.position) > detectRange +.5f)
        {
            soldierState.PopState();
            soldierState.PushState(Idle, OnIdleEnter, OnIdleExit);
        }
        if (inAttackRange)
        {
            soldierState.PushState(Attack, OnAttackEnter, OnAttackExit);
        }
    }
    private void OnSeekExit()
    {
        //animator.SetBool("IsMoving", false); Incorporar una vez teniendo las animaciones
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
        }
        else if (attackTimer <= 0)
        {
            Debug.Log("esta atacando");
            //animator.SetTrigger("IsAttacking"); Incorporar una vez teniendo las animaciones
            DealDamage();
            attackTimer = attackRatio;
        }
    }
    private void OnAttackExit()
    {

    }
    public override void Stop()
    {
        base.Stop();
        agent.isStopped = true;
    }
    public void DealDamage()
    {
        targetHealth = target.GetComponent<SoldierHealth>();

        if (targetHealth != null)
        {
            targetHealth.ReceiveDamage(attackDamage);
        }
        else if (targetHealth = null)
        {
            soldierState.PopState();
        }
    }
    private void OnDieEnter()
    {
        //animator.SetTrigger("Murió"); Incorporar una vez teniendo las animaciones
        agent.isStopped = true;
    }
    protected override void Die()
    {
        base.Die();
    }
    private void OnDieExit()
    {
        Destroy(this.gameObject,5);//esto se puede cambiar si se implementa un objectPool 
    }
}
