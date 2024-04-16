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
        agent.speed = velocity;
        agent.enabled = true;
        soldierState.PushState(Idle, OnIdleEnter, OnIdleExit);
    }
    private void Update()
    {
        enemyNear = Vector3.Distance(transform.position, target.transform.position) < 40;
        inAttackRange = Vector3.Distance(transform.position, target.transform.position) < 1;
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
    }
    private void OnIdleEnter()
    {
        agent.ResetPath();
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

    }
    private void OnSeekEnter()
    {

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
        if(Vector3.Distance(transform.position,target.transform.position) > 40.5f)
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

    }
    private void OnAttackEnter()
    {

    }
    public override void Attack()
    {
        //base.Attack();
        //Debug.Log("Esta atacando");
        //agent.isStopped = true;     //Reestructurar este metodo
        //animator.SetBool("IsMoving", false);
        //animator.SetTrigger("Attack");
        //transform.forward = (target.transform.position - transform.position).normalized;
    }
    private void OnAttackExit()
    {

    }
    public override void Stop()
    {
        base.Stop();
        agent.isStopped = true;
        animator.SetBool("Caminando", false);  //Temporal hasta que sepa como se llama el estado de caminar
    }

    protected override void Die()
    {
        base.Die();
        agent.enabled = false;
        animator.SetTrigger("Murió"); //Temporal hasta que sepa como se llama el estado de morir
    }
}
