using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconPooling : MonoBehaviour
{
    public static IconPooling instance;
    private List<GameObject> pooledObjects = new List<GameObject>(); //Objetos que voy a reciclar
    public int amountToPool; 
    [SerializeField] private GameObject iconPrefab;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(iconPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
