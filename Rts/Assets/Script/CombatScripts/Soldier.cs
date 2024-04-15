using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Soldier : SoldierBase
{
    [SerializeField] SoldierStats stats; //PORHACER: Analizar que tan limpio es esto
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    public float radius;
    public LayerMask targetLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Activate(stats);
    }

    public void Activate(SoldierStats sData) //Metodo para llamarlo desde un manager, sin esto no se mueve padre santo
    {
        health = sData.health;
        velocity = sData.velocity;
        attackRange = sData.attackRange;
        attackDamage = sData.attackDamage;
        attackRatio = sData.attackRatio;
        navMeshAgent.speed = velocity;
        state = States.Idle;
        navMeshAgent.enabled = true;
    }
    private void Update()
    {
        Debug.Log(state);
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
    public override void Seek()
    {
        if(target == null)
        {
            return;
        }
        base.Seek();
        navMeshAgent.SetDestination(target.transform.position);
        navMeshAgent.isStopped = false;
        animator.SetBool("Caminando", true);  //Temporal hasta que sepa como se llama el estado de caminar
    }
    public override void StartAttack()
    {
        base.StartAttack();
        navMeshAgent.isStopped = true;
        animator.SetBool("IsMoving", false);
        animator.SetTrigger("Attack");
        transform.forward = (target.transform.position - transform.position).normalized;
    }
    public override void Stop()
    {
        base.Stop();
        navMeshAgent.isStopped = true;
        animator.SetBool("Caminando", false);  //Temporal hasta que sepa como se llama el estado de caminar

    }

    protected override void Die()
    {
        base.Die();
        navMeshAgent.enabled = false;
        animator.SetTrigger("Murió"); //Temporal hasta que sepa como se llama el estado de morir

    }
}
