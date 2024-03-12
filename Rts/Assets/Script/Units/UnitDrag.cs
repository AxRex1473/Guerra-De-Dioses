using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    Camera cam;

    //graphical
    [SerializeField]
    RectTransform boxVisual;

    //logical
    Rect selectionBox;

    Vector2 startPosition;
    Vector2 endPosition;
    void Start()
    {
        cam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }

    void Update()
    {
        //when is clicked
        if(Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selectionBox = new Rect();
        }
        //when is drag
        if(Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }
        //when release click
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }
    }

    void DrawVisual()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxCenter = (boxStart + boxEnd)/2;
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
        boxVisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        //do x calculations
        if(Input.mousePosition.x < startPosition.x)
        {
            //dragging to the left
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            //dragging to the right
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }

        //do y calculations
        if(Input.mousePosition.y < startPosition.y)
        {
            //dragging down
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            //dragging up
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }

    void SelectUnits()
    {
        //loop thru all the units
        foreach(var unit in UnitSelections.Instance.UnitList)
        {
            //if unit is within the bounds of the selection rect
            if (selectionBox.Contains(cam.WorldToScreenPoint(unit.transform.position)))
            {
                //if any unit is within the selection add them to selection
                UnitSelections.Instance.DragSelect(unit);
            }
        }
    }
}
