using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NPCGrabbable : Grabbable
{

    [SerializeField] private Animator[] animators;
    [SerializeField] private float returnSpeed = 2f;
    [SerializeField] private float returnTimeout = 10f;

    private NPCMovement npcMovement;
    private Rigidbody rb;
    private DialogueTrigger dialogueTrigger;
    private Dialogue dialogue;

    private Vector3 homePosition;
    private Quaternion homeRotation;
    private bool wasMoving;
    private Coroutine returnRoutine;

    protected override void Start()
    {
        base.Start();

        npcMovement = GetComponent<NPCMovement>();
        rb = GetComponent<Rigidbody>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogue = FindFirstObjectByType<Dialogue>(FindObjectsInactive.Include);

        if (animators == null || animators.Length == 0)
            animators = GetComponentsInChildren<Animator>();
    }

    public override void OnInteract(PlayerInteract interactor)
    {
        if (dialogue == null)
            dialogue = FindFirstObjectByType<Dialogue>(FindObjectsInactive.Include);

       
        if (dialogueTrigger != null && dialogue != null && !dialogue.IsDialogueActive)
        {
            dialogueTrigger.OnInteract(interactor);
            return;
        }

        Grab(interactor);
    }

    private void Grab(PlayerInteract interactor)
    {
    
        if (returnRoutine != null)
        {
            StopCoroutine(returnRoutine);
            returnRoutine = null;
        }
        else
        {
            homePosition = transform.position;
            homeRotation = transform.rotation;
        }

        if (npcMovement != null)
        {
            wasMoving = npcMovement.isMoving;
            npcMovement.isMoving = false;
            npcMovement.enabled = false;
        }

        foreach (Animator animator in animators)
        {
            if (animator != null)
                animator.enabled = false;
        }

        base.OnInteract(interactor);
    }

    public override void OnDrop()
    {
        base.OnDrop();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

   
        foreach (Animator animator in animators)
        {
            if (animator != null)
                animator.enabled = true;
        }

        returnRoutine = StartCoroutine(ReturnHome());
    }

    private IEnumerator ReturnHome()
    {
        float fallTimeout = 3f;
        while (fallTimeout > 0f && !IsGrounded())
        {
            fallTimeout -= Time.deltaTime;
            yield return null;
        }

        float speed = npcMovement != null && npcMovement.moveSpeed > 0f ? npcMovement.moveSpeed : returnSpeed;

       
        float timeLeft = returnTimeout;
        Vector3 toHome = homePosition - rb.position;
        toHome.y = 0;

        while (toHome.magnitude > 0.1f && timeLeft > 0f)
        {
            Vector3 direction = toHome.normalized;
            rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.z * speed);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.fixedDeltaTime);

            timeLeft -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();

            toHome = homePosition - rb.position;
            toHome.y = 0;
        }

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.SetPositionAndRotation(homePosition, homeRotation);
        returnRoutine = null;

        if (npcMovement != null)
        {
            npcMovement.enabled = true;
            npcMovement.isMoving = wasMoving;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.25f);
    }
}
