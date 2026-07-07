using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] private float holdForce = 50;
    [SerializeField] private float rotationLockFactor = 0.1f;
    [SerializeField] private float holdLinearDamp = 1;
    [SerializeField] private float holdAngularDamp = 1;

    private Rigidbody holdingRB;
    private float oldholdLDamp;
    private float oldholdADamp;
    public bool IsHoldingItem()
    {
        return holdingRB != null;
    }

    private bool lockRotation;
    public void GrabRB(Rigidbody rb, bool lockRot)
    {
        holdingRB = rb;

        // update values
        oldholdADamp = rb.linearDamping;
        oldholdADamp = rb.angularDamping;
        rb.linearDamping = holdLinearDamp;
        rb.angularDamping = holdAngularDamp;
        //rb.useGravity = false;

        lockRotation = lockRot;
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


    void FixedUpdate()
    {
        //physics move to current pos
        if(holdingRB == null) return;

        Vector3 targetPos = transform.position;  
        Vector3 currentPos = holdingRB.position;

        Vector3 dir = (targetPos - currentPos).normalized;
        float distance = Vector3.Distance(targetPos, currentPos);

        Vector3 springForce = dir * distance * holdForce;
        Debug.DrawRay(holdingRB.position, springForce, Color.blue);

        holdingRB.AddForce(springForce, ForceMode.Force);

        if(lockRotation)
        {
            Quaternion clampedRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            holdingRB.rotation = Quaternion.Slerp(holdingRB.rotation, clampedRotation, rotationLockFactor * Time.fixedDeltaTime);
        }
    }


}
