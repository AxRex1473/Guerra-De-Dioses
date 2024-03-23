using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int saludMaxima = 100;
    public int saludActual;

    private void Start()
    {
        saludActual = saludMaxima;
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
            DestruirObjeto();
        }
    }

    private void DestruirObjeto()
    {
        Destroy(gameObject);
    }
}
