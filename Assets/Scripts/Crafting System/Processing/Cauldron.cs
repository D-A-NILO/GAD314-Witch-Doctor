using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour, IInteractable
{
    public List<Recipe> allRecipes;
    public List<IngredientInfo> ingredients = new List<IngredientInfo>();

    public float stirProgress = 0f;
    public float stirRequired = 100f;

    public bool isCrafted = false;
    public Recipe matchedRecipe;

    private Ingredient currentIngredient;

    public EquipItem playerEquip;

    public PotionResultType resultType = PotionResultType.Unknown;
    public Recipe resultRecipe;

    public void OnInteract(PlayerInteract playerInteract)
    {
        GameObject heldItem = playerEquip.GetActiveItem();

        Debug.Log($"Held item: {heldItem}");

        //place ingredient into cauldron
        if (heldItem != null && heldItem.TryGetComponent(out Ingredient ingredient))
        {
            //AddIngredient(ingredient);
            return;
        }
        //try fill bottle
        if (heldItem != null && heldItem.TryGetComponent(out FillingThePotion bottle))
        { 
            TryFillBottle(bottle);
            return;
        }
        
        Debug.Log("cauldron interacted but nothing happened");
    }

    //public void AddIngredient(Ingredient ingredient)
   // {
       // ingredients.Add(new IngredientInfo(
            //ingredient.ingredientData,
            //ingredient.ingredientState
       // ));

      //  playerEquip.RemoveActiveItem();

      //  Destroy(ingredient.gameObject);

      //  Debug.Log("ingredient added to cauldron");
    //}

    public void AddStir(float amount)
    {
        if (ingredients.Count == 0 || isCrafted)
            return;

        stirProgress += amount;

        if (stirProgress >= stirRequired)
        {
            CraftPotion();
        }
    }

    private void CraftPotion()
    {
        matchedRecipe = FindMatchingRecipe();

        if (matchedRecipe != null)
        {
            resultType = PotionResultType.SuccessfulMix;
            resultRecipe = matchedRecipe;
            Debug.Log($"craft success: {matchedRecipe.recipeName}");
            isCrafted = true;
        }
        else
        {
            resultType = PotionResultType.FailedMix;
            resultRecipe = null;
            Debug.Log("craft failed");
        }

        
        stirProgress = 0f;
    }

    private Recipe FindMatchingRecipe()
    { 
        foreach (Recipe recipe in allRecipes)
        {
            if (IsMatch(recipe))
                return recipe;
        }

        return null;
    }

    private bool IsMatch(Recipe recipe)
    { 
        if (recipe.requiredIngredients.Count != ingredients.Count)
            return false;

        foreach (IngredientInfo required in recipe.requiredIngredients)
        { 
            bool found = false;

            foreach (IngredientInfo actual in ingredients)
            {
                if (required.ingredientData == actual.ingredientData && required.ingredientState == actual.ingredientState)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                return false;
        }

        return true;
    }

    public void TryFillBottle(FillingThePotion bottle)
    {
        if (resultType == PotionResultType.Unknown)
        {
            Debug.Log("nothing to bottle");
            return;
        }

        bottle.FillFromCauldron(this);

        Debug.Log("bottle filled successfully");

        ClearCauldron();
    }

    public void ClearCauldron()
    { 
        ingredients.Clear();
        stirProgress = 0f;
        isCrafted = false;
        matchedRecipe = null;
    }
}
