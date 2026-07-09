using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientData data;
    public int craftInteractionsRequired;
    
    public CraftingTable heldInTable = null;


    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out CraftingUtensil utensil))
        {
            if(heldInTable)
                heldInTable.IngredientInteracted(this, utensil);
        }
    }

}
