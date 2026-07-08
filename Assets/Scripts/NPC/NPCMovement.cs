using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public Transform[] destinationPoints;
    public float moveSpeed;
    public float stoppingDistance = 0.1f;

    public int currentPoint = 0;
    public bool isMoving = true;
    public bool pathFinished = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving || pathFinished)
            return;

        MoveToCurrentPoint();
    }

    private void MoveToCurrentPoint()
    {
        Transform target = destinationPoints[currentPoint];

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        Vector3 direction = target.position - transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.001f)
        { 
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target.position) <= stoppingDistance)
        {
            transform.position = target.position;
            isMoving = false;

            Debug.Log($"reached current destination point: {currentPoint}");
        }
    }
}
