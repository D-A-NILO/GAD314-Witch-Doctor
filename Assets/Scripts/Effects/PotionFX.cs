using System;
using System.Security;
using UnityEngine;

public class PotionFX : MonoBehaviour
{
    [SerializeField] private Renderer liquidRenderer;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float wobbleFactor = 0.3f;
    [SerializeField] private float wobbleSmoothing = 5f;

    float xWobble;
    float zWobble;

    public void Show(bool show)
    {
        liquidRenderer.enabled = show;
    }

    public void SetColor(Color col)
    {
        liquidRenderer.material.SetColor("_Color", col);
    }

    

    void Update()
    {
        xWobble = Mathf.Lerp(xWobble, rb.linearVelocity.x, wobbleSmoothing * Time.deltaTime);
        zWobble = Mathf.Lerp(zWobble, rb.linearVelocity.z, wobbleSmoothing * Time.deltaTime);

        liquidRenderer.material.SetFloat("_xWobble", xWobble * wobbleFactor);
        liquidRenderer.material.SetFloat("_zWobble", zWobble * wobbleFactor);  
    }

}
