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
            Debug.Log("Prefab seleccionado: " + index);
        }
        else
        {
            Debug.LogWarning("Índice de prefab inválido: " + index);
        }
    }
}
