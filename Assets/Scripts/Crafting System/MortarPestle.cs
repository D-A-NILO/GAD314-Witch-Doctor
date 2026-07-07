using UnityEngine;

public class MortarPestle : CraftingTable
{
    protected override Ingredient CraftIngredient()
    {
        GameObject obj = Instantiate(heldIngredient.crushedPrefab, transform.position, transform.rotation);
        
        Destroy(heldIngredient.gameObject);

        return obj.GetComponent<Ingredient>();
    }
}
