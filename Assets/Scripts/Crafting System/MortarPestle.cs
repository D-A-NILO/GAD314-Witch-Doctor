using UnityEngine;

public class MortarPestle : CraftingTable
{
    protected override Ingredient CraftIngredient()
    {
        GameObject obj = Instantiate(heldIngredient.crushedPrefab, holdPoint.position, holdPoint.rotation);
        
        Destroy(heldIngredient.gameObject);

        return obj.GetComponent<Ingredient>();
    }
}
