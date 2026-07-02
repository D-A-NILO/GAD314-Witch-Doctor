using UnityEngine;

[CreateAssetMenu(fileName = "Crafting", menuName ="Ingredient")]
public class IngredientData : ScriptableObject
{
    public string ingredientName;

    public GameObject rawPrefab;
    public GameObject choppedPrefab;
    public GameObject grindedPrefab;
}

