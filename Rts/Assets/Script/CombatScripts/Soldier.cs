using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Soldier : SoldierBase
{
    [SerializeField] SoldierStats stats; //PORHACER: Analizar que tan limpio es esto
    [SerializeField] StateMachine soldierState;
    private Animator animator;
    private NavMeshAgent agent;

    public float radius;
    public LayerMask targetLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        agent.speed = velocity;
        agent.enabled = true;
        soldierState.PushState(Idle, OnIdleEnter, OnIdleExit);
    }
    private void Update()
    {
        SetTarget(detectRange);
        TargetInRange(attackRange);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    public List<Transform> DetectUnits()
    {
        List<Transform> detectedUnits = new List<Transform>();

        // Encuentra todas las unidades dentro del radio
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, targetLayer);
        foreach (Collider col in colliders)
        {
            detectedUnits.Add(col.transform);
        }

        return detectedUnits;
    } //PORHACER: hacer que detecte todas las unidades dentro de un rango y que ataque a la unidad que se encuentra en la cima de la lista
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
        float attackTimer = 0;
        attackTimer -= Time.deltaTime;
        if (!inAttackRange)
        {
            soldierState.PopState();
        }
        else if (attackTimer <= 0)
        {
            Debug.Log("esta atacando");
            //animator.SetTrigger("IsAttacking"); Incorporar una vez teniendo las animaciones
            //target.funcionParaRecibirDaño(attackDamage);
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
    private void OnDieEnter()
    {
        //animator.SetTrigger("Murió"); Incorporar una vez teniendo las animaciones
        agent.isStopped = true;
    }
    protected override void Die()
    {
        base.Die();
        agent.enabled = false;
    }
    private void OnDieExit()
    {
        Destroy(this,5);//esto se puede cambiar si se implementa un objectPool 
    }
}
