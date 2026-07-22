using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class ChoppingBoard : CraftingTable
{

    [SerializeField] private GameObject defaultIfNull;
    [SerializeField] private int defaultCraftRequirement = 6;
    protected override Ingredient CraftIngredient()
    {
        GameObject prefab = heldIngredient.data.choppedPrefab;
        if (!prefab)
        {
            Debug.Log($"{heldIngredient.data.name} has no chopped prefab, creating trash instead");
            prefab = defaultIfNull;
        }
        else
        {
            Debug.Log($"{heldIngredient.data.name} will be crafted into {prefab.name}");
        }


            GameObject obj = Instantiate(prefab, holdPoint.position, holdPoint.rotation);
        
        Destroy(heldIngredient.gameObject);

        return obj.GetComponent<Ingredient>();
    }

    public override void PlaceIngredient(Ingredient ingredient)
    {
        base.PlaceIngredient(ingredient);

        GameObject prefab = heldIngredient.data.choppedPrefab;
        if(!prefab)
            prefab = defaultIfNull;
        
        if(prefab.TryGetComponent(out Ingredient result))
        {
            craftingInteractRequirement = result.data.craftInteractionsRequired;
        }else
            craftingInteractRequirement = defaultCraftRequirement;

    }
}
