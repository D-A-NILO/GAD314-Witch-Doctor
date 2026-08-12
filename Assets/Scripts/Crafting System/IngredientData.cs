using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName ="Crafting/Ingredient")]
public class IngredientData : ShopItemData
{
    public Color potionAffectColor;
    public Sprite sprite;
    public int craftInteractionsRequired;
    public GameObject choppedPrefab;
    public GameObject crushedPrefab;
}

