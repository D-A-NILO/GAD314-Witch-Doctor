using UnityEngine;

public class Bottle : MonoBehaviour
{
    private PotionData potionData;
    public PotionData PotionData
    {
        get { return potionData;}
    }
    public PotionFX potionFX;

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
        Empty();
    }
    void Start()
    {
        
    }


    public void Fill(PotionData potion)
    {
        potionData = potion;
        
        potionFX.SetColor(potion.color);
        potionFX.Show(true);
        grabbable.DisplayName = potion.name;
    }

    public void Empty()
    {
        potionData = null;
        potionFX.Show(false);
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
