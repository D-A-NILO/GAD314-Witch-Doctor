using JetBrains.Annotations;
using UnityEngine;

public class ChoppingBoard : CraftingTable
{
    protected override Ingredient CraftIngredient()
    {
        
        GameObject obj = Instantiate(heldIngredient.choppedPrefab, holdPoint.position, holdPoint.rotation);
        
        Destroy(heldIngredient.gameObject);

        return obj.GetComponent<Ingredient>();
    }
}
