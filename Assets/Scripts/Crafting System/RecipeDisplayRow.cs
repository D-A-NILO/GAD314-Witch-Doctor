using TMPro;
using UnityEngine;

public class RecipeDisplayRow : MonoBehaviour
{
    [SerializeField] private Transform itemsContainer;
    [SerializeField] private TMP_Text recipeTitle;
    [SerializeField] private recipeMenuItem recipeMenuItemPrefab;

    public void SetRecipe(Recipe recipe)
    {
        recipeTitle.text = recipe.name;
        foreach(IngredientData ingredient in recipe.requiredIngredients)
        {
            recipeMenuItem recipeItem = Instantiate(recipeMenuItemPrefab, itemsContainer);
            recipeItem.setIngredient(ingredient);
        }
    }
}
