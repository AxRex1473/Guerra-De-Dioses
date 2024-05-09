using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;

    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;

    void Start()
    {
        myCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out hit,Mathf.Infinity,clickable))
            {
                //if we hit a clickable object
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    //This is a function trying to work with the new list
                    //UnitSelections.UnitList.unitListData.ShiftClickSelect(hit.collider.gameObject);
                    //Shift clicked
                    //UnitSelections.UnitList.ShiftClickSelect(hit.collider.gameObject); // Access UnitList directly
                    UnitSelections.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    //normal clicked
                    UnitSelections.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                //if we didn't and we're not shift clicking
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                UnitSelections.Instance.DeselectAll();
                }   
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(false);
                groundMarker.SetActive(true);
            }
        }
    }
}
