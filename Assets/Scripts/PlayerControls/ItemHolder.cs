using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] private float holdForce = 50;
    [SerializeField] private float rotationLockFactor = 0.1f;
    [SerializeField] private float holdLinearDamp = 1;
    [SerializeField] private float holdAngularDamp = 1;
    [SerializeField] private float itemMoveSens = 0.01f;
    [SerializeField] private float maxOffset = 1.5f;
    [SerializeField] private PlayerCam playerCam;
    [SerializeField] private float holdDamping = 10f;

    private Rigidbody holdingRB;
    private float oldholdLDamp;
    private float oldholdADamp;
    private bool lockRotation;
    private Vector2 holdOffset;
    public bool IsHoldingItem()
    {
        return holdingRB != null;
    }

    public void GrabRB(Rigidbody rb, bool lockRot)
    {
        holdingRB = rb;

        // update values
        oldholdLDamp = rb.linearDamping;
        oldholdADamp = rb.angularDamping;
        rb.linearDamping = holdLinearDamp;
        rb.angularDamping = holdAngularDamp;
        //rb.useGravity = false;

        lockRotation = lockRot;
        holdOffset = Vector2.zero;
    }

    public Rigidbody ReleaseHeldRB()
    {
        Rigidbody rb = holdingRB;
        holdingRB = null;
        
        //update values
        rb.linearDamping = oldholdLDamp;
        rb.angularDamping = oldholdADamp;
        rb.useGravity = true;

        return rb;
    }

    private void Update()
    {
        if (holdingRB == null || playerCam == null) return;

        if (playerCam.FreezeCam)
        {
            Vector2 delta = playerCam.MouseDelta * itemMoveSens;
            holdOffset += delta;
            holdOffset = Vector2.ClampMagnitude(holdOffset, maxOffset);
        }
        else
        {
            holdOffset = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        //physics move to current pos
        if(holdingRB == null) return;

        Vector3 targetPos = transform.position + playerCam.transform.right * holdOffset.x + playerCam.transform.up * holdOffset.y;  


        Vector3 currentPos = holdingRB.position;
        Vector3 dir = (targetPos - currentPos).normalized;
        float distance = Vector3.Distance(targetPos, currentPos);

        Vector3 springForce = dir * distance * holdForce;
        Debug.DrawRay(holdingRB.position, springForce, Color.blue);

        holdingRB.AddForce(springForce, ForceMode.Force);

        //holdingRB.linearVelocity = (targetPos - holdingRB.position);

        if(lockRotation)
        {
            Quaternion clampedRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            holdingRB.rotation = Quaternion.Slerp(holdingRB.rotation, clampedRotation, rotationLockFactor * Time.fixedDeltaTime);
        }

    }


}
