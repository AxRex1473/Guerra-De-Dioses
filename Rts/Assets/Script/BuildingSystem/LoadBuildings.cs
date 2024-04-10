using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingsData
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
}

public class LoadBuildings : MonoBehaviour
{
    public static List<BuildingsData> estructureObjects = new List<BuildingsData>();

}
