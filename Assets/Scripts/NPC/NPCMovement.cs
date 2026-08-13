using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] destinationPoints;
    public float moveSpeed;
    public float stoppingDistance = 0.1f;

    public int currentPoint = 0;
    public bool isMoving = true;
    public bool pathFinished = false;
    public Animator animator;


    // Update is called once per frame
    void Update()
    {
        if (!isMoving || pathFinished)
            return;

        MoveToCurrentPoint();
    }

    private void MoveToCurrentPoint()
    {
        if (currentPoint >= destinationPoints.Length)
        {
            Debug.Log("NPC has finished all destination points");
            isMoving = false;
            pathFinished = true;
            return;
        }

        Transform target = destinationPoints[currentPoint];

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        Vector3 direction = transform.position - target.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.001f)
        { 
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target.position) <= stoppingDistance)
        {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
            isMoving = false;

            Debug.Log($"reached current destination point: {currentPoint}");
        }
        animator.SetBool("moving", isMoving);
    }

    public void ContinueToNextPoint()
    {
        if (pathFinished)
            return;

        currentPoint++;

        if (currentPoint >= destinationPoints.Length)
        {
            pathFinished = true;
            Debug.Log("NPC finished all waypoints.");
            return;
        }

        isMoving = true;
    }

    public int CurrentPoint
    {
        get { return currentPoint; }
    }

    public void SetPath(Transform[] points)
    {
        destinationPoints = points;
        Debug.Log($"assigned {destinationPoints.Length} destination points");

        currentPoint = 0;
        isMoving = true;
    }
}
