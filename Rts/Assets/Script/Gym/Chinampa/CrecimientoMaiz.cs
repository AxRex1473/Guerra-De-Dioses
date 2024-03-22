using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrecimientoMaiz : MonoBehaviour
{
    public GameObject maizPrefab;
    public GameObject chinampa;
    public float growthTime;

    private bool _maizGrowth = false;

    private void Start()
    {
        
    }

    private IEnumerator MaizGrowing(Vector3 position)
    {
        _maizGrowth = true;
        GameObject maiz = Instantiate(maizPrefab, position, Quaternion.identity);
        float elapsedTime = 0;

        while (elapsedTime < growthTime)
        {
            elapsedTime += Time.deltaTime;
            maiz.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsedTime / growthTime);
            yield return null;
        }

        _maizGrowth = false;
    }

    private void Update()
    {
        if(!_maizGrowth && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit)) 
            {
                if(hit.collider.gameObject == chinampa)
                {
                    StartCoroutine(MaizGrowing(hit.point));
                }
            }
        }
    }
}
