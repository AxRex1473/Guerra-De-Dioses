using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawn : MonoBehaviour
{
    public GameObject nativePrefab;
    public Transform spawnPoint;
    private int _spawnIndex = 3; // Maximum number of natives spawned per house
    private int _totalNativesSpawned = 0; // Total number of natives spawned by this house

    // Flag to check if natives have been spawned
    [SerializeField] private bool _nativesSpawnedAllAtOnce = false;

    void Start()
    {
        if (LoadGame.loadGameDone)
        {
            CalculateNativesToSpawn();
            StartCoroutine(SpawnNatives());
        }
    }

    private void CalculateNativesToSpawn()
    {
        int totalNatives = StatCon.totalNative;
        int totalHouses = FindObjectsOfType<Spawn>().Length;

        // Calculate the number of natives each house should spawn
        _totalNativesSpawned = totalNatives / totalHouses;

        // Ensure that no house spawns more than _spawnIndex natives
        _totalNativesSpawned = Mathf.Min(_totalNativesSpawned, _spawnIndex);

        float remainingNatives = totalNatives % totalHouses;

        // If there are remaining natives, distribute them among the houses
        if (remainingNatives > 0)
        {
            float houseIndex = 0;
            foreach (var house in GameObject.FindObjectsOfType<Spawn>())
            {
                if (houseIndex < remainingNatives)
                {
                    _totalNativesSpawned++;
                }
                houseIndex++;
            }
        }
    }

    IEnumerator SpawnNatives()
    {
        // Spawn natives until the total count is reached
        while (_totalNativesSpawned > 0)
        {
            Vector3 pos = spawnPoint.position;
            Quaternion rot = spawnPoint.rotation;
            Instantiate(nativePrefab, pos, rot);
            _totalNativesSpawned--;

            yield return null; // Wait for the next frame
        }
    }
}

