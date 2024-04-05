using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    // Update is called once per frame
    void Awake()
    {
        // Get all objects with Spawn script
        Spawn[] spawnScripts = FindObjectsOfType<Spawn>();

        int totalHouses = spawnScripts.Length;
        int maxTotalNatives = StatCon.totalNative;// Total number of natives to spawn across all houses
        int totalNativesSpawned = spawnScripts.Sum(script => script.spawnIndex);

        int remainingSpawns = maxTotalNatives - totalNativesSpawned;

        int nativesPerHouse = Mathf.Min(3, remainingSpawns / totalHouses);

        /*
        foreach (Spawn spawnScript in spawnScripts)
        {
            spawnScript.SpawnTotalNatives(); // Spawn natives per house
        }*/

    }
}
