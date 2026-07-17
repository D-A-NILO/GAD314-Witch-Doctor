using UnityEngine;
using UnityEngine.Events;

public class TutorialCauldron : Cauldron
{
    public int requiredIngredientCount;

    public UnityEvent OnIngredientsAdded;
    public UnityEvent OnRecipeCrafted;
    public UnityEvent OnPotionFIlled;
    public UnityEvent OnValidPotionFIlled;

    public override void AddIngredient(Ingredient ingredient)
    {
        base.AddIngredient(ingredient);

        if(ingredients.Count >= requiredIngredientCount)
        {
            OnIngredientsAdded?.Invoke();
        }
    }

    bool validRecipe;
    protected override void CraftRecipe(Recipe recipeToCraft)
    {
        base.CraftRecipe(recipeToCraft);
        OnRecipeCrafted?.Invoke();

        validRecipe = resultType == MixState.SUCEEDED;
    }

    public override void TryFillBottle(Bottle bottle)
    {
        base.TryFillBottle(bottle);

        if(bottle.hasPotion)
        {
            OnPotionFIlled?.Invoke();

            if(validRecipe) OnValidPotionFIlled?.Invoke();
        }
    }
}
