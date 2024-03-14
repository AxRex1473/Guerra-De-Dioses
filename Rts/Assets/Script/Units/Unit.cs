using UnityEngine;

public class Unit : MonoBehaviour
{
    void Start()
    {
        UnitSelections.Instance.UnitList.Add(this.gameObject);
    }

    void OnDestroy()
    {
        UnitSelections.Instance.UnitList.Remove(this.gameObject);
    }
}
