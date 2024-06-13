using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSelector : MonoBehaviour
{
    public SoldierSpawner soldierSpawner;

    public void SelectPrefabIndex(int index)
    {
        if (index >= 0 && index < soldierSpawner.soldierPrefabs.Length)
        {
            soldierSpawner.selectedPrefabIndex = index;
            soldierSpawner.Spawn();
        }
        else
        {
            Debug.LogWarning("Índice de prefab inválido: " + index);
        }
    }
}
