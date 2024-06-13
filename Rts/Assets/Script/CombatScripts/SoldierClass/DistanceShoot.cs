using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceShoot : MonoBehaviour
{
    private GameObject target;
    public Transform shootTransform;
    public Soldier soldier;
    void AssignTarget()
    {
        if (target == null)
        {
            target = soldier.target;
        }
    }
    void LanzarFlecha()
    {
        GameObject flecha = ArrowPooling.instance.GetPooledObject();
        if (flecha != null)
        {
            flecha.transform.position = shootTransform.position;
            flecha.SetActive(true);
        }
        Vector3 direccion = (target.transform.position - transform.position).normalized;
        Quaternion rotacion = Quaternion.LookRotation(direccion);
        flecha.transform.rotation = rotacion;
        Rigidbody rb = flecha.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direccion * 60f, ForceMode.Impulse);
        }
        StartCoroutine(DesactivarFlechaDespuesDeTiempo(flecha, 1f));
    }

    IEnumerator DesactivarFlechaDespuesDeTiempo(GameObject flecha, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        flecha.SetActive(false);
    }
}
