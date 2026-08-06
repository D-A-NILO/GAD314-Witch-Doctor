using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    
    public Vector3 openOffset = new Vector3(0, -4f, 0); 
    public float moveSpeed = 4f;

    
    public bool autoClose = false;
    public float autoCloseDelay = 2.5f;

    
    public UnityEvent OnDoorOpened;
    public UnityEvent OnDoorClosed;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;
    private Coroutine moveRoutine;

    void Start()
    {
        
        closedPosition = transform.position;
        openPosition = closedPosition + openOffset;
    }

    
    public void Open()
    {
        if (isOpen) return;
        isOpen = true;

        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveDoor(openPosition));

        OnDoorOpened?.Invoke();

        if (autoClose)
        {
            StartCoroutine(AutoCloseCountdown());
        }
    }

   
    public void Close()
    {
        if (!isOpen) return;
        isOpen = false;

        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveDoor(closedPosition));

        OnDoorClosed?.Invoke();
    }

    private IEnumerator AutoCloseCountdown()
    {
        yield return new WaitForSeconds(autoCloseDelay);
        Close();
    }

    private IEnumerator MoveDoor(Vector3 targetPos)
    {
       
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.position = targetPos;
    }
}
