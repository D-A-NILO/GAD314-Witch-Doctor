using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public new string name;
    public GameObject choppedPrefab;
    public GameObject crushedPrefab;
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
