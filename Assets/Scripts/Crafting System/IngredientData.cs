using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName ="Crafting/Ingredient")]
public class IngredientData : ScriptableObject
{
    public int craftInteractionsRequired;
    public GameObject choppedPrefab;
    public GameObject crushedPrefab;

    public int pricePerUnit;
    public GameObject worldPickupPrefab;
}

