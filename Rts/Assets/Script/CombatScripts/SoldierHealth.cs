using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SoldierHealth : MonoBehaviour
{
    public float currentHealth;
    public SoldierStats stats;
    public Action OnHurt { get; set; }

    private void Start()
    {
        SetHealth();
    }
    public void SetHealth()    
    {
        currentHealth = stats.health;
    }

    public void ReceiveDamage(float attackDamage)
    {
        currentHealth -= attackDamage;
        if (currentHealth <= 0)
        {
            if (OnHurt != null)
            {
                OnHurt.Invoke();
            }
        }
    }
}
