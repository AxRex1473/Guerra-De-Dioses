using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoldierBase : MonoBehaviour
{
    public States state = States.Idle;
    public enum States
    {
        Idle,
        Seeking,
        Attacking,
        Dead,
    }
    [HideInInspector] public AttackType attackType;
    public enum AttackType
    {
        Melee,
        Ranged
    }
    [HideInInspector] public Transform target; //Momentaneo

    [Header("Proyectiles")] //Parte de este apartado podria vivir en el ScripObject pero lo sigo analizando 
    public GameObject projectilePrefab; 
    public Transform projectileSpawnPoint;
    private Projectile projectile; //Codigo base para las flechas
    protected AudioSource audioSource; //Tengo duda de si esto va aqui pero depende de como quieran manejar el audio
    public UnityAction<SoldierBase> OnDealDamage, OnProjectileFired, OnDie;

    [HideInInspector] public int health;
    [HideInInspector] public float velocity;
    [HideInInspector] public float attackRange;
    [HideInInspector] public float attackDamage;
    [HideInInspector] public float attackRatio; 
    [HideInInspector] public float detectRange;
    public virtual void SetTarget()
    {
    }
    public bool TargetInRange()
    {
        return (transform.position - target.transform.position).sqrMagnitude <= attackRange * attackRange;
    }
    public virtual void StartAttack()
    {
        state = States.Attacking; 
    }

    public virtual void FireProjectile()
    {

    }

    public virtual void Seek()
    {
        state = States.Seeking;
    }
    public virtual void Stop()
    {
        state = States.Idle;
    }
    protected virtual void Die()
    {
        state = States.Dead;
    }
}
