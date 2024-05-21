using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitAI : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 5f;
    public float waitTime = 2f;
    public int unitsToCollect = 5;
    public TextMeshProUGUI unitsText;
    public Animator animator;

    private bool movingToA = true;
    private bool atPoint = false;
    private int collectedUnits = 0;
    private float waitTimer = 0f;
    //private float originalYPosition;

    /*private void Start()
    {
        originalYPosition = transform.position.y;
    }*/

    void Update()
    {
        if (!atPoint)
        {
            Vector3 targetPosition = movingToA ? pointA.position : pointB.position;
            float distance = Vector3.Distance(transform.position, targetPosition);

            if (distance < 0.1f)
            {
                atPoint = true;
                waitTimer = 0f;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                if (waitTimer >= waitTime && !movingToA)
                {
                    collectedUnits += unitsToCollect;
                    Debug.Log("Units collected: " + collectedUnits);
                }

                movingToA = !movingToA;
                atPoint = false;
            }
        }

        
        /*Vector3 newPosition = transform.position;
        newPosition.y = originalYPosition;
        transform.position = newPosition;*/

        animator.SetFloat("move1", moveSpeed);

        // Update Units
        unitsText.text = "Units: " + collectedUnits.ToString();
    }
}