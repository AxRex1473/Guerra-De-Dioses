using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    //Este codigo es de prueba para poder seleccionar a un enemigo  para convertirlo en target y testearlo
    //En la implementacion final mediante el player controller se tendrá que seleccionar primero una unidad militar para despues asignarle un enemigo el cual se convertirá en target
    public Vector3 clickPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                clickPosition = hit.transform.position;
            }
            Debug.Log("Posición del último objeto clickeado: " + clickPosition);
        }
    }
}
