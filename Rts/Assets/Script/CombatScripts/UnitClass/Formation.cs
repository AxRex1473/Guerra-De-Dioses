using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    private Vector3 leaderPosition;
    private List<Vector3> soldierPositions = new List<Vector3>();

    public Formation(Vector3 leaderPosition)
    {
        this.leaderPosition = leaderPosition;
    }

    public void AddSoldier(Vector3 relativePosition)
    {
        Vector3 absolutePosition = leaderPosition + relativePosition;
        soldierPositions.Add(absolutePosition);
    }

    public List<Vector3> GetSoldierPositions()
    {
        return soldierPositions;
    }
}
