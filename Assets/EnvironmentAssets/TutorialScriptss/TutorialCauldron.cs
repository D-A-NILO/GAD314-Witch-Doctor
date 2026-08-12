using UnityEngine;
using UnityEngine.Events;

public class TutorialCauldron : Cauldron
{
    
    
    public int requiredIngredientCount = 2; 

    [Header("Tutorial Hooks")]
    public UnityEvent OnSingleIngredientAdded; // Fires on EVERY ingredient
    public UnityEvent OnRequiredIngredientsMet; // Fires only when the pot is full enough!
    public UnityEvent OnSuccessfulStir;
    public UnityEvent OnFailedStir;
    public UnityEvent OnSuccessfullyBottled;

    public override void AddIngredient(Ingredient ingredient)
    {
        base.AddIngredient(ingredient); 

        OnSingleIngredientAdded?.Invoke();

        // Check if the cauldron now contains the required amount of items
        if (ingredients.Count == requiredIngredientCount)
        {
            OnRequiredIngredientsMet?.Invoke(); 
        }
    }

    protected override void CraftRecipe(Recipe recipeToCraft)
    {
        base.CraftRecipe(recipeToCraft);

        if (resultType == MixState.SUCEEDED)
        {
            OnSuccessfulStir?.Invoke();
        }
        else if (resultType == MixState.FAILED)
        {
            OnFailedStir?.Invoke();
        }
    }

    public override void TryFillBottle(Bottle bottle)
    {
        base.TryFillBottle(bottle);

        if (bottle.hasPotion && resultType == MixState.EMPTY)
        {
            OnSuccessfullyBottled?.Invoke();
        }
    }
}
