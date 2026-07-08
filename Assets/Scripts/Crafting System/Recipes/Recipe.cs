using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New recipe", menuName = "crafting/recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public string recipeDescription;
    public List<IngredientInfo> requiredIngredients = new List<IngredientInfo>();
    public GameObject potionPrefab;
}
