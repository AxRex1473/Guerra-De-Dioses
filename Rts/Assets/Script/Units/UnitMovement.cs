using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    NavMeshAgent myAgent;
    public LayerMask ground;
    private Animator animator;
    public float stoppingDistance = 1f;
    public float separationDistance = 5f; // Distancia mínima entre unidades
    List<UnitMovement> allUnits = new List<UnitMovement>();

    void Start()
    {
        myCam = Camera.main;
        myAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Obtener referencias a todas las unidades en el inicio
        allUnits.AddRange(FindObjectsOfType<UnitMovement>());
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
                Vector3 averageUnitPosition = Vector3.zero;

                // Calcular la posición promedio de todas las unidades cercanas
                foreach (var unit in allUnits)
                {
                    if (unit != this)
                    {
                        averageUnitPosition += unit.transform.position;
                    }
                }
                averageUnitPosition /= allUnits.Count - 1;

                // Si la unidad está lo suficientemente cerca de otras, se mueve hacia una posición alrededor de esas unidades
                if (Vector3.Distance(transform.position, averageUnitPosition) < separationDistance)
                {
                    destination = averageUnitPosition + (destination - averageUnitPosition).normalized * separationDistance;
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
