using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class ChoppingBoard : CraftingTable
{

    [SerializeField] private GameObject defaultIfNull;
    protected override Ingredient CraftIngredient()
    {
        GameObject prefab = heldIngredient.data.choppedPrefab;
        if(!prefab)
            prefab = defaultIfNull;
            
        GameObject obj = Instantiate(prefab, holdPoint.position, holdPoint.rotation);
        
        Destroy(heldIngredient.gameObject);

        return obj.GetComponent<Ingredient>();
    }
}
