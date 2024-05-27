using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GroundIcon : MonoBehaviour
{
    public delegate void ActivationAction();

    public static event ActivationAction ActivateIcon;

    public static void Activate()
    {
        if (ActivateIcon != null)
        {
            ActivateIcon();
        }
    }
    void OnEnable()
    {
        GroundIcon.ActivateIcon += ActivateObject;
    }

    void OnDisable()
    {
        GroundIcon.ActivateIcon -= ActivateObject;
    }

    // Método que se ejecuta cuando se invoca el evento
    void ActivateObject()
    {
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
