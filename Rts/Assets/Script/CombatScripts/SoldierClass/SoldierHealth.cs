using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class SoldierHealth : MonoBehaviour
{
    private float currentHealth; 
    private Soldier soldier;
    [SerializeField] private Slider healthBar;
    private void Start()
    {
        soldier = GetComponent<Soldier>();
        SetHealth();
    }
    public float Health
    {
        get { return currentHealth; }
    }
    public void SetHealth()    
    {
        currentHealth = soldier.health;
        SetHealthBar();
    }
    private void SetHealthBar()
    {
        healthBar.value = (float)currentHealth / (float)soldier.health;
    }

    public IEnumerator ReceiveDamage(float attackDamage, float delay)
    {
        yield return new WaitForSeconds(delay);
        currentHealth -= attackDamage;
        if (currentHealth <= 0)
        {
            soldier.OnDead();
        }
        SetHealthBar();
    }
}
