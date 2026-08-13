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
            
            RecipeDisplayRow recipeRow = Instantiate(recipeDisplayPrefab, container).GetComponentInChildren<RecipeDisplayRow>();
            
            recipeRow.SetRecipe(recipe);
        }
    }

    private bool shown;
    private void ToggleShow(InputAction.CallbackContext context)
    {
        shown = !shown;

        container.gameObject.SetActive(shown);
        Cursor.lockState = shown ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = shown;
        //disable camera movement
        FindAnyObjectByType<PlayerCam>().enabled = !shown;

        if(!shown) Tooltip.SetActive(false);

    }
}
