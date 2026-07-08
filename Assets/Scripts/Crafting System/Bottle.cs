using UnityEngine;

public class Bottle : MonoBehaviour
{
    private PotionData potionData;
    public Renderer liquidRenderer;

    void Start()
    {
        Empty(); 
    }

    public void Fill(PotionData potion)
    {
        potionData = potion;
        
        liquidRenderer.material.SetColor("_Color", potion.color);
        liquidRenderer.enabled = true;
    }

    public void Empty()
    {
        potionData = null;
        liquidRenderer.enabled = false;
    }
}
