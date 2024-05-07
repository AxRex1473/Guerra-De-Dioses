using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour
{
    public Transform leaderTransform; 
    public Soldier soldierPrefab; 

    private Formation formation;

    private void Start()
    {
        // Crear la formación
        formation = new Formation(leaderTransform.position);

        
        AssignSoldierPositions();
    }

    private void AssignSoldierPositions()
    {
        
        float spacing = 2.0f;
        int rows = 3; 
        int cols = 3; 

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Vector3 offset = new Vector3(i * spacing, 0, j * spacing); 
                Soldier newSoldier = Instantiate(soldierPrefab, leaderTransform.position + offset, Quaternion.identity);
                //newSoldier.SetFormationOffset(offset); 
            }
        }
    }

    private void Update()
    {
        List<Vector3> soldierPositions = formation.GetSoldierPositions();
        foreach (Vector3 position in soldierPositions)
        {

        }
    }
}
