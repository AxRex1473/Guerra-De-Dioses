using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrecimientoMaiz : MonoBehaviour
{
    public GameObject[] maiz;
    private int _prefabIndex = 0;
    private float _timer = 10f;
    private float _changeInter = 5f;

    private void Update()
    {

        _timer += Time.deltaTime;

        if (_timer > _changeInter && _prefabIndex < maiz.Length)
        {
            _timer = 0f;

            Instantiate(maiz[_prefabIndex], transform.position, transform.rotation);
            new WaitForSeconds(20);

            _prefabIndex++;
        }
    }
}
