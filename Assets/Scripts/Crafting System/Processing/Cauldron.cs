using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField] private  List<Recipe> allRecipes;
    [SerializeField] private Recipe failedRecipe;
    [SerializeField] private  List<IngredientData> ingredients = new List<IngredientData>();
    [SerializeField] private  Renderer mixingRenderer;
    [SerializeField] private Color mixingColor = Color.purple;
    //[SerializeField] private  Color SuccessColor = Color.green;
    //[SerializeField] private  Color FailedColor = Color.red;

    private float stirProgress = 0f;
    public float stirRequired = 100f;

    private bool isCrafted = false;
    private Recipe matchedRecipe;

    private MixState resultType = MixState.EMPTY;

    void Start()
    {
        ClearCauldron();
    }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log($"item entered cauldron: {other.name}");

        if(other.tag == "Sponge")
            ClearCauldron();

        //place ingredient into cauldron
        if (other.TryGetComponent(out Ingredient ingredient))
        {

            mixingRenderer.enabled = true; // show mixing
            mixingRenderer.material.SetColor("_Color", mixingColor);
            AddIngredient(ingredient);
            return;
        }
        //try fill bottle
        if (other.TryGetComponent(out Bottle bottle))
        { 
            TryFillBottle(bottle);
            return;
        }
        
        Debug.Log("somthing entered but nothing happened");
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient.data);
        PlayerInteract interactor = ingredient.GetComponent<Grabbable>().GetHoldingInteractor();
        if(interactor)
            interactor.DropItem();

        Destroy(ingredient.gameObject);

        Debug.Log($"{ingredient.data.name} added to cauldron");
    }

    public void AddStir(float amount)
    {
        if (ingredients.Count == 0 || isCrafted)
            return;

        stirProgress += amount;
        Debug.Log(stirProgress);

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
            resultType = MixState.SUCEEDED;
            Debug.Log($"craft success: {matchedRecipe.name}");
        }
        else
        {
            matchedRecipe = failedRecipe;
            resultType = MixState.FAILED;
            Debug.Log("craft failed");
        }

        mixingRenderer.material.SetColor("_Color", matchedRecipe.result.color);

        isCrafted = true;
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

        List<IngredientData> actualIngredients = ingredients.ToList();

        foreach (IngredientData required in recipe.requiredIngredients)
        { 
            bool found = false;

            foreach (IngredientData actual in actualIngredients)
            {
                if (required == actual)
                {
                    found = true;
                    //reduce list 
                    /// allows for matching multiple count of same ingredients and reduces search time
                    actualIngredients.Remove(actual);
                    break;
                }
            }

            if (!found)
                return false;
        }

        return true;
    }

    public void TryFillBottle(Bottle bottle)
    {
        if (resultType == MixState.EMPTY)
        {
            Debug.Log("Nothing to bottle");
            return;
        }

        bottle.Fill(matchedRecipe.result);
        Debug.Log($"bottle filled with {matchedRecipe.result.name}");

        ClearCauldron();
    }

    public void ClearCauldron()
    { 
        ingredients.Clear();
        stirProgress = 0f;
        isCrafted = false;
        matchedRecipe = null;
        mixingRenderer.enabled = false;
        resultType = MixState.EMPTY;
    }
}
