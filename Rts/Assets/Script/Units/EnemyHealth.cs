using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int saludMaxima = 100;
    public int saludActual;

    private Animator animator;

    private void Start()
    {
        saludActual = saludMaxima;
        animator = GetComponent<Animator>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AldeanoSoldado"))
        {
            InvokeRepeating("ReducirSalud", 1f, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AldeanoSoldado"))
        {
            CancelInvoke("ReducirSalud");
        }
    }

    private void ReducirSalud()
    {
        saludActual -= 10;

        if (saludActual <= 0)
        {
            animator.SetBool("IsDead", true);
            Invoke("DestruirAldeano", 5);
        }
    }

    private void DestruirAldeano()
    {
        Destroy(gameObject);
    }
}
