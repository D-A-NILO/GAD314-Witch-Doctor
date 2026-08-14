using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField] private  List<Recipe> allRecipes;
    [SerializeField] private Recipe failedRecipe;
    [SerializeField] private PotionData unmixedPotion;
    [Tooltip("When enabled will overide potion color of failed potions with FailedOverideColor")]
    [SerializeField] bool overrideFailColor = false;
    [SerializeField] Color failedColorOverride = Color.black;
    [Tooltip("When enabled will use average color, otherwise will use potionData Color")]
    [SerializeField] bool useAverageColor = true;
    [SerializeField] protected  List<IngredientData> ingredients = new List<IngredientData>();
    [SerializeField] private  Renderer mixingRenderer;
    [SerializeField] private ParticleSystem ingredientParticle;
    [SerializeField] private ParticleSystem stirringParticle;
    [SerializeField] private ParticleSystem fillPotionParticle;

    private float stirProgress = 0f;
    public float stirRequired = 100f;
    private ParticleSystem ingredientParticleInstance;
    private bool isCrafted = false;
    private Recipe matchedRecipe;

    protected MixState resultType = MixState.EMPTY;

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

    private Ingredient lastIngredient; // stops double hitboxes from triggering
    public virtual void AddIngredient(Ingredient ingredient)
    {
        if(ingredient == lastIngredient) return;
        lastIngredient = ingredient;
        
        ingredients.Add(ingredient.data);
        PlayerInteract interactor = ingredient.GetComponent<Grabbable>().GetHoldingInteractor();
        if(interactor)
            interactor.DropItem();

        SpawnIngredientParticles(ingredient);
        Debug.Log($"spawn particles has been called: {ingredientParticle}");

        Destroy(ingredient.gameObject);

        resultType = MixState.UNMIXED;
        isCrafted = false;
        mixingRenderer.enabled = true; // show mixing if not
        mixingRenderer.material.SetColor("_Color", ingredient.data.potionAffectColor);

        Debug.Log($"{ingredient.data.name} added to cauldron");
    }

    public void AddStir(float amount)
    {
        if (ingredients.Count == 0 || isCrafted)
            return;

        stirProgress += amount;
        var mainModule = Instantiate(stirringParticle, transform.position, transform.rotation).main;
        mainModule.startColor = AverageIngredientColor;
        Debug.Log(stirProgress);

        if (stirProgress >= stirRequired)
        {
            CraftRecipe(FindMatchingRecipe());
        }
    }

    protected virtual void CraftRecipe(Recipe recipeToCraft)
    {
        //generate average ingredient color...

        //TODO: generate on Enable in recipe class


        if (recipeToCraft != null)
        {
            resultType = MixState.SUCEEDED;
            Debug.Log($"craft success: {recipeToCraft.name}");

            if(useAverageColor)
                recipeToCraft.result.color = AverageIngredientColor;
        }
        else
        {
            recipeToCraft = failedRecipe;
            resultType = MixState.FAILED;
            Debug.Log("craft failed");


            if(overrideFailColor)
            {
                recipeToCraft.result.color = failedColorOverride;
            }else
                if(useAverageColor)
                    recipeToCraft.result.color = AverageIngredientColor;
                    
        }
        
        
        mixingRenderer.material.SetColor("_Color", recipeToCraft.result.color);

        isCrafted = true;
        matchedRecipe = recipeToCraft;
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

    public virtual void TryFillBottle(Bottle bottle)
    {
        if(bottle.PotionData != null) // already filled
        {
            Debug.Log("Bottle is already filled");
            return;
        }
        if (resultType == MixState.EMPTY)
        {
            Debug.Log("Nothing to bottle");
            return;
        }

        PotionData result;
        if(resultType == MixState.UNMIXED)
        {
            result = unmixedPotion;
            result.color = AverageIngredientColor;
        }
        else
            result = matchedRecipe.result;

        bottle.Fill(result);
        var mainModule = Instantiate(fillPotionParticle, transform.position, transform.rotation).main;
        mainModule.startColor = AverageIngredientColor;
        Debug.Log($"bottle filled with {result.name}");

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

    public Color AverageIngredientColor
    {
        get
        {
            Color totalColor = new Color(0,0,0);
            foreach(IngredientData iData in ingredients)
            {
                totalColor += iData.potionAffectColor;
            }
            return totalColor /= ingredients.Count;
        }
    }

    private void SpawnIngredientParticles(Ingredient ingredient)
    {
        ingredientParticleInstance = Instantiate(ingredientParticle, transform.position, Quaternion.identity);
        var MainModule = ingredientParticleInstance.main;
        MainModule.startColor = ingredient.data.potionAffectColor;
    }

}
