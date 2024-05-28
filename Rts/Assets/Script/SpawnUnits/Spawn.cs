using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject nativePrefab;
    public Transform spawnPoint;
    private int _spawnIndex = 1;
    private int _totalNativesSpawned = 0;

    // Flag to check if natives have been spawned
    [SerializeField]private bool _nativesSpawnedAllAtOnce = false;

    //La funci�n Start sirve gracias al bool nativesSpawned... con el cu�l permite a otras casas que se a�adan adicionalmente a continuar Spawneando 
    void Start()
    {
        if(_nativesSpawnedAllAtOnce)
        {
            StartCoroutine(NativesSpawn());
        }
        
    }

    public void SpawnAllNatives()
    {
        int totalNatives = StatCon.totalNative; // Get the total number of natives from StatCon
        if (!_nativesSpawnedAllAtOnce)
        {
            // Se calcula cu�ntos nativos en promedio se tienen que generar por SpawnScript.
            for (int i = 0; i < Mathf.Min(totalNatives, _spawnIndex); i++)
            {
                Vector3 pos = spawnPoint.position;
                Quaternion rot = spawnPoint.rotation;
                Instantiate(nativePrefab, pos, rot);
                _totalNativesSpawned++;
            }
            _nativesSpawnedAllAtOnce = true; // Set flag to true after all natives have been spawned at once                        
        }

    }

    IEnumerator NativesSpawn()
    {
        while (_totalNativesSpawned < _spawnIndex)
        {
            yield return new WaitForSeconds(3);
            Vector3 pos = spawnPoint.position;
            Quaternion rot = spawnPoint.rotation;
            Instantiate(nativePrefab, pos, rot);
            _totalNativesSpawned++;
            Debug.Log("Spawneando");
        }
    }
}

