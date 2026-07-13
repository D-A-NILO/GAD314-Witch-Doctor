using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecipeBook : MonoBehaviour
{
    
    public InputActionReference toggleAction;
    public Recipe[] recipes;
    [SerializeField] private GameObject recipeDisplayPrefab;
    [SerializeField] private Transform container;


    void OnEnable()
    {
        toggleAction.action.performed += ToggleShow;

        toggleAction.action.Enable();
    }

    void OnDisable()
    {
      toggleAction.action.performed -= ToggleShow;  
    }

    void Start()
    {
        //populate recipe book
        foreach(Recipe recipe in recipes)
        {
            if(recipe.requiredIngredients.Count <= 0) continue;
            
            TMP_Text recipeTMP = Instantiate(recipeDisplayPrefab, container).GetComponentInChildren<TMP_Text>();
            
            string recipetxt = recipe.requiredIngredients[0].name;
            for(int i = 1; i < recipe.requiredIngredients.Count; i++)
            {
                recipetxt += " + " + recipe.requiredIngredients[i].name;
            }
            recipetxt += " = " + recipe.result.name;
            recipeTMP.text = recipetxt;
        }
    }

    private bool shown;
    private void ToggleShow(InputAction.CallbackContext context)
    {
        shown = !shown;

        container.gameObject.SetActive(shown);
        // Cursor.lockState = shown ? CursorLockMode.None : CursorLockMode.Locked;
        // Cursor.visible = shown;

    }
}
