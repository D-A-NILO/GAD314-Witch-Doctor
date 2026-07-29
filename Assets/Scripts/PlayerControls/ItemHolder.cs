using TMPro;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [SerializeField] private float holdForce = 50;
    [SerializeField] private float rotationLockFactor = 0.1f;
    [SerializeField] private float holdLinearDamp = 1;
    [SerializeField] private float holdAngularDamp = 1;
    [SerializeField] private float gravityCounter = 1f;
    [SerializeField] private float itemMoveSens = 0.01f;
    [SerializeField] private PlayerCam playerCam;
    [SerializeField] private float holdDamping = 10f;
    [SerializeField] private Vector2 freeMouseMax = Vector2.one;
    [SerializeField] private Vector2 freeMouseMin = -Vector2.one;
    [SerializeField] private CrosshairIndicator crosshair;
    [HideInInspector] public float holdDistance;
    

    private Rigidbody holdingRB;
    private float oldholdLDamp;
    private float oldholdADamp;
    private bool lockRotation;
    public Vector2 cursorScreenPos {get; private set;}
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

    public TMP_Text tmpOut;
    
    private void Update()
    {

        //if (holdingRB == null ) return;
        if(playerCam.FreezeCam)
        {
            

            
            Vector2 nMousePos = ScreenToNormalised(Input.mousePosition);
            float mx = Mathf.Clamp(nMousePos.x, freeMouseMin.x, freeMouseMax.x);
            float my = Mathf.Clamp(nMousePos.y, freeMouseMin.y, freeMouseMax.y);
            nMousePos = new Vector2(mx, my);

            cursorScreenPos = NormalisedToScreen(nMousePos);
            crosshair.SetPositionFromNormal(nMousePos);

            tmpOut.text = nMousePos + "\n" + cursorScreenPos;
        }
        else
        {
            cursorScreenPos = NormalisedToScreen(Vector2.zero);
            crosshair.SetPositionFromNormal(Vector2.zero);

        }
        

            //Debug.Log(cursorScreenPos);
        
        
                
    }

    private Vector2 ScreenToNormalised(Vector2 mouseIn)
    {        
        Vector2 screenRes = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);

        Vector2 remappedMousePos = (Vector2)mouseIn - screenRes * 0.5f;
        Vector2 normalisedMousePos = new Vector2(remappedMousePos.x / screenRes.x * 2, remappedMousePos.y / screenRes.y * 2);
        return normalisedMousePos;

    }
    private Vector2 NormalisedToScreen(Vector2 normaliseddIn)
    {
        Vector2 screenRes = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
        
        Vector2 remappedMousePos = new Vector2(normaliseddIn.x * screenRes.x * 0.5f, normaliseddIn.y * screenRes.y * 0.5f);        
        Vector2 mouseOut = (Vector2)remappedMousePos + screenRes * 0.5f;
        return mouseOut;

    }

    void FixedUpdate()
    {
        //physics move to current pos

        Vector3 targetPos = Camera.main.ScreenPointToRay(cursorScreenPos, Camera.MonoOrStereoscopicEye.Mono).GetPoint(holdDistance);
        Debug.DrawLine(playerCam.transform.position, targetPos);  
        
        if(holdingRB == null) return;

        Vector3 currentPos = holdingRB.position;
        Vector3 dir = (targetPos - currentPos).normalized;
        float distance = Vector3.Distance(targetPos, currentPos);

        Vector3 springForce = dir * distance * holdForce;
        Debug.DrawRay(holdingRB.position, springForce, Color.blue);


        holdingRB.AddForce(springForce + gravityCounter * Vector3.up, ForceMode.Force);

        //holdingRB.linearVelocity = (targetPos - holdingRB.position);

        if(lockRotation)
        {
            Quaternion clampedRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            holdingRB.rotation = Quaternion.Slerp(holdingRB.rotation, clampedRotation, rotationLockFactor * Time.fixedDeltaTime);
        }

    }


}
