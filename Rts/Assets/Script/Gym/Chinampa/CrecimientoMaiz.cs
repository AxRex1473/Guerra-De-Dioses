using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrecimientoMaiz : MonoBehaviour
{
    public GameObject[] maiz;
    private int _prefabIndex = 0;
    private float _timer = 15f;
    private float _changeInter = 5f;

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > _changeInter && _prefabIndex < maiz.Length)
        {
            _timer = 0f;

            Instantiate(maiz[_prefabIndex], transform.position, transform.rotation);

            _prefabIndex++;
        }
    }
}
