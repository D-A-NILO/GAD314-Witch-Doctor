using System;
using UnityEngine;

[Serializable]
public class IngredientInfo
{
    public IngredientData ingredientData;
    public IngredientState ingredientState;

    public IngredientInfo(Ingredient ingredient)
    {
       // ingredientData = ingredient.ingredientData;
       // ingredientState = ingredient.ingredientState;
    }

    public IngredientInfo(IngredientData data, IngredientState state)
    {
        ingredientData = data;
        ingredientState = state;
    }
}
