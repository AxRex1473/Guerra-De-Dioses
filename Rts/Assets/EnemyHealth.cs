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
            InvokeRepeating("ReducirSalud", 1f, 1f); // Reduce la salud cada segundo
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AldeanoSoldado"))
        {
            CancelInvoke("ReducirSalud"); // Detiene la reducción de salud cuando el aldeano sale del trigger
        }
    }

    private void ReducirSalud()
    {
        saludActual -= 10; // Reducción de salud cada segundo, puedes ajustar este valor según sea necesario

        if (saludActual <= 0)
        {
            DestruirObjeto();
        }
    }

    private void DestruirObjeto()
    {
        // Destruye el objeto cuando su salud llega a 0
        Destroy(gameObject);
    }
}
