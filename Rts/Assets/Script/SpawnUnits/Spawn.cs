using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject nativePrefab;
    public Transform spawnPoint;
    private int _spawnIndex = 3;
    private int _totalNativesSpawned = 0;

    // Flag to check if natives have been spawned Este lo puede modificar el SpawnManager
    [SerializeField] private bool _nativesSpawnedAllAtOnce = false;

    //La funci�n Start sirve gracias al bool nativesSpawned... con el cu�l permite a otras casas que se a�adan adicionalmente a continuar Spawneando 
    void Start()
    {
        if (_nativesSpawnedAllAtOnce)
        {
            StartCoroutine(NativesSpawn());
            //SpawnAllNatives();
        }
    }

    //Esta funci�n la llamada SpawnManager y ayuda a spawnear el total de nativos que le diga el otro spawn
    public void SpawnAllNatives(float Natives)
    {
        
        if (!_nativesSpawnedAllAtOnce)
        {
            // Se calcula cu�ntos nativos en promedio se tienen que generar por SpawnScript.
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

    //Una vez que se spawneen todos los nativos se ejecuta esta funci�n la cu�l hace que _nativesSpawnedAllAtOnce pase a ser true, de esta forma todas las casas que faltan por spawnear nativos o casas nuevas puedan seguir spawneando nativos.
    IEnumerator NativesSpawn()
    {
        //Primero se activa aqu� el booleano y no en otro lado ya que puede conflictuar con el Start que requiere el booleano,
        //As� que la l�gica es Spawnamager llama SpawnAll... despu�s se activa esta corrutina y de ah� se activa el booleano y por �ltimo si hay espacio para nativos se crean nativos
        _nativesSpawnedAllAtOnce = true;
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

    // Function to set _nativesSpawnedAllAtOnce externally
    public void SetNativesSpawnedAllAtOnce(bool value)
    {
        _nativesSpawnedAllAtOnce = value;
    }
}