using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New recipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeDescription;
    public List<IngredientData> requiredIngredients = new List<IngredientData>();
    public PotionData result;
}
