using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public GameObject target;
    [HideInInspector] public float damage;
    private float speed = 3f;
    private float progress = 0f;
    private Vector3 offset = new Vector3(0f, 1.2f, 0f);
    private Vector3 initialPosition;
    public GameObject flechaPrefab; 
    public bool lanzarFlecha = false; 
    public Transform objetivo;
    private void Awake()
    {
        initialPosition = transform.position;
    }

    public float Move()
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(initialPosition, target.transform.position + offset, progress);

        return progress;
    }
    void Update()
    {
        if (lanzarFlecha)
        {
            LanzarFlecha();
            lanzarFlecha = false; // Reiniciar el booleano después de lanzar la flecha
        }
    }

    void LanzarFlecha()
    {
        GameObject flecha = Instantiate(flechaPrefab, transform.position, Quaternion.identity);
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        Quaternion rotacion = Quaternion.LookRotation(direccion);
        flecha.transform.rotation = rotacion;
        Rigidbody rb = flecha.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(direccion * 10f, ForceMode.Impulse);
        }
    }
}
