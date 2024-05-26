using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    public Vector3 areaSpawn;
    public Vector3 offsetSpawn;
    public int spawnRatio;
    public int soldierCount;
    public GameObject soldierPrefab;

    [ContextMenu("Spawnea")]
    private void Spawn()
    {
        StartCoroutine(SpawnSoldiers());
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + offsetSpawn, areaSpawn);
    }

    IEnumerator SpawnSoldiers()
    {
        for (int i = 0; i < soldierCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(soldierPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(spawnRatio);
        }
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

}
