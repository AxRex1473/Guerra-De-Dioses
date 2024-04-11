using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Soldier : SoldierBase
{
    [SerializeField] SoldierStats stats; //PORHACER: Analizar que tan limpio es esto
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Activate(SoldierStats sData)
    {
        navMeshAgent.enabled = true;
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
    protected override void Die()
    {
        base.Die();
        navMeshAgent.enabled = false;
        animator.SetTrigger("Murió"); //Temporal hasta que sepa como se llama el estado de morir

    }
}
