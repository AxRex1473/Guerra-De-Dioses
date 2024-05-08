using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> UnitList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    private static UnitSelections _instance;
    //Singleton, así que aún cambiando o reiniciando escenas se mantendrá
    public static UnitSelections Instance { get { return _instance; } }

    private void Awake()
    {
        //If an instance of this already exists and it isn't this one
        if (_instance != null && _instance != this)
        {
            //We destroy this instance
            Destroy(this.gameObject);
        }
        else
        {
            //Make this instance
            _instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitsSelected.Add(unitToAdd);
        unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        unitToAdd.GetComponent<UnitMovement>().enabled = true;
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }
        else
        {
            unitToAdd.GetComponent<UnitMovement>().enabled = false;
            unitToAdd.transform.GetChild(0).gameObject.SetActive(false);
            unitsSelected.Remove(unitToAdd);
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitsSelected.Contains(unitToAdd))
        {
            unitsSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<UnitMovement>().enabled = true;
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            unit.GetComponent<UnitMovement>().enabled = false;
            unit.transform.GetChild(0).gameObject.SetActive(false);
        }

        unitsSelected.Clear();
    }

    public void Deselect(GameObject unitToDeselect)
    {

    }
}
