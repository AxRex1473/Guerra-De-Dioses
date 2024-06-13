using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 areaSpawn;
    [SerializeField] Vector3 offsetSpawn;
    [SerializeField] int spawnRatio;
    [SerializeField] bool AllyBase;
    public int soldierCount;
    public int soldierMaxAmount;
    public GameObject[] soldierPrefabs;  // Arreglo para múltiples prefabs
    public int selectedPrefabIndex = 0;  // Índice del prefab seleccionado


    private Queue<int> spawnQueue = new Queue<int>(); // Cola de spawneo
    private Coroutine spawnCoroutine;

    [ContextMenu("Spawnea")]
    public void Spawn()
    {
        EnqueueSoldiers(soldierCount);

        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(ProcessSpawnQueue());
        }
    }

    private void EnqueueSoldiers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            spawnQueue.Enqueue(selectedPrefabIndex);
        }
    }

    private IEnumerator ProcessSpawnQueue()
    {
        while (spawnQueue.Count > 0)
        {
            if (soldierMaxAmount > 0)
            {
                int prefabIndex = spawnQueue.Dequeue();
                yield return new WaitForSeconds(spawnRatio);
                SpawnSoldier(prefabIndex);
            }
            else
            {
                yield return null;
            }
        }

        // Detenemos la corrutina cuando la cola esté vacía
        spawnCoroutine = null;
    }

    private void SpawnSoldier(int prefabIndex)
    {
        Vector3 randomPosition = GetRandomPosition();
        if (soldierPrefabs.Length > 0 && prefabIndex >= 0 && prefabIndex < soldierPrefabs.Length)
        {
            GameObject soldier = Instantiate(soldierPrefabs[prefabIndex], randomPosition, Quaternion.identity);
            if (!AllyBase)
            {
                Identifier(soldier);
            }
            soldierMaxAmount--;
        }
        else
        {
            Debug.LogWarning("No existe un prefab con ese index");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + offsetSpawn, areaSpawn);
    }
    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            transform.position.x + offsetSpawn.x + Random.Range(-areaSpawn.x / 2, areaSpawn.x / 2),
            transform.position.y + offsetSpawn.y + Random.Range(-areaSpawn.y / 2, areaSpawn.y / 2),
            transform.position.z + offsetSpawn.z + Random.Range(-areaSpawn.z / 2, areaSpawn.z / 2)
        );
        return randomPosition;
    }
    private void Identifier(GameObject soldier)
    {
        Soldier soldierScript = soldier.GetComponent<Soldier>();
        if (soldierScript != null)
        {
            soldierScript.targetLayer = LayerMask.GetMask(LayerMask.LayerToName(10));
            soldierScript.gameObject.tag = "Enemy";
            soldierScript.gameObject.layer = 8;
        }
        else
        {
            Debug.LogWarning("El objeto instanciado no tiene el componente Soldier.");
        }
    }
}
