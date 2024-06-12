using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GroundIcon : MonoBehaviour
{
    private Coroutine deactivateCoroutine;

    // Métodopara activar el ícono y reiniciar el temporizador
    public void ActivateAndResetTimer()
    {
        gameObject.SetActive(true); 

        if (deactivateCoroutine != null)
        {
            StopCoroutine(deactivateCoroutine);
        }

        deactivateCoroutine = StartCoroutine(Deactivate());
    }

    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2); 
        gameObject.SetActive(false);
        deactivateCoroutine = null; 
    }
}
