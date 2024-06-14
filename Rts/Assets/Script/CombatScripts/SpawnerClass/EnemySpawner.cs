using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public SoldierSpawner sp;
    void Start()
    {
        sp.soldierCount = 6;
        sp.Spawn();
    }
}
