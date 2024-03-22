using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth2 : MonoBehaviour
{
    public float CurrentHealth;

    private void Start()
    {
        CurrentHealth = 100;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AldeanoSoldado"))
        {
            CurrentHealth -= 10;
        }
    }
}
