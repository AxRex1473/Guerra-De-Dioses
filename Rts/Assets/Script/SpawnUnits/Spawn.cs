using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject nativePrefab;
    public Transform spawnPoint;
    public int spawnIndex = 3;
    private int totalNativesSpawned = 0;

    // Flag to check if natives have been spawned
    private bool nativesSpawnedAllAtOnce = false;

    void Start()
    {
        SpawnAllNatives(); // Spawn all natives at once when the script starts
    }

    public void SpawnAllNatives()
    {
        int totalNatives = StatCon.totalNative; // Get the total number of natives from StatCon
        if (!nativesSpawnedAllAtOnce)
        {
            // Spawn all natives at once
            for (int i = 0; i < Mathf.Min(totalNatives, spawnIndex); i++)
            {
                Vector3 pos = spawnPoint.position;
                Quaternion rot = spawnPoint.rotation;
                Instantiate(nativePrefab, pos, rot);
                totalNativesSpawned++;
            }
            nativesSpawnedAllAtOnce = true; // Set flag to true after all natives have been spawned at once                        
        }
        StartCoroutine(NativesSpawn());
    }

    IEnumerator NativesSpawn()
    {
        while (totalNativesSpawned < spawnIndex)
        {
            yield return new WaitForSeconds(3);
            Vector3 pos = spawnPoint.position;
            Quaternion rot = spawnPoint.rotation;
            Instantiate(nativePrefab, pos, rot);
            totalNativesSpawned++;
        }
    }
}

