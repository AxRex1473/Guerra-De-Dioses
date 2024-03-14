using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera myCam;
    NavMeshAgent myAgent;
    public LayerMask ground;
    private Animator animator;
    public float stoppingDistance = .3f; // Umbral para detener la animación de caminar

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
                myAgent.SetDestination(hit.point);
            }
        }

        // Verificar si el agente está en movimiento
        if (myAgent.velocity.magnitude > 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Verificar si el agente está cerca de su destino
        if (myAgent.remainingDistance <= stoppingDistance)
        {
            // Detener el agente
            myAgent.isStopped = true;
        }
        else
        {
            // Continuar el movimiento del agente
            myAgent.isStopped = false;
        }
    }
}
