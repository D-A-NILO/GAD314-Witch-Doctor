using UnityEngine;

public class Bottle : MonoBehaviour
{
    private PotionData potionData;
    public PotionData PotionData
    {
        get { return potionData;}
    }
    public Renderer liquidRenderer;

    public bool hasPotion => potionData != null;

    private Grabbable grabbable;

    // public PotionID CurrentPotionID
    // {
    //     get
    //     {
    //         if (potionData == null)
    //         {
    //             return PotionID.None;
    //         }

    //         return potionData.potionID;
    //     }
    // }

    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
    }
    void Start()
    {
        Empty(); 
    }


    public void Fill(PotionData potion)
    {
        potionData = potion;
        
        liquidRenderer.material.SetColor("_Color", potion.color);
        liquidRenderer.enabled = true;
        grabbable.DisplayName = potion.name;
    }

    public void Empty()
    {
        potionData = null;
        liquidRenderer.enabled = false;
        grabbable.name = "Empty Bottle";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (grabbable.GetHoldingInteractor() == null)
            return;

        if (!hasPotion)
            return;

        if (collision.gameObject.TryGetComponent(out NPCIllness npc))
        {
            npc.GivePotion(this);
        }
    }
}
