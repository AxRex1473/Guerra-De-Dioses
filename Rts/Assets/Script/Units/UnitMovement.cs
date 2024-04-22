using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    NavMeshAgent myAgent;
    public LayerMask ground;
    private Animator animator;
    public float stoppingDistance = 1f;
    public float separationDistance = 2f; // Distancia mínima entre unidades

    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                Vector3 destination = hit.point;
                foreach (var unit in FindObjectsOfType<UnitMovement>())
                {
                    if (unit != this && Vector3.Distance(unit.transform.position, destination) < separationDistance)
                    {
                        destination = unit.transform.position - (destination - unit.transform.position).normalized * separationDistance;
                    }
                }

                NavMeshHit navHit;
                if (NavMesh.SamplePosition(destination, out navHit, 5f, NavMesh.AllAreas))
                {
                    myAgent.SetDestination(navHit.position);
                }
            }
        }

        if (myAgent.velocity.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (myAgent.remainingDistance <= stoppingDistance)
        {
            // Detener el agente
            myAgent.isStopped = true;
        }
        else
        {
            myAgent.isStopped = false;
        }
    }
}
