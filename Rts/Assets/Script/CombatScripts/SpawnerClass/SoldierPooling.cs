using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPooling : MonoBehaviour
{
    public static SoldierPooling instance;
    private List<List<GameObject>> _pools = new List<List<GameObject>>();
    public int poolAmount;
    public GameObject[] prefabsToPool; // Array de prefabs que quieres usar

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // Inicializa una lista para cada prefab
        for (int p = 0; p < prefabsToPool.Length; p++)
        {
            List<GameObject> pool = new List<GameObject>();
            for (int i = 0; i < poolAmount; i++)
            {
                GameObject obj = Instantiate(prefabsToPool[p]);
                obj.SetActive(false);
                pool.Add(obj);
            }
            _pools.Add(pool);
        }
    }

    // Retorna un objeto de la piscina del tipo especificado
    public GameObject GetPooledObjects(int prefabIndex)
    {
        List<GameObject> pool = _pools[prefabIndex];
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }
}
