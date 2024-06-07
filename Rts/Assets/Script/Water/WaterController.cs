using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public Material waterMaterial;
    public float waveSpeed = 1.0f;
    public float waveScale = 0.02f;

    void Update()
    {
        waterMaterial.SetFloat("_WaveSpeed", waveSpeed);
        waterMaterial.SetFloat("_WaveScale", waveScale);
    }
}
