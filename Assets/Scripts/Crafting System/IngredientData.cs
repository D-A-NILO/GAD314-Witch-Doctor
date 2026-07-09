using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName ="Crafting/Ingredient")]
public class IngredientData : ScriptableObject
{
    public GameObject choppedPrefab;
    public GameObject crushedPrefab;
}

