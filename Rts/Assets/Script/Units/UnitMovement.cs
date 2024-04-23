using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    NavMeshAgent myAgent;
    public LayerMask ground;
    private Animator animator;
    public float stoppingDistance = 10f;
    public float separationDistance = 10f; // Distancia mínima entre unidades

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
                Vector3 closestUnitPosition = Vector3.zero;
                float closestDistance = float.MaxValue;

                // Encuentra la posición de la unidad más cercana
                foreach (var unit in FindObjectsOfType<UnitMovement>())
                {
                    if (unit != this)
                    {
                        float distance = Vector3.Distance(unit.transform.position, destination);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestUnitPosition = unit.transform.position;
                        }
                    }
                }

                // Si la unidad está lo suficientemente cerca de otra, se mueve hacia una posición alrededor de esa unidad
                if (closestDistance < separationDistance)
                {
                    destination = closestUnitPosition + (destination - closestUnitPosition).normalized * separationDistance;
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
