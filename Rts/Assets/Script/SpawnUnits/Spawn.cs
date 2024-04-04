using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject nativePrefab;
    public Transform spawnPoint;
    public int spawnIndex = 3;

    // Flag to check if natives have been spawned
    private bool nativesSpawnedAllAtOnce = false;

    void Start()
    {
        SpawnAllNatives(); // Spawn all natives at once when the script starts
    }

    void SpawnAllNatives()
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
            }
            nativesSpawnedAllAtOnce = true; // Set flag to true after all natives have been spawned at once
        }
    }

    public void SpawnTotalNatives(int numberOfNatives)
    {
        if (nativesSpawnedAllAtOnce)
        {
            int nativesSpawned = 0;
            while (nativesSpawned < numberOfNatives)
            {
                // Spawn up to 3 natives at a time
                int batchNatives = Mathf.Min(3, numberOfNatives - nativesSpawned);
                StartCoroutine(SpawnNativesWithDelay(batchNatives));
                nativesSpawned += batchNatives;
            }
        }
    }

    IEnumerator SpawnNativesWithDelay(int numberOfNatives)
    {
        for (int i = 0; i < numberOfNatives; i++)
        {
            Vector3 pos = spawnPoint.position;
            Quaternion rot = spawnPoint.rotation;
            Instantiate(nativePrefab, pos, rot);
            yield return new WaitForSeconds(3);
        }
    }
}
