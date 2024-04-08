using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingsData
{
    public List<GameObject> Buildings;
}

public class LoadBuildings : MonoBehaviour
{
    public static List<GameObject> estructureObjects;

    void Start()
    {
        // Find all objects with the "Estructure" tag and add them to the list
        GameObject[] estructureArray = GameObject.FindGameObjectsWithTag("Estructure");
        estructureObjects = new List<GameObject>(estructureArray);

        // Now you can use estructureObjects list to access all objects with the "Estructure" tag
    }
}
