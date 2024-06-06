using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textUpdate : MonoBehaviour
{
    public TextMeshProUGUI texto; // Referencia al TextMeshProUGUI
    public float intervaloActualizacion = 5f; // Intervalo en segundos
    private int valor = 0; // Valor incremental

    private void Start()
    {
        if (texto != null)
        {
            StartCoroutine(ActualizarTextoPeriodicamente());
        }
        else
        {
            Debug.LogError("TextoActualizador: No se ha asignado el TextMeshProUGUI.");
        }
    }

    private IEnumerator ActualizarTextoPeriodicamente()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloActualizacion);
            valor += 4; // Incrementar el valor
            texto.text = "" + valor; // Actualizar el texto
        }
    }
}
