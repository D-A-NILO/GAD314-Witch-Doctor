using JetBrains.Annotations;
using UnityEngine;

public class ChoppingBoard : CraftingTable
{
    protected override Ingredient CraftIngredient()
    {
        
        GameObject obj = Instantiate(heldIngredient.choppedPrefab, transform.position, transform.rotation);
        
        Destroy(heldIngredient.gameObject);

        return obj.GetComponent<Ingredient>();
    }
}
