using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrecimientoMaiz : MonoBehaviour
{
    public GameObject[] maiz;
    public int _prefabIndex = 0;
    public float _timer = 0f;
    public float _changeInter = 5f;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _changeInter)
        {
            _timer = 0f;

            if (_prefabIndex < maiz.Length)
            {
                Instantiate(maiz[_prefabIndex], transform.position, transform.rotation);
                _prefabIndex++;
            }
        }
    }
}
