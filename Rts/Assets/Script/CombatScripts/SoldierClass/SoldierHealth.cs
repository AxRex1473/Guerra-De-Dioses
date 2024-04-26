using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SoldierHealth : MonoBehaviour
{
    private float currentHealth; //Hacer privada terminando el testeo
    private Soldier soldier;
    private void Start()
    {
        soldier = GetComponent<Soldier>();
        SetHealth();
    }
    public void SetHealth()    
    {
        currentHealth = soldier.health;
    }

    public void ReceiveDamage(float attackDamage)
    {
        currentHealth -= attackDamage;
        if (currentHealth <= 0)
        {
            soldier.OnDead();
        }
    }
}
