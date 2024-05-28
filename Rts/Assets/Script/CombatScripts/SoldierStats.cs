using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Scriptable Objects/Soldier")]
public class SoldierStats : ScriptableObject
{
    [Header("Common")]
    public string soldierName;
    public string description;
    public GameObject soldierPrefab;

    [Header("Soldier Stats")]
    public int health;
    public float velocity;

    [Header("Attack Settings")]
    public float attackDamage;
    public float attackRatio; //Tiempo entre ataques
    public float attackRange; 
    public float detectRange; //Rango en el que la unidad va a poder detectar otros enemigos


}
