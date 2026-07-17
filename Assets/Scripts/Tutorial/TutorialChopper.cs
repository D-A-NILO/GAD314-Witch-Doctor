using UnityEngine;
using UnityEngine.Events;

public class TutorialChopper : ChoppingBoard
{
    public UnityEvent OnPlaced;
    public UnityEvent OnProcessed;
    public UnityEvent OnTrashed;
    public override void PlaceIngredient(Ingredient ingredient)
    {
        base.PlaceIngredient(ingredient);
        OnPlaced?.Invoke();
    }

    protected override Ingredient CraftIngredient()
    {
        Ingredient ingredient =  base.CraftIngredient();

        OnProcessed?.Invoke();
        
        if(ingredient == null) // trash
            OnTrashed?.Invoke();

        return ingredient;
    }
}
