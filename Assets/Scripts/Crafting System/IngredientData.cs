using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName ="Crafting/Ingredient")]
public class IngredientData : ScriptableObject
{
    public Color potionAffectColor;
    public int craftInteractionsRequired;
    public GameObject choppedPrefab;
    public GameObject crushedPrefab;

    public int pricePerUnit;
    public GameObject worldPickupPrefab;
}

