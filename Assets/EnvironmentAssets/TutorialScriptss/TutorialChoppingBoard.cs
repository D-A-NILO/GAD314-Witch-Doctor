using UnityEngine;
using UnityEngine.Events;

public class TutorialChoppingBoard : ChoppingBoard
{
    [Header("Tutorial Hooks")]
    public UnityEvent OnIngredientPlaced;
    public UnityEvent OnSuccessfulChop;
    public UnityEvent OnOverprocessed;

    public override void PlaceIngredient(Ingredient ingredient)
    {
        base.PlaceIngredient(ingredient); // Do normal table logic
        OnIngredientPlaced?.Invoke(); // Tell the tutorial manager!
    }

    protected override Ingredient CraftIngredient()
    {
        Ingredient result = base.CraftIngredient(); // Do normal chopping logic

        
        if (result == null)
        {
            OnOverprocessed?.Invoke();
        }
        else
        {
            OnSuccessfulChop?.Invoke();
        }

        return result;
    }
}
