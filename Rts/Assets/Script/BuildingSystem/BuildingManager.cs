using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    public GameObject pendingObject;

    private Vector3 pos;

    [SerializeField] private LayerMask LayerMask;

    public float grifSize;

    void Update()
    {
        if (pendingObject != null)
        {
            pendingObject.transform.position = new Vector3(
                RoundToNearestGrid(pos.x),
                RoundToNearestGrid(pos.y) + 7.5f,
                RoundToNearestGrid(pos.z)
                );

            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }

    public void PlaceObject()
    {
        pendingObject = null;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectedObject(int index)
    {
        pendingObject = Instantiate(objects[index],pos,transform.rotation);
    }

    float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % grifSize;
        pos -= xDiff;
        if(xDiff > (grifSize / 2))
        {
            pos += grifSize;
        }
        return pos;
    }
}
