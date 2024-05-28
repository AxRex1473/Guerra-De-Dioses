using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoldierBase : MonoBehaviour
{
    [HideInInspector] public States state = States.Idle;
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
    [HideInInspector] public SoldierBase target;

    [Header("Flechas")] //Parte de este apartado podria vivir en el ScripObject pero lo sigo analizando 
    public GameObject arrowPrefab; //Le puse arrow pero tambien pueden ser lanzas?
    public Transform arrowSpawnPoint;
    //private Flechas flechas; PORHACER: Implementacion de futuro ataque a distancia
    protected AudioSource audioSource; //Tengo duda de si esto va aqui pero depende de como quieran manejar el audio
    public UnityAction<SoldierBase> OnDealDamage, OnProjectileFired, OnDie;


    public virtual void SetTarget()
    {

    }
    public virtual void StartAttack()
    {
        state = States.Attacking; 
    }

    public void FireArrow()
    {

    }


    public virtual void Seek()
    {
        state = States.Seeking;
    }
    protected virtual void Die()
    {
        state = States.Dead;
    }
}
