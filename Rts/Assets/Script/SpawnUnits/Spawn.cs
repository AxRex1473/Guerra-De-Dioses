using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject nativePrefab;
    public Transform spawnPoint;
    private int _spawnIndex = 3;
    private int _totalNativesSpawned = 0;

    // Flag to check if natives have been spawned
    [SerializeField] private bool _nativesSpawnedAllAtOnce = false;

    //La función Start sirve gracias al bool nativesSpawned... con el cuál permite a otras casas que se añadan adicionalmente a continuar Spawneando 
    void Start()
    {
        if (_nativesSpawnedAllAtOnce)
        {
            StartCoroutine(NativesSpawn());
            //SpawnAllNatives();
        }
    }

    //Esto lo hace cada casa así que debería de tener otro Script que haga esta función.
    public void SpawnAllNatives(float Natives)
    {
        //int totalNatives = StatCon.totalNative; // Get the total number of natives from StatCon
        if (!_nativesSpawnedAllAtOnce)
        {
            // Se calcula cuántos nativos en promedio se tienen que generar por SpawnScript.
            for (float i = 0; i < Mathf.Min(_spawnIndex, Natives); i++)
            {
                Vector3 pos = spawnPoint.position;
                Quaternion rot = spawnPoint.rotation;
                Instantiate(nativePrefab, pos, rot);
                _totalNativesSpawned++;
                
            }
            StartCoroutine(NativesSpawn());
            
            // Set flag to true after all natives have been spawned at once                        
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
            _nativesSpawnedAllAtOnce = true;
            Debug.Log("Spawneando");
        }
    }
}