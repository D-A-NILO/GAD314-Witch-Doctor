using UnityEngine;

public class MortarPestle : CraftingTable
{
     [SerializeField] private GameObject defaultIfNull;
    protected override Ingredient CraftIngredient()
    {
        GameObject prefab = heldIngredient.data.crushedPrefab;
        if(!prefab)
            prefab = defaultIfNull;
            
        GameObject obj = Instantiate(prefab, holdPoint.position, holdPoint.rotation);
        
        Destroy(heldIngredient.gameObject);

        return obj.GetComponent<Ingredient>();
    }
}
