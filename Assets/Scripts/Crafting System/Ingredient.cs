using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientData data;
    public int craftInteractionsRequired;
    
    public CraftingTable heldInTable = null;

    void Start()
    {
        if(data == null)
        {
            Debug.LogError($"Ingredient {name} does not have data assigned");
            return;
        }
        if(TryGetComponent(out Grabbable grabbable))
        {
            grabbable.displayName = data.name;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out CraftingUtensil utensil))
        {
            if(heldInTable)
                heldInTable.IngredientInteracted(this, utensil);
        }
    }

}
